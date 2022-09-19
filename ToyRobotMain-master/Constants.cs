namespace ToyRobotMain
{
    /// <summary>
    /// //For this particular simulation many values are hardcoded since we know the dimensions of the table beforehand but could inject that from the azure app configuration if any of these needs to be changed in the future.
    /// </summary>
    public static class Constants
    {
        public const string welcomeMessage = "Welcome to Vladimir`s Legendary Toy Robot Simulator 3600";
        public const string defaultErrorMessage = "Invalid command, please use the following commands, PLACE X,Y,F, MOVE, LEFT, RIGHT, REPORT";
        public const string defaultErrorPlacementMessage = "It is required that the first command to the robot is a PLACE command, after that, any sequence of commands may be issued, in any order, including another PLACE command";
        public const int minimumPosition = 0; 
        public const int maximumPosition = 4;
        public const int placementCommandFixedParameters = 4;
        public const int robotStepDistance = 1;
        public const string successfullPlacementFormattingValidationMessage = "Placement Formatting Validated";
    }
}
