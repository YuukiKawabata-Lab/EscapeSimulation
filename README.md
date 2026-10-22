# EscapeSimulation

This project contains the source code for the cricket escape simulation described in the following article:

[In preparation]

We used previously published datasets on wind-evoked escape behaviors of crickets as input data for the simulation. The data was collected in the following study:

Fukutomi, M. and Ogawa, H (2017). Crickets alter wind-elicited escape strategies depending on acoustic context. Scientific Reports, 7: 15158.
https://doi.org/10.1038/s41598-017-15276-x

## Prerequisites

- Windows (file paths are parsed with `\` as the separator)
- Unity 6000.3.11f1
- Git (for version control)

## Getting Started

1. Clone the repository:
   ```
   git clone https://github.com/YuukiKawabata-Lab/EscapeSimulation.git
   ```
2. Open Unity Hub and click "Add" to add the project.
3. Navigate to the cloned repository folder and select it.
4. Open the project using Unity version 6000.3.11f1.
5. Wait for Unity to import all assets and compile scripts.

## Usage

Follow these steps to use the simulation:

1. Open the scene: `Assets/Scenes/Menu.unity`
2. Press the Play button in the Unity Editor to run the simulation.
3. Enter the stimulus (frog tongue) speed in cm/s in the "Stimulus Speed (cm/s)" field. The default is 90.
4. Click the "Start Analysis" button to begin the simulation.
5. When prompted, select the top data folder you want to analyze (see [Data Folder Structure](#data-folder-structure)).
6. When prompted, specify the name of the CSV file where you want to save the results. The default name is `<yyyyMMddHHmmss>OutPutData`.
7. The simulation runs every combination of stimulus conditions for each trajectory CSV file and appends one row per combination to the result file.
8. Once all conditions have been simulated, the simulation stops. Press the `Q` key to stop the simulation early. Rows written before stopping are kept.

The "Start Simulation of Crikets' motion" button opens `Assets/Scenes/Motion.unity`. This scene is not used for the analysis.

## Simulation Conditions

For each trajectory, the stimulus is varied over the following conditions:

- **Tongue width (`ScaleX`, `ScaleY`)**: five levels, from 1/16 of the full tongue width up to the full width, doubling at each step (x1, x2, x4, x8, x16).
- **Terminal point (`TerminalPoint`)**: the stimulus endpoint is extended step by step for each tongue width.
- **FID (`FID`)**: the starting distance of the stimulus increases after all tongue widths and terminal points have been tried.

## Data Folder Structure

The expected data folder structure is as follows:

```
Top folder
├── Treatment 1 folder (e.g., "5-kHz tone")
│   ├── CSV file 1
│   ├── CSV file 2
│   └── ...
├── Treatment 2 folder (e.g., "15-kHz tone")
│   ├── CSV file 1
│   ├── CSV file 2
│   └── ...
└── Control folder (e.g., "Tone-free")
    ├── CSV file 1
    ├── CSV file 2
    └── ...
```

All `*.csv` files under the top folder are read, including those in nested subfolders. The name of the folder that directly contains each CSV file is written to the `Section` column of the result file.

## Input CSV File Format

The CSV files should be comma-separated and have the following structure. Rows are sampled at 200 Hz (5 ms per frame).

| Time.ms. | X.mm. | Y.mm. | Theta.deg. | Omega.deg.ms. | integrated.Theta.deg. | Trajectory.Length.mm. | Velocity.m.s. | stim |
|----------|-------|-------|------------|---------------|----------------------|----------------------|---------------|------|
| 0        | 0     | 0     | 0          | 0             | 0                    | 0                    | 0             | 1    |
| 5.00074  | 0.170 | -0.093| -0.07808   | -0.01561      | -0.07808             | 0.19407              | 0.03881       | 1    |
| ...      | ...   | ...   | ...        | ...           | ...                  | ...                  | ...           | ...  |

The first row is treated as a header and skipped. Columns are read by position, not by name. The simulation uses only the following three columns; the others are ignored:

| Position | Column | Use |
|----------|--------|-----|
| 2nd | `X.mm.` | Cricket X coordinate |
| 3rd | `Y.mm.` | Cricket Y coordinate |
| 6th | `integrated.Theta.deg.` | Cricket heading (yaw angle) |

## Output CSV File Format

The result file is UTF-8 with BOM. The header row is:

```
File Name, Section, Result, Result_frame, FID, ScaleY, ScaleX, EndPoint, TerminalPoint, ExitDangerFrame,
```

| Column | Description |
|--------|-------------|
| `File Name` | Input trajectory CSV file name |
| `Section` | Name of the folder containing the input file |
| `Result` | `Caught` or `NotCaught`. Each trial continues until the stimulus has finished its strike, even if the trajectory data ends earlier; after the last row the cricket stays at its final position. A trial is therefore never recorded as `NotCaught` while the strike is still in progress. (The code contains an `_Error` suffix, e.g. `Caught_Error`, but it is never written under this rule.) |
| `Result_frame` | Frame at which the trial ended |
| `FID` | Flight initiation distance of the stimulus (cm) |
| `ScaleY`, `ScaleX` | Tongue width (diameter, cm). The capture area is a circle (`CircleCollider2D`) scaled equally in X and Y, so both values are equal |
| `EndPoint` | Stimulus terminal point (center) |
| `TerminalPoint` | Stimulus terminal point (edge) |
| `ExitDangerFrame` | First frame at which the cricket left the danger area. Empty if it never left. Multiply by 5 to convert to ms |

The header ends with a trailing comma, so CSV readers may show one extra empty column.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.



