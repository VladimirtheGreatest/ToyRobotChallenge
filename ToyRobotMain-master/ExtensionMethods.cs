using System;
using System.Linq;
using static ToyRobotMain.Enums;

namespace ToyRobotMain
{
    public static class ExtensionMethods
    {
        public static string GetArrayOfStringsFromEnum(Enum e)
        {
            return "[" + string.Join(", ", Enum.GetNames(e.GetType()).Select(e => e).Select(s => $"'{s}'")) + "]";
        }

        public static bool TryParse(string input, out RobotDirection direction)
        {
            return Enum.TryParse<RobotDirection>(input, out direction);
        }

        public static string[] GetArrayFromInput(string input)
        {
            return input?.Split(new string[] { " ", "," }, StringSplitOptions.None);
        }
        public static RobotDirection EvaluatePositionForLeft(RobotDirection direction)
        {
            return (direction == RobotDirection.EAST) ? RobotDirection.NORTH : (RobotDirection)((int)direction + 1);
        }

        public static RobotDirection EvaluatePositionForRight(RobotDirection direction)
        {
            return (direction == RobotDirection.NORTH) ? RobotDirection.EAST : (RobotDirection)((int)direction - 1);
        }
    }
}
