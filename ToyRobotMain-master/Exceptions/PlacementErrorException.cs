using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotMain.Exceptions
{
    public class PlacementErrorException : Exception
    {
        public PlacementErrorException(string message) : base(message)
        {

        }
    }
}
