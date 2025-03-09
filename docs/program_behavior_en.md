# EscapeSimulation Program Behavior

## Simulation Overview
This program is a system designed to simulate evacuation behavior from dangerous areas and analyze subject reactions and behavioral patterns.

## Main Flow

### 1. Simulation Initialization
1. Scene initialization by `MainScene.cs`
2. Simulation parameter settings
   - Stimulus display interval
   - Danger area placement
   - Initial movement speed
3. Subject (Cricket) initial position setting

### 2. Stimulus Presentation System
1. Stimulus control by `Stimulus.cs`
   - Switching between normal state (stimulus.png) and warning state (stimulusRed.png)
   - Stimulus display timing control
   - Dynamic updating of stimulus position

### 3. Danger Area Management
1. Danger area control by `DangerArea.cs`
   - Management of danger levels within areas
   - Contact detection with subjects
   - Warning display control

2. Escape Processing
   - `ExitDangerArea.cs`: Area escape logic
   - `ExitDangerRoad.cs`: Road escape processing
   - Calculation and presentation of optimal escape routes

### 4. Subject Behavior Control
1. Movement control by `Cricket.cs`
   - Keyboard input movement
   - Speed parameter application
   - Collision detection and movement restrictions

2. Collision Detection System
   - Object interference checking by `Collider.cs`
   - Wall and obstacle collision processing
   - Danger area contact detection

### 5. Data Collection and Storage
1. Data recording by `CsvSave.cs`
   - Movement trajectory recording
   - Stimulus reaction time
   - Time spent in danger areas
   - Escape route selection

2. File Management
   - `FileManeger.cs`: Basic file system operations
   - `FileReadWrite.cs`: Data read/write processing
   - CSV format data storage

## User Interface

### 1. Parameter Adjustment
- Slider UI by `FID_Slider.cs`
  - Stimulus intensity adjustment
  - Display interval settings
  - Danger level adjustment

### 2. Speed Control
- Speed parameter settings by `Speed_Input.cs`
  - Movement speed input
  - Value validation
  - Real-time speed updates

### 3. Menu Operations
- Menu control by `Menu.cs`
  - Simulation start/stop
  - Parameter reset
  - Data save operations

## Data Format

### 1. Recording Data
```csv
TimeStamp,Position_X,Position_Y,Stimulus_State,DangerArea_Contact,Reaction_Time
```
- TimeStamp: Recording timestamp
- Position_X/Y: Subject coordinates
- Stimulus_State: Stimulus state
- DangerArea_Contact: Danger area contact state
- Reaction_Time: Stimulus reaction time

### 2. Configuration Data
```json
{
  "simulation": {
    "stimulus_interval": 1000,
    "danger_level": 3,
    "movement_speed": 5
  },
  "display": {
    "resolution": "1920x1080",
    "refresh_rate": 60
  }
}
```

## Error Handling
1. Collision Detection Errors
   - Boundary checking
   - Invalid movement prevention
   - Error log recording

2. File Operation Errors
   - Save directory existence verification
   - File access permission verification
   - Automatic backup on error

3. Parameter Validation
   - Input value range checking
   - Invalid setting prevention
   - Default value fallback

## Important Notes
1. Performance
   - Load from frequent collision detection
   - Memory usage during mass data storage
   - Frame rate drop countermeasures

2. Data Storage
   - Regular automatic saving
   - Backup file creation
   - Capacity management for long experiments

3. Experimental Environment
   - Display refresh rate settings
   - Input device response verification
   - System resource allocation 