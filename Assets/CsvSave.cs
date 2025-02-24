using System;
using System.IO;
using System.Text;
using UnityEngine;
using FileManeger;

namespace CsvSave
{
    public class csvsaver
    {
        public VariableArray file_end;
        public VariableArray file_section;
        public VariableArray result;
        public VariableArray result_flame;
        public VariableArray Fid_setting;
        public VariableArray Scale_x;
        public VariableArray Scale_y;
        public VariableArray EndPoint;
        public VariableArray TerminalPoint;
        public VariableArray ExitDangerAreaFrame;

        public csvsaver()
        {
            file_end = new VariableArray();
            file_section = new VariableArray();
            result = new VariableArray();
            result_flame = new VariableArray();
            Fid_setting = new VariableArray();
            Scale_x = new VariableArray();
            Scale_y = new VariableArray();
            EndPoint = new VariableArray();
            TerminalPoint = new VariableArray();
            ExitDangerAreaFrame = new VariableArray();
            return;
        }

        // Write results to CSV file
        public void SaveFile(string SaveDataPath)
        {
            try
            {
                // Generate CSV file for data storage
                using (var save = new StreamWriter(SaveDataPath, false, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true)))
                {
                    // Generate header
                    save.WriteLine("File Name, Section, Result, Result_frame, FID, ScaleY, ScaleX, EndPoint, TerminalPoint, ExitDangerFrame,");
                    // Read cricket data sequentially
                    for (int i = 0; i < this.result.GetArray().Length - 1; i++)
                    {
                        // Add file name to CSV
                        save.WriteLine
                        (
                            "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                            this.file_end.GetArrayElement(i + 1), this.file_section.GetArrayElement(i + 1), this.result.GetArrayElement(i + 1),
                            this.result_flame.GetArrayElement(i + 1), this.Fid_setting.GetArrayElement(i + 1), this.Scale_y.GetArrayElement(i + 1),
                            this.Scale_x.GetArrayElement(i + 1), this.EndPoint.GetArrayElement(i + 1), this.TerminalPoint.GetArrayElement(i + 1), 
                            this.ExitDangerAreaFrame.GetArrayElement(i + 1)
                        );
                    }
                    save.Close();
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
            return;
        }
    }

    public class VariableArray
    {
        string[] arrayVal;

        public VariableArray()
        {
            this.arrayVal = new string[1] { null };
            return ;
        }

        public void AddArrayElement(string Element)
        {
            Array.Resize(ref this.arrayVal, this.arrayVal.Length + 1);
            this.arrayVal[this.arrayVal.Length - 1] = Element;
            return;
        }
        public string GetArrayElement(int element)
        {
            return this.arrayVal[element];
        }
        public string[] GetArray()
        {
            return this.arrayVal;
        }
    }
}