using FluentAssertions;
using Moq;
using System;
using ToyRobotMain;
using ToyRobotMain.Exceptions;
using ToyRobotMain.Interfaces;
using ToyRobotMain.Main;
using ToyRobotMain.Models;
using Xunit;
using static ToyRobotMain.Enums;

namespace ToyRobotTests
{
    public class ToyRobotTests
    {
        [Theory]
        [InlineData(0, 0, RobotDirection.NORTH, 0, 0)]
        [InlineData(3, 3, RobotDirection.SOUTH, 3, 3)]
        [InlineData(2, 2, RobotDirection.WEST, 2, 2)]
        [InlineData(2, 2, RobotDirection.EAST, 2, 2)]
        public void ToyRobot_ShouldBePlacedOnTheTableIfCorrectPlacementProvided(int startingY, int startingX, RobotDirection robotDirection, int expectedPositionX, int expectedPositionY)
        {
            //arrange
            Mock<IPlacementValidator> validator = new Mock<IPlacementValidator>();

            var mockPlacement = Mock.Of<Placement>(m =>
                m.AxisY == startingY &&
                m.AxisX == startingX &&
                m.Direction == robotDirection);

            ToyRobot robot = MockRobotInitialPlacement(startingY, startingX, robotDirection, validator);

            //act
            robot.PlaceRobot(mockPlacement, It.IsAny<string>());

            //assert
            robot._AxisX.Should().Be(expectedPositionX);
            robot._AxisY.Should().Be(expectedPositionY);
            robot._Direction.Should().Be(robotDirection);
        }

