using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using ToyRobotMain.Interfaces;
using ToyRobotMain.Main;

namespace ToyRobotMain
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(Constants.welcomeMessage);

            var host = CreateHostBuilder(args).Build();

            var robotCommander = host.Services.GetService<IRobotCommander>();

            while (true)
            {
                try
                {
                    robotCommander.Command(ExtensionMethods.GetArrayFromInput(Console.ReadLine()));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                }
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                 //Registering dependencies.    
                 services.AddScoped<IToyRobot, ToyRobot>();
                 services.AddScoped<IRobotCommander, RobotCommander>();
                 services.AddScoped<IPlacementValidator, PlacementValidator>();
                });

            return hostBuilder;
        }
    }
}
