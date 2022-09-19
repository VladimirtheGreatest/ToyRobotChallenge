using System;
using ToyRobotMain.Exceptions;
using ToyRobotMain.Interfaces;
using static ToyRobotMain.Enums;

namespace ToyRobotMain.Main
{
    public class RobotCommander : IRobotCommander
    {
        private readonly IToyRobot _Robot;
        private readonly IPlacementValidator _PlacementValidator;
        public RobotCommander(IToyRobot rob, IPlacementValidator placementValidator)
        {
            _Robot = rob;
            _PlacementValidator = placementValidator;
        }

        public void Command(string[] command)
        {
            GuardCommand(command);
            EvaluateCommand(command);
        }

        private void GuardCommand(string[] command)
        {
            if (command == null || command.Length == 0)
                throw new InvalidCommandException(Constants.defaultErrorMessage);

            if (!_PlacementValidator._robotIsPlaced && command[0] != Commands.PLACE.ToString())
            {
                throw new PlacementErrorException(Constants.defaultErrorPlacementMessage);
            }
        }

        private void EvaluateCommand(string[] command)
        {
            if (command[0] == Commands.PLACE.ToString())
            {
                ProcessPlacementValidation(command);
            }
            else if (command[0] == Commands.MOVE.ToString())
            {
                _Robot.Move();
            }
            else if (command[0] == Commands.LEFT.ToString())
            {
                _Robot.TurnLeft();
            }
            else if (command[0] == Commands.RIGHT.ToString())
            {
                _Robot.TurnRight();
            }
            else if (command[0] == Commands.REPORT.ToString())
            {
                Console.WriteLine(_Robot.Report());
            }
            else
            {
                throw new InvalidCommandException(Constants.defaultErrorMessage);
            }
        }

        private void ProcessPlacementValidation(string[] command)
        {
            var placementValidation = _PlacementValidator.ValidatePlacementFormatting(command);

            if (placementValidation.PlacementSuccessfull)
            {
                var validPositionX = _PlacementValidator.IsValidPlacementPosition(placementValidation.Placement.AxisX, Constants.minimumPosition, Constants.maximumPosition);
                var validPositionY = _PlacementValidator.IsValidPlacementPosition(placementValidation.Placement.AxisY, Constants.minimumPosition, Constants.maximumPosition);

                if (!validPositionX || !validPositionY)
                {
                    throw new PlacementValidationException($"Invalid position for {Commands.PLACE} command, minimum position for both axis is {Constants.minimumPosition} and maximum position for both axis is {Constants.maximumPosition}");
                }

                _PlacementValidator._robotIsPlaced = true;
                _Robot.PlaceRobot(placementValidation.Placement, placementValidation.ValidationMessage);
            }
            else
            {
                throw new PlacementValidationException($"Invalid {Commands.PLACE} command, error: {placementValidation.ValidationMessage}");
            }
        }
    }
}
