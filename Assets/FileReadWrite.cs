using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;
using System.IO;
using System.Text;
using System;
using System.Linq;


public class FileReadWrite : MonoBehaviour
{
    double de;
    // Creation of global variables
    public string Path; // Path of CSV
    public string[] CSVlist; // List of CSVs
    public int[] CSVlist_length;
    public string SavePath; // Creation of file to save
    public List<string> CSVname; // List of cricket CSV files
    GameObject Criket;
    GameObject Stimulus;
    GameObject State;
    GameObject Stimulus_M;
    GameObject DangerAria;
    float Initial_x;
    float Initial_y;
    float Flog_Initial_x;
    int i = 0; // Number of the file being read
    int j = 0; // Number of the line being read
    int k = 0; // Sequential number for analysis
    public bool NotFinish = true; // Not finished reading all files
    public float Speed = 90f; // cm/s
    public float FPS = 200f;
    public float Endpoint;// = -2.20f;//-2f;//-1.90f;
    float ED_plus;
    float Target = 2.45f * 0.142f;
    float Target_Frame; // Frame when stimulus movement ends
    float FID_1 = 5f; // Means 0.45*4 (FID:2.25)
    float FID;
    float position;
    float delta;
    float delta_Speed_Init;
    float Stimulus_Scale_x; // 0.25f; // Size of stimulus (cm)
    float Stimulus_Scale_y; // 0.25f;
    float STIMULUS_SCALE_INIT_X;
    float STIMULUS_SCALE_INIT_Y;
    float Stimulus_len_x; // Length to the center of the stimulus
    public string state = "NotCaught";
    string[] file_end = new string[1] { null };
    string[] file_section = new string[1] { null };
    string[] result = new string[1] { null };
    string[] result_flame = new string[1] { null };
    string[] Fid_setting = new string[1] { null };
    string[] Scale_x = new string[1] { null };
    string[] Scale_y = new string[1] { null };
    string[] EndPoint = new string[1] { null };
    string[] Stimulus_moving = new string[1] { null };
    List<List<float>> df;
    int df_length;
    //bool HERE = true;
    float delta_time;
    int loop;

    // Creation of global variables
    GameObject MainMenu;
    FileReadWrite methods;

    // Environment setup
    Encoding enc = Encoding.GetEncoding("UTF-8");

    // Start is called before the first frame update
    void Start()
    {
        // Calculate distance moved per frame
        delta_Speed_Init = Speed / FPS;
        // Get the stimulus object
        Stimulus = GameObject.Find("stimulus_c");
        this.STIMULUS_SCALE_INIT_X = this.Stimulus.GetComponent<Transform>().localScale.x;
        this.STIMULUS_SCALE_INIT_Y = this.Stimulus.GetComponent<Transform>().localScale.y;
        this.Stimulus_Scale_x = this.STIMULUS_SCALE_INIT_X;
        this.Stimulus_Scale_y = this.STIMULUS_SCALE_INIT_Y;

        ED_plus = 0f;
        Stimulus_len_x = StimulusLenX(Stimulus_Scale_x);
        Endpoint = -Stimulus_len_x + (delta_Speed_Init * ED_plus) - Target;

        Speed = Speed_Input.Set_speed;
        // Folder containing CSV files to read (can also read subfolders)
        Path = ClickCSV();
        // Create a list of files
        CSVlist = Directory.GetFiles(Path, "*.csv", SearchOption.AllDirectories);
        // Generate sequential numbers from the number of elements in CSVlist
        CSVlist_length = Enumerable.Range(0, CSVlist.Length - 1).ToArray();
        // Create a file to save (more precisely, specify the directory)
        SavePath = SaveDataAuto(Path);
        // Get the cricket object
        Criket = GameObject.Find("criket");
        Initial_x = Criket.GetComponent<Transform>().position.x;
        Initial_y = Criket.GetComponent<Transform>().position.y;
        // Get the stimulus object
        Stimulus.transform.position = new Vector3(((FID_1 * delta_Speed_Init * -1) - Stimulus_len_x) - Target, 0f, 0f);
        Flog_Initial_x = Stimulus.GetComponent<Transform>().position.x;
        Stimulus.transform.localScale = new Vector3((Stimulus_Scale_x * 0.578f), (Stimulus_Scale_y * 0.578f));
        // Set up the danger area object
        DangerAria = GameObject.Find("denger_aria");

        State = GameObject.Find("State");
        Stimulus_M = GameObject.Find("Stimulus_moving");
        Stimulus_M.GetComponent<Text>().text = "Moving";
        loop = 1;

        Target_Frame = FID_1 + ED_plus;
        // Calculate distance moved per frame
        delta = delta_Speed_Init;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        delta_time = Time.deltaTime;
        position = Stimulus.GetComponent<Transform>().position.x;
        if (NotFinish)
        {
            State.GetComponent<Text>().text = state;
            Stimulus_M.GetComponent<Text>().text = Math.Round(de, 2).ToString();
            if (Input.GetKeyDown(KeyCode.Q))
            {
                NotFinish = false;
            }
            // Load one set of data
            if (j == 0)
            {
                df = ReadCSV(CSVlist[i]);
                df_length = df[1].Count() - 1;
            }
            // Move the cricket and frog
            if (state == "NotCaught" && (transform.position.x < Endpoint) && (j < df_length))
            {
                State.GetComponent<Text>().text = state;
                Stimulus_M.GetComponent<Text>().text = Math.Round(de, 2).ToString();
                // Move the cricket
                if (state == "NotCaught")
                {
                    CriketDriver(Criket, j, df, Initial_x, Initial_y);
                }
                else
                {
                    delta = 0f;
                }
                // If frame is exceeded, don't transition to next frame
                if (j < df_length)
                {
                    j++;
                }
                // Move the frog
                position += delta;
                de = Endpoint - position;
                if (de > -0.005f)
                {
                    Stimulus.transform.position = new Vector3(position, 0f);
                }
                else
                {
                    delta = 0f;
                }
            }
            else
            // Reset cricket and stimulus after reading all rows
            {
                position += delta;
                de = Endpoint - position;
                if (this.loop > 10)
                {
                    string file = CSVlist[i];
                    // Remove directory part from filename
                    var fileS = file.Split('\\');
                    var file_end0 = fileS[fileS.Length - 1];
                    // Extract experimental condition part
                    var file_section0 = fileS[fileS.Length - 2];
                    i++; // Move to next data
                    k++;

                    loop = 1;

                    if (state == "Caught")
                    {
                        Array.Resize(ref result, result.Length + 1);
                        if (j<=Target_Frame)
                        {
                            result[result.Length - 1] = "Caught";
                        }
                        else
                        {
                            result[result.Length - 1] = "Caught_Error";
                        }
                        
                    }
                    else
                    {
                        Array.Resize(ref result, result.Length + 1);
                        if (j >= Target_Frame)
                        {
                            result[result.Length - 1] = "NotCaught";
                        }
                        else
                        {
                            result[result.Length - 1] = "NotCaught_Error";
                        }
                    }
                    state = "NotCaught";

                    // File name
                    Array.Resize(ref file_end, file_end.Length + 1);
                    file_end[file_end.Length - 1] = file_end0;
                    // Section name
                    Array.Resize(ref file_section, file_section.Length + 1);
                    file_section[file_section.Length - 1] = file_section0;
                    // Frame when caught
                    Array.Resize(ref result_flame, result_flame.Length + 1);
                    result_flame[result_flame.Length - 1] = j.ToString();
                    // FID setting
                    Array.Resize(ref Fid_setting, Fid_setting.Length + 1);
                    Fid_setting[Fid_setting.Length - 1] = (FID_1 * delta_Speed_Init * 1).ToString();
                    // Scale_Y setting
                    Array.Resize(ref Scale_y, Scale_y.Length + 1);
                    Scale_y[Scale_y.Length - 1] = (Stimulus_Scale_y).ToString();
                    // Scale_X setting
                    Array.Resize(ref Scale_x, Scale_x.Length + 1);
                    Scale_x[Scale_x.Length - 1] = (Stimulus_Scale_x).ToString();
                    // EndPoint setting
                    Array.Resize(ref EndPoint, EndPoint.Length + 1);
                    EndPoint[EndPoint.Length - 1] = Endpoint.ToString();
                    // Check if Stimulus movement speed has become 0
                    Array.Resize(ref Stimulus_moving, Stimulus_moving.Length + 1);
                    Stimulus_moving[Stimulus_moving.Length - 1] = Target_Frame.ToString();

                    // End analysis when all data has been read
                    if (i > CSVlist.Length - 1)
                    {
                        i = 0;
                        // Extend the endpoint of the stimulus (extend by 2 twice)
                        ED_plus += 2f;
                        Endpoint = -Stimulus_len_x + (delta_Speed_Init * ED_plus) - Target;
                        if (ED_plus > 5)
                        {
                            ED_plus = 0;
                            // Move to the next X scale
                            Stimulus_Scale_x += 1f;
                            Stimulus_len_x = StimulusLenX(Stimulus_Scale_x);
                            Endpoint = -Stimulus_len_x + (delta_Speed_Init * ED_plus) - Target;
                        }
                        // If X scale is 1 or more, move to the next Y scale and reset X scale
                        if (Stimulus_Scale_x > 1.0f)//4.25f)
                        {
                            this.Stimulus_Scale_x = this.STIMULUS_SCALE_INIT_X;
                            Stimulus_len_x = StimulusLenX(Stimulus_Scale_x);
                            Endpoint = -Stimulus_len_x + (delta_Speed_Init * ED_plus) - Target;
                            Stimulus_Scale_y += 1f;
                        }
                        // If Y scale is 1 or more, move to the next FID and reset Y scale
                        if (Stimulus_Scale_y > 1.0f)//2.25f)
                        {
                            this.Stimulus_Scale_y = this.STIMULUS_SCALE_INIT_Y;
                            // Move to the next FID
                            FID_1 += 3;
                        }
                        //i++;
                        Stimulus.transform.position = new Vector3(((FID_1 * delta_Speed_Init * -1) - Stimulus_len_x) - Target, 0f, 0f);
                        Flog_Initial_x = Stimulus.GetComponent<Transform>().position.x;
                        Stimulus.transform.localScale = new Vector3((Stimulus_Scale_x * 0.578f), (Stimulus_Scale_y * 0.578f));
                        if (FID_1 > 13)// FID starts from 0.45cm*5 and increases to 0.45cm*8, 0.45cm*11
                        {
                            NotFinish = false;
                        }
                    }
                    // Reset cricket's position
                    Criket.transform.position = new Vector3(Initial_x, Initial_y, 0f);
                    Criket.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    // Reset frog's position
                    Stimulus.transform.position = new Vector3(Flog_Initial_x, 0f, 0f);
                    Target_Frame = FID_1 + ED_plus;
                    position = Flog_Initial_x;
                    delta = Speed / FPS;
                    //HERE = true;
                    j = 0; // Reset row number
                }
                this.loop += 1;
            }
        }
        else
        {
            Debug.Log(result[result.Length - 1]+" "+result.Length);
            SaveFile(result, result_flame);
        }
        
    }

    // Write results to CSV file
    public void SaveFile(string[] result, string[] result_flame)
    {
        try
        {
            // Generate CSV file for data storage
            using (var save = new StreamWriter(SavePath, false, enc))
            {
                // Generate header
                save.WriteLine("File Name, Section, Result, Result_frame, FID, ScaleY,ScaleX,EndPoint,Stimulus_Moving,");
                // Read cricket data sequentially
                for (int i = 0; i < result.Length - 2; i++)
                {
                    // Add file name to CSV
                    save.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                        file_end[i + 1], file_section[i + 1], result[i + 1], result_flame[i + 1], Fid_setting[i + 1], Scale_y[i + 1], Scale_x[i + 1], EndPoint[i + 1], Stimulus_moving[i + 1]);
                }
                save.Close();
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    // Select folder containing CSV files
    public string ClickCSV()
    {
        // Select folder
        var paths = StandaloneFileBrowser.OpenFolderPanel("Choose a folder", "", false);
        // Assign as variable (from array)
        string cricket_paths = paths[0];
        return cricket_paths;
    }

    // Select file containing CSV files
    public string[] ClickCSVFile()
    {
        // Select folder
        var extensions = new[] {
        new ExtensionFilter("csv", "csv" ),
        new ExtensionFilter("All Files", "*" ),
    };
        var paths = StandaloneFileBrowser.OpenFilePanel("Choose CSV files", "", extensions, true);
        // Return as variable (from array)
        return paths;
    }

    // Automatically select the folder one level up from the CSV file folder as the folder to save data
    public string SaveDataAuto(string CSV_path)
    {
        // Get current date and time
        DateTime dt = DateTime.Now;
        // Set save file name
        var data_csv = dt.ToString("yyyyMMddHHmmss");
        // Get CSV file location
        var dir = CSV_path.Substring(0, CSV_path.LastIndexOf(@"\") + 1);
        // Select file
        var paths = StandaloneFileBrowser.SaveFilePanel("Save File (Don't change this directory)", dir, data_csv + "OutPutData", new[] { new ExtensionFilter("Text Files", "csv", "txt"), new ExtensionFilter("All Files", "*") });
        // Convert to FileReadWrite type string
        Path = paths.Replace(@"\", @"\\");
        return Path;
    }

    // Create a list equivalent to a dataframe from CSV file
    public List<List<float>> ReadCSV(string File)
    {
        // Read the file
        var csv = new StreamReader(File);
        // Skip header
        string first = csv.ReadLine();
        // Create lists
        var x_value = new List<string>();
        var y_value = new List<string>();
        var deg_value = new List<string>();
        // Repeat until end of file
        while (!csv.EndOfStream)
        {
            // Read Nth line as a string on Nth iteration
            var l1 = csv.ReadLine();
            // Convert to array
            string[] l2 = l1.Split(',');
            // Add to lists
            x_value.Add(l2[1]);
            y_value.Add(l2[2]);
            deg_value.Add(l2[5]);
        }
        // Convert to float, [data type][list number within data]
        var dataframe = new List<List<float>>()
    {
        x_value.ConvertAll(float.Parse),  // X coordinate
        y_value.ConvertAll(float.Parse),  // Y coordinate
        deg_value.ConvertAll(float.Parse) // Yaw angle
    };
        return dataframe;
    }

    // Control the cricket
    public void CriketDriver(GameObject gameObject, int rownumber, List<List<float>> coordinations, float initial_x, float initial_y)
    {
        // Set xy coordinates
        // Unity's 1 = 1cm = 10mm
        float xv = coordinations[0][rownumber] * 0.1f + initial_x;
        float yv = coordinations[1][rownumber] * 0.1f + initial_y;
        // Move the cricket
        gameObject.transform.position = new Vector3(xv, yv, 0f);
        // Rotate the cricket
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, coordinations[2][rownumber]);
    }

    private float StimulusLenX(float scaleX)
    {
        float lenX = scaleX/2;
        return lenX;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        state = "Caught";
    }
}