        [Theory]
        [InlineData(0, 0, RobotDirection.NORTH, 0, 1)]
        [InlineData(3, 3, RobotDirection.SOUTH, 3, 2)]
        [InlineData(2, 2, RobotDirection.WEST, 1, 2)]
        [InlineData(2, 2, RobotDirection.EAST, 3, 2)]
        public void ToyRobot_ShouldMove_IfCorrectDirectionProvided(int startingY, int startingX, RobotDirection robotDirection, int expectedPositionX, int expectedPositionY)
        {
            //arrange
            Mock<IPlacementValidator> validator = new Mock<IPlacementValidator>();
            validator.Setup(x => x.IsValidPlacementPosition(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            ToyRobot robot = MockRobotInitialPlacement(startingY, startingX, robotDirection, validator);

            //act
            robot.Move();

            //assert
            robot._AxisX.Should().Be(expectedPositionX);
            robot._AxisY.Should().Be(expectedPositionY);
        }

        [Theory]
        [InlineData(0, 0, RobotDirection.SOUTH, 0, 0)]
        [InlineData(4, 4, RobotDirection.NORTH, 4, 4)]
        [InlineData(2, 0, RobotDirection.WEST, 0, 2)]
        [InlineData(4, 2, RobotDirection.EAST, 2, 4)]
        public void ToyRobot_ShouldIgnoreMoveThatWouldCauseHimToFall(int startingY, int startingX, RobotDirection robotDirection, int expectedPositionX, int expectedPositionY)
        {
            //arrange
            Mock<IPlacementValidator> validator = new Mock<IPlacementValidator>();
            validator.Setup(x => x.IsValidPlacementPosition(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            ToyRobot robot = MockRobotInitialPlacement(startingY, startingX, robotDirection, validator);

            //act
            robot.Move();

            //assert
            robot._AxisX.Should().Be(expectedPositionX);
            robot._AxisY.Should().Be(expectedPositionY);
        }

        [Theory]
        [InlineData(0, 0, RobotDirection.SOUTH, 0, 0, RobotDirection.SOUTH)]
        [InlineData(4, 4, RobotDirection.NORTH, 4, 4, RobotDirection.NORTH)]
        [InlineData(2, 0, RobotDirection.WEST, 0, 2, RobotDirection.WEST)]
        [InlineData(4, 2, RobotDirection.EAST, 2, 4, RobotDirection.EAST)]
        public void ToyRobot_ShouldRotateWithoutChangingPosition(int startingY, int startingX, RobotDirection robotInitialDirection, int expectedPositionX, int expectedPositionY, RobotDirection robotDirectionAfterRotation)
        {
            //arrange
            Mock<IPlacementValidator> validator = new Mock<IPlacementValidator>();
            ToyRobot robot = MockRobotInitialPlacement(startingY, startingX, robotInitialDirection, validator);

            //act
            robot.TurnLeft();
            robot.TurnRight();

            //assert
            robot._AxisX.Should().Be(expectedPositionX);
            robot._AxisY.Should().Be(expectedPositionY);
            robot._Direction.Should().Be(robotDirectionAfterRotation);
        }

        [Theory]
        [InlineData(new object[] { new string[] { "MOVE" } })]
        [InlineData(new object[] { new string[] { "LEFT" } })]
        [InlineData(new object[] { new string[] { "RIGHT" } })]
        [InlineData(new object[] { new string[] { "REPORT" } })]
        public void ToyCommander_ShouldIgnoreCommandIfRobotNotPlacedOnTheTable(string[] command)
        {
            //arrange
            Mock<IRobotCommander> commander = new Mock<IRobotCommander>();
            Mock<IPlacementValidator> validator = new Mock<IPlacementValidator>();
            validator.Setup(x => x._robotIsPlaced).Returns(false);
            Mock<IToyRobot> robotMock = new Mock<IToyRobot>();
            var robotCommander = new RobotCommander(robotMock.Object, validator.Object);

            //act
            Action act = () => robotCommander.Command(command);

            //assert
            var exception = Assert.Throws<PlacementErrorException>(act);
            exception.Message.Should().Be(Constants.defaultErrorPlacementMessage);
        }

        [Theory]
        [InlineData(new object[] { new string[] {  } })]
        [InlineData(new object[] { new string[] { "ASFDTCJMNKMSKNMDJKSDNJSFNJKSNFJDFDF" } })]
        [InlineData(new object[] { new string[] { "VODKA" } })]
        [InlineData(new object[] { new string[] { "RITA ORA" } })]
        public void ToyCommander_ShouldIgnoreInvalidCommands(string[] command)
        {
            //arrange
            Mock<IPlacementValidator> validator = new Mock<IPlacementValidator>();
            validator.Setup(x => x._robotIsPlaced).Returns(true);
            Mock<IToyRobot> robotMock = new Mock<IToyRobot>();
            var robotCommander = new RobotCommander(robotMock.Object, validator.Object);

            //act
            Action act = () => robotCommander.Command(command);

            //assert
            var exception = Assert.Throws<InvalidCommandException>(act);
            exception.Message.Should().Be(Constants.defaultErrorMessage);
        }

        [Fact]
        public void Report_ShouldAnnounceThePositionOfToyRobot()
        {
            //arrange
            var initialX = 0;
            var initialY = 0;
            var initialDirection = RobotDirection.NORTH;
            Mock<IPlacementValidator> validator = new Mock<IPlacementValidator>();
            ToyRobot robot = MockRobotInitialPlacement(initialX, initialY, initialDirection, validator);

            //act
            var report = robot.Report();

            //assert
            report.Should().NotBeNullOrEmpty();
            report.Should().Be($"Output: {initialX},{initialY},{initialDirection}");
        }

        [Fact]
        public void FurtherValidCommands_AreAllowedAfterToyRobotPreventedFromFalling()
        {
            //arrange
            var initialX = 0;
            var initialY = 0;
            var validMove = 1;
            var initialDirection = RobotDirection.SOUTH;
            var changedDirection = RobotDirection.NORTH;
            Mock<IPlacementValidator> validator = new Mock<IPlacementValidator>();
            validator.Setup(x => x.IsValidPlacementPosition(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            ToyRobot robot = MockRobotInitialPlacement(initialX, initialY, initialDirection, validator);

            //act
            robot.Move(); //invalid move
            robot.Move(); //invalid move
            validator.Setup(x => x.IsValidPlacementPosition(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            robot = MockRobotInitialPlacement(initialX, initialY, changedDirection, validator);//place the robot in the correct direction
            robot.Move(); //valid move

            //assert
            robot._AxisX.Should().Be(initialX);
            robot._AxisY.Should().Be(initialX + validMove);
        }

        [Fact]
        public void PlacementValidator_ShouldValidateThePlacementCommand()
        {
            //arrange
             string[] invalidPlacementCommand = { "PLACE" };
            var validator = new PlacementValidator();

            //act
            var result = validator.ValidatePlacementFormatting(invalidPlacementCommand);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<PlacementValidationResult>();
            result.PlacementSuccessfull.Should().BeFalse();
        }

        private static ToyRobot MockRobotInitialPlacement(int startingY, int startingX, RobotDirection robotDirection, Mock<IPlacementValidator> validator)
        {
            var robot = new ToyRobot(validator.Object);
            robot._AxisX = startingX;
            robot._AxisY = startingY;
            robot._Direction = robotDirection;
            return robot;
        }
    }
}
