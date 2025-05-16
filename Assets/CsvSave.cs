using System;
using System.IO;
using System.Text;
using UnityEngine;
using FileManeger;

namespace CsvSave
{
    public class csvsaver
    {
        private string file_end;
        private string file_section;
        private string result;
        private string result_flame;
        private string Fid_setting;
        private string Scale_x;
        private string Scale_y;
        private string EndPoint;
        private string TerminalPoint;
        private string ExitDangerAreaFrame;
        private bool isFirstWrite = true;

        public csvsaver()
        {
            return;
        }

        public void SetResult(string fileEnd, string fileSection, string result, string resultFlame, 
            string fidSetting, string scaleX, string scaleY, string endPoint, string terminalPoint, string exitDangerFrame)
        {
            this.file_end = fileEnd;
            this.file_section = fileSection;
            this.result = result;
            this.result_flame = resultFlame;
            this.Fid_setting = fidSetting;
            this.Scale_x = scaleX;
            this.Scale_y = scaleY;
            this.EndPoint = endPoint;
            this.TerminalPoint = terminalPoint;
            this.ExitDangerAreaFrame = exitDangerFrame;
        }

        // Write results to CSV file
        public void SaveFile(string SaveDataPath)
        {
            try
            {
                // Generate CSV file for data storage
                using (var save = new StreamWriter(SaveDataPath, !isFirstWrite, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true)))
                {
                    // Generate header only on first write
                    if (isFirstWrite)
                    {
                        save.WriteLine("File Name, Section, Result, Result_frame, FID, ScaleY, ScaleX, EndPoint, TerminalPoint, ExitDangerFrame,");
                        isFirstWrite = false;
                    }

                    // Write current iteration result
                    save.WriteLine(
                        "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                        this.file_end, this.file_section, this.result,
                        this.result_flame, this.Fid_setting, this.Scale_y,
                        this.Scale_x, this.EndPoint, this.TerminalPoint, 
                        this.ExitDangerAreaFrame
                    );
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