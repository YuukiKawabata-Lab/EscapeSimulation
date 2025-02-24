using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using System.Linq;
using pray;
using FileManeger;
using stimulus;
using CsvSave;
using DangerArea;

public class MainScene : MonoBehaviour
{
    //Global variables  
    public List<string> CSVname; // List of cricket CSV files
    int i = 0; // Number of the file currently being read
    int j = 0; // Number of the line currently being read
    int k = 0; // Sequential number for analysis
    public bool NotFinish = true; // Not finished reading all files
    public string state = "NotCaught";
    public static bool StimulusMoving;
    int loop;
    List<List<float>> df;
    int df_length;
    public static bool PreyMoving;

    GameObject MainMenu;
    CricketObj cricket;
    CsvFileIO FileIO;
    Stimulus stimulus;
    csvsaver csvsave;
    FileReadWrite methods;
    public static StatusDangerArea dangerArea;
    int? ExitDangerFrame = null;
    //bool session_continued = true;

    // Environment settings
    Encoding enc = Encoding.GetEncoding("UTF-8"); // Specify encoding

    // Start is called before the first frame update
    void Start()
    {
        cricket = new pray.CricketObj();
        FileIO = new FileManeger.CsvFileIO();
        stimulus = new stimulus.Stimulus(StimulusSpeed: Speed_Input.Set_speed);
        csvsave = new CsvSave.csvsaver();

        dangerArea = new DangerArea.StatusDangerArea();
        dangerArea.UpdateTransform(stimulus.TerminalPoint, 0f, stimulus.FlogTongScaleX, stimulus.StartStimulusPosition_X);

        loop = 1;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (stimulus.NotFinishedSimulation) // End when all FIDs have been tried
        {
            cricket.UpdateCricketStatus(state);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                stimulus.NotFinishedSimulation = false;
            }
            // Read one data entry
            if (j== 0)
            {
                df = FileIO.ReadCSV(FileIO.ImportCSVList[i]);
                Debug.Log("Enter file:" + FileIO.ImportCSVList[i]);
                df_length = df[1].Count() - 1;
                PreyMoving = true;
                StimulusMoving = true;
            }

            // Move a cricket and a frog
            if (j < df_length || StimulusMoving)
            {
                Debug.Log("StimulusMoving ("+ StimulusMoving +") is True");

                Debug.Log("j <= df_length is True and J is " + j);
                cricket.UpdateCricketStatus(state);
                
                //Move a cricket
                if (j <= df_length)
                {
                    cricket.CriketMoving(j, df);
                }
                else
                {
                    PreyMoving = false;
                }
                //Move a flog
                StimulusMoving = stimulus.StimulusMoving();
                if (dangerArea.State == "Out" && this.ExitDangerFrame is null)
                {
                    this.ExitDangerFrame = j;
                    Debug.Log("Exit: Frame " + j);
                }
 
                Debug.Log(state + " " + j + " / " + df_length + ": FID " + stimulus.FidDistance + ": Scale " + stimulus.FlogTongScaleX + ": TerminalPoint " + stimulus.TerminalPointEdge + ": TerminalPointDenger " + dangerArea.GetScale() + ": File " + FileIO.GetFilename(FileIO.ImportCSVList[i]) + ": Section " + FileIO.GetSectionname(FileIO.ImportCSVList[i]));
                j++;

            }
            else
            // Reset a cricket and a frog if all lines of csv have been read, or if the frog's action is finished
            {
                Debug.Log("StimulusMoving is False");
                Debug.Log("Loop: " + this.loop);
                if (this.loop > 10)
                {   
                    this.loop = 1;

                    if (state == "Caught")
                    {
                        if (StimulusMoving)
                        {
                            csvsave.result.AddArrayElement("Caught_Error");
                        }
                        else
                        {
                            csvsave.result.AddArrayElement("Caught");
                        }
                        Debug.Log("Catch!");
                    }
                    if (state == "NotCaught")
                    {
                        if (StimulusMoving)
                        {
                            csvsave.result.AddArrayElement("NotCaught_Error");
                        }
                        else
                        {
                            csvsave.result.AddArrayElement("NotCaught");
                        }
                        Debug.Log("Escaped!");
                    }
                    state = "NotCaught";

                    // File name
                    csvsave.file_end.AddArrayElement(FileIO.GetFilename(FileIO.ImportCSVList[i]));
                    // Section name
                    csvsave.file_section.AddArrayElement(FileIO.GetSectionname(FileIO.ImportCSVList[i]));
                    // Frame at which predation occurred
                    csvsave.result_flame.AddArrayElement(j.ToString());
                    // FID setting
                    csvsave.Fid_setting.AddArrayElement(stimulus.FidDistance.ToString());
                    // Scale_Y setting
                    csvsave.Scale_y.AddArrayElement(stimulus.FlogTongScaleY.ToString());
                    // Scale_X setting
                    csvsave.Scale_x.AddArrayElement(stimulus.FlogTongScaleX.ToString());
                    // EndPoint setting
                    csvsave.EndPoint.AddArrayElement(stimulus.TerminalPoint.ToString());
                    // TerminalPointsetting
                    csvsave.TerminalPoint.AddArrayElement(stimulus.TerminalPointEdge.ToString());
                    // Frame at which the danger area was exited
                    csvsave.ExitDangerAreaFrame.AddArrayElement(this.ExitDangerFrame.ToString());

                    i++;
                    k++;
                    // End analysis when all data has been read
                    if (i >= FileIO.ImportCSVList.Length)
                    {
                        i = 0;
                        // Change the stimulus endpoint, size and FID
                        stimulus.SetNextItr();
                    }
                    stimulus.SetStartPosition();
                    //this.StimulusMoving = true;
                    // Reset cricket position
                    cricket.ResetCricket();
                    // Reset danger area information
                    dangerArea.UpdateState2In();
                    dangerArea.UpdateTransform(stimulus.TerminalPoint, 0f, stimulus.FlogTongScaleX, stimulus.StartStimulusPosition_X);
                    //session_continued = true;
                    this.ExitDangerFrame = null;
                    j = 0; // Reset line number
                }
                this.loop += 1; 
            }
        }
        else
        {
            Debug.Log(csvsave.result.GetArrayElement(i)+" "+csvsave.result.GetArray().Length);
            csvsave.SaveFile(FileIO.SaveDataPath);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "cricket")
        {
            GameObject StimulusObj_ = GameObject.Find("stimulus_c");
            Color32 rgbcol = StimulusObj_.GetComponent<SpriteRenderer>().color;

            Debug.Log("Enter: " + other.gameObject.tag + "; Stimulus:" +StimulusMoving + "; Color:" + rgbcol.g);
            if (StimulusMoving && rgbcol.g == 0)
            {
                state = "Caught";
                Debug.Log("Status was updated to Caught");
            }
        }
    }
}

