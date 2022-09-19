# Welcome to Vladimir`s Legendary Toy Robot Simulator 3600.



## Table of contents:

* [Description and requirements](./README.md#description)
  * [Constraints](./README.md#constraints)
  * [Example Input and Output](./README.md#example-input-and-output)
* [Setup](./README.md#setup)
* [Thoughts](./README.md#thoughts)

## Description

* The application is a simulation of a toy robot moving on a square tabletop, of dimensions 5 units x 5 units.

* There are no other obstructions on the table surface.

* The robot is free to roam around the surface of the table, but must be prevented from falling to destruction. Any movement that would result in the robot falling from the table must be prevented, however further valid movement commands must still be allowed.

Create an application that can read in commands of the following form:
```
PLACE X,Y,F
MOVE
LEFT
RIGHT
REPORT
```

* `PLACE` will put the toy robot on the table in position X,Y and facing NORTH, SOUTH, EAST or WEST.

* The origin (0,0) can be considered to be the SOUTH WEST most corner.

* The first valid command to the robot is a `PLACE` command, after that, any sequence of commands may be issued, in any order, including another `PLACE` command. The application should discard all commands in the sequence until a valid `PLACE` command has been executed

* `MOVE` will move the toy robot one unit forward in the direction it is currently facing.

* `LEFT` and `RIGHT` will rotate the robot 90 degrees in the specified direction without changing the position of the robot.

* `REPORT` will announce the X,Y and F of the robot. This can be in any form, but standard output is sufficient.

* A robot that is not on the table can choose to ignore the `MOVE`, `LEFT`, `RIGHT` and `REPORT` commands.

* Input can be from a file, or from standard input, as the developer chooses.

* Provide test data to exercise the application.

### Constraints

* The toy robot must not fall off the table during movement. This also includes the initial placement of the toy robot.

* Any move that would cause the robot to fall must be ignored.

### Example Input and Output:

#### Example a

    PLACE 0,0,NORTH
    MOVE
    REPORT

Expected output:

    0,1,NORTH

#### Example b

    PLACE 0,0,NORTH
    LEFT
    REPORT

Expected output:

    0,0,WEST

#### Example c

    PLACE 1,2,EAST
    MOVE
    MOVE
    LEFT
    MOVE
    REPORT

Expected output

    3,3,NORTH
    
## Setup

1. You can just clone this repo and run this app in the Visual Studio.Click on ToyRobotMain solution.  Set ToyRobotMain as a starting project. Build => Build Solution => Press F5 start debugging. Run tests by clicking Test => Run All tests.

2. You can publish this app into your local folder or ask me for an executable.


### Thoughts:

* The application should provide the elegant solution and super simple clean code written in the most beautiful language C#(thanks to Bill Gates). Specifically I was trying to put emphasis on readability, separation of concerns and maintainability of this application. 
Unit tests should ensure that a section of an application (especially the core functionality of the robot) meets its design and behaves as intended. Using dependency injection in order to be able to reuse the logic if different parts of the application are added in the future. Since the application 
is very small and I knew all possible commands beforehand I decided to use a very simple conditional logic to evaluate possible commands and move directions, eventually could create a class for a specific command that would inherit from the base command and evaluate commands without if, 
else statements, however for this specific type of the application I think it would be an absolute nonsense. Also many values are hardcoded in the Constants file, these could be stored in the Azure app configuration which would give the application more flexiblity to change the values dynamically. 
Comments describing the main functionality are inside the contracts. Thanks
