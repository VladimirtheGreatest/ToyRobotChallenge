using ToyRobotMain.Models;
using static ToyRobotMain.Enums;

namespace ToyRobotMain.Interfaces
{
    public interface IToyRobot
    {
        int _AxisX { get; set; }
        int _AxisY { get; set; }
        RobotDirection _Direction { get; set; }
        /// <summary>
        /// MOVE will move the toy robot one unit forward in the direction it is currently facing.Any move that would cause the robot to fall must be ignored so we will not throw exceptions in case the command is invalid.
        /// </summary>
        void Move();
        /// <summary>
        /// PLACE will put the toy robot on the table in position X,Y and facing NORTH, SOUTH, EAST or WEST. The origin (0,0) can be considered to be the SOUTH WEST most corner.It is required that the first command to the robot is a PLACEcommand,
        /// after that, any sequence of commands may be issued, in any order, including another PLACE command.
        /// </summary>
        void PlaceRobot(Placement placement, string placementValidationMessage);
        /// <summary>
        /// REPORT will announce the X,Y and F of the robot.
        /// </summary>
        string Report();
        /// <summary>
        /// LEFT will rotate the robot 90 degrees in the specified direction without changing the position of the robot. Example If Robot is facing NORTH and you COMMAND LEFT, the robot will rotate to the WEST(new direction), WITHOUT MOVING.
        /// </summary>
        void TurnLeft();
        /// <summary>
        /// RIGHT will rotate the robot 90 degrees in the specified direction without changing the position of the robot. Example If Robot is facing NORTH and you COMMAND RIGHT, the robot will rotate to the EAST(new direction), WITHOUT MOVING.
        /// </summary>
        void TurnRight();
    }
}