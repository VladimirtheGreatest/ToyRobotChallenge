using System;
using ToyRobotMain.Interfaces;
using ToyRobotMain.Models;
using static ToyRobotMain.Enums;

namespace ToyRobotMain.Main
{
    public class ToyRobot : IToyRobot
    {
        public int _AxisX { get; set; }
        public int _AxisY { get; set; }
        public RobotDirection _Direction { get; set; }

        private readonly IPlacementValidator _PlacementValidator;

        public ToyRobot(IPlacementValidator placementValidator)
        {
            _PlacementValidator = placementValidator;
        }

        public void PlaceRobot(Placement placement, string placementValidationMessage)
        {
            Console.WriteLine($"{placementValidationMessage}");
            Console.WriteLine($"Placed at {placement.AxisX} and {placement.AxisY}, direction {placement.Direction}");

            _AxisX = placement.AxisX;
            _AxisY = placement.AxisY;
            _Direction = placement.Direction;
        }

        public void Move()
        {
            if (_Direction == RobotDirection.NORTH && _PlacementValidator.IsValidPlacementPosition(_AxisY + Constants.robotStepDistance, Constants.minimumPosition, Constants.maximumPosition))
            {
                _AxisY += Constants.robotStepDistance;
            }
            else if (_Direction == RobotDirection.SOUTH && _PlacementValidator.IsValidPlacementPosition(_AxisY - Constants.robotStepDistance, Constants.minimumPosition, Constants.maximumPosition))
            {
                _AxisY -= Constants.robotStepDistance;
            }
            else if (_Direction == RobotDirection.EAST && _PlacementValidator.IsValidPlacementPosition(_AxisX + Constants.robotStepDistance, Constants.minimumPosition, Constants.maximumPosition))
            {
                _AxisX += Constants.robotStepDistance;
            }
            else if (_Direction == RobotDirection.WEST && _PlacementValidator.IsValidPlacementPosition(_AxisX - Constants.robotStepDistance, Constants.minimumPosition, Constants.maximumPosition))
            {
                _AxisX -= Constants.robotStepDistance;
            }
        }

        public void TurnLeft()
        {
            _Direction = ExtensionMethods.EvaluatePositionForLeft(_Direction);
        }

        public void TurnRight()
        {
            _Direction = ExtensionMethods.EvaluatePositionForRight(_Direction);
        }

        public string Report()
        {
            return $"Output: { _AxisX},{ _AxisY},{ _Direction}";
        }
    }
}
