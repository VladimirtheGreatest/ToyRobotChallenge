using ToyRobotMain.Interfaces;
using ToyRobotMain.Models;
using static ToyRobotMain.Enums;

namespace ToyRobotMain.Main
{
    public class PlacementValidator : IPlacementValidator
    {
        public bool _robotIsPlaced { get; set; } = false;

        public bool IsValidPlacementPosition(int position, int minimumPosition, int maximumPosition)
        {
            return position >= minimumPosition && position <= maximumPosition;
        }

        public PlacementValidationResult ValidatePlacementFormatting(string[] command)
        {
            int X;
            int Y;
            RobotDirection direction;

            if (command.Length < Constants.placementCommandFixedParameters)
            {
                return ProcessValidationResult(string.Format($"Not enough parameters for the placement command. Expected {Constants.placementCommandFixedParameters}, got {0}", command.Length));
            }

            if (!int.TryParse(command[1], out X))
            {
                return ProcessValidationResult(string.Format("Place parameter for X is not an integer: {0}", command[1]));
            }

            if (!int.TryParse(command[2], out Y))
            {
                return ProcessValidationResult(string.Format("Place parameter for Y is not an integer: {0}", command[2]));
            }

            if (!ExtensionMethods.TryParse(command[3], out direction))
            {
                return ProcessValidationResult($"Place parameter for direction is not a proper direction, please use the following directions {ExtensionMethods.GetArrayOfStringsFromEnum(new RobotDirection())}");
            }

            return ProccessSuccessfulPlacementValidation(X, Y, direction);
        }

        private PlacementValidationResult ProccessSuccessfulPlacementValidation(int X, int Y, RobotDirection direction)
        {
            var successfulResult = ProcessValidationResult(Constants.successfullPlacementFormattingValidationMessage, true);

            successfulResult.Placement = new Placement
            {
                AxisX = X,
                AxisY = Y,
                Direction = direction
            };

            return successfulResult;
        }

        private PlacementValidationResult ProcessValidationResult(string message, bool success = false)
        {
            var result = new PlacementValidationResult();

            if (success)
            {
                result.PlacementSuccessfull = true;
                return result;
            }

            result.ValidationMessage = message;
            return result;
        }

    }
}
