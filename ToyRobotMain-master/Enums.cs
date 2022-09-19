using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotMain
{
    public class Enums
    {
        public enum RobotDirection
        {
            NORTH,
            WEST,
            SOUTH,
            EAST,
        }
        public enum Commands
        {
            PLACE,
            MOVE,
            LEFT,
            RIGHT,
            REPORT
        }
    }
}
