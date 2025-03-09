# EscapeSimulation Project Structure

## Project Overview
This project is an evacuation simulation system built with Unity. It simulates subject evacuation behavior and escape from dangerous areas, enabling data collection and analysis.

## Directory Structure
```
EscapeSimulation/
├── Assets/                     # Main program code and resources
│   ├── Scenes/                # Unity scene files
│   ├── Resources/             # In-game resources
│   ├── StandaloneFileBrowser/ # File browser related
│   └── [Script files]
├── ProjectSettings/           # Unity project settings
├── UserSettings/             # User-specific settings
└── Packages/                 # Dependencies
```

## Main Script Files

### Core System
- `MainScene.cs`
  - Main scene control
  - Implementation of core simulation features
  - Scene object management

- `Stimulus.cs`
  - Stimulus generation and control
  - Stimulus parameter management
  - Stimulus display timing control

- `Cricket.cs`
  - Simulation subject movement control
  - Movement parameter management
  - Collision detection processing

### Danger Area Related
- `DangerArea.cs`
  - Danger area definition
  - Area state management
  - Danger level control

- `ExitDangerArea.cs`
  - Danger area escape logic
  - Escape route calculation

- `ExitDangerRoad.cs`
  - Dangerous road escape processing
  - Road evacuation behavior control

### Data Management
- `FileManeger.cs`
  - Basic file system operations
  - File I/O management

- `FileReadWrite.cs`
  - Data read/write processing
  - File format handling

- `CsvSave.cs`
  - CSV format saving of simulation data
  - Data format definition
  - Save process implementation

### User Interface
- `FID_Slider.cs`
  - Slider UI component
  - Parameter adjustment interface

- `Speed_Input.cs`
  - Speed parameter input processing
  - Value validation and application

- `Menu.cs`
  - Menu screen control
  - User operation management

### Collision Detection System
- `Collider.cs`
  - Basic collision detection logic
  - Object interference checking

## Resource Files
- `stimulus.png`: Normal state stimulus image
- `stimulusRed.png`: Warning state stimulus image
- `criket.png`: Simulation subject sprite

## Dependencies
- Unity Engine: Simulation platform
- System.Drawing.dll: Drawing functionality library

## Configuration Files
- `ProjectSettings/`: Unity project basic settings
- `UserSettings/`: User-specific environment settings
- `Assembly-CSharp.csproj`: C# project settings
- `EscapeSimulation.sln`: Solution settings

## Development Environment
- Unity
- C#
- Visual Studio / Visual Studio Code

## Important Notes
- Appropriate Unity version is required to run the project
- Simulation data must be saved to directories with proper access permissions
- System.Drawing.dll is required as part of the .NET Framework 