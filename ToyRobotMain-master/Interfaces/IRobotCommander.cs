namespace ToyRobotMain.Interfaces
{
    public interface IRobotCommander
    {
        /// <summary>
        /// Command will evaluate and discard all commands in the sequence until a valid PLACE command has been executed. Successfull commands will be passed down to <see cref="IToyRobot"/>
        /// The placement will be validated by <see cref="IPlacementValidator"/>
        /// </summary>
        /// <param name="command">User`s input from the console line.</param>
        void Command(string[] command);
    }
}