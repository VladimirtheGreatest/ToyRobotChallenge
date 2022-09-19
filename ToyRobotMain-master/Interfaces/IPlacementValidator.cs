using ToyRobotMain.Models;

namespace ToyRobotMain.Interfaces
{
    public interface IPlacementValidator
    {
        /// <summary>
        /// The current state of robot placement. We cannot "unplace" the robot, once it is placed on the table we can run commands. We can also place the robot again.
        /// </summary>
        bool _robotIsPlaced { get; set; }

        /// <summary>
        /// Validates the initial placement position and also the moving position in which the toy robot is moving to.
        /// The toy robot must not fall off the table during movement. This also includes the initial placement of the toy robot. This helps us to ignore the move that would cause the robot to fall and also to ignore the invalid placement.
        /// </summary>
        bool IsValidPlacementPosition(int position, int minimumPosition, int maximumPosition);

        /// <summary>
        /// Validates the placement command formatting, produces the validation result <see cref="PlacementValidationResult"/>
        /// </summary>
        /// <param name="command">User`s input from the console line.</param>
        PlacementValidationResult ValidatePlacementFormatting(string[] command);
    }
}