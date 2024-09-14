# EscapeSimulation

This project contains the source code for the cricket escape simulation described in the following article:

[In preparation]

We used previously published datasets on wind-evoked escape behaviors of crickets as input data for the simulation. The data was collected in the following study:

Fukutomi, M. and Ogawa, H (2017). Crickets alter wind-elicited escape strategies depending on acoustic context. Scientific Reports, 7: 15158.
https://doi.org/10.1038/s41598-017-15276-x

## Prerequisites

- Unity 2021.3.6f1
- Git (for version control)

## Getting Started

1. Clone the repository:
   ```
   git clone https://github.com/YuukiKawabata-Lab/EscapeSimulation.git
   ```
2. Open Unity Hub and click "Add" to add the project.
3. Navigate to the cloned repository folder and select it.
4. Open the project using Unity version 2021.3.6f1.
5. Wait for Unity to import all assets and compile scripts.

## Usage

Follow these steps to use the simulation:

1. Open the scene: `Assets/Scenes/Menu.unity`
2. Press the Play button in the Unity Editor to run the simulation.
3. Click the "Start Analysis" button to begin the simulation.
4. When prompted, select the data folder you want to analyze.
5. When prompted, specify the name of the CSV file where you want to save the results.
6. The simulation will start and analyze the data in the specified folder.
7. Once all data has been analyzed, the simulation will stop and save the results in the specified CSV file.

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

## CSV File Format

The CSV files should have the following structure:

| Time.ms. | X.mm. | Y.mm. | Theta.deg. | Omega.deg.ms. | integrated.Theta.deg. | Trajectory.Length.mm. | Velocity.m.s. | stim |
|----------|-------|-------|------------|---------------|----------------------|----------------------|---------------|------|
| 0        | 0     | 0     | 0          | 0             | 0                    | 0                    | 0             | 1    |
| 5.00074  | 0.170 | -0.093| -0.07808   | -0.01561      | -0.07808             | 0.19407              | 0.03881       | 1    |
| ...      | ...   | ...   | ...        | ...           | ...                  | ...                  | ...           | ...  |

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.



