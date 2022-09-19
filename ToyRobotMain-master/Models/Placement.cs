using static ToyRobotMain.Enums;

namespace ToyRobotMain.Models
{
    public class Placement
    {
        public int AxisX { get; set; }
        public int AxisY { get; set; }
        public RobotDirection Direction { get; set; }
    }
}