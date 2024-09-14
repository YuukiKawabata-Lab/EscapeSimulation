using System;
using SFB;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace FileManeger
{
	public class CsvFileIO
	{
        public string ImportFolderDir;
        public readonly string[] ImportCSVList;
        public readonly int[] SequImportCSV;
        public string SaveDataPath;

		public CsvFileIO()
		{
            this.GetImportFolder();
            // Create a list of files
            this.ImportCSVList = Directory.GetFiles(this.ImportFolderDir, "*.csv", SearchOption.AllDirectories);
            // Generate a sequence of numbers based on the number of elements in CSVlist
            this.SequImportCSV = Enumerable.Range(0, this.ImportCSVList.Length - 1).ToArray();

            this.GetSaveDataPath();
		}

        //Select import data csv file's folder
        void GetImportFolder()
        {
            //Select a folder
            var paths = StandaloneFileBrowser.OpenFolderPanel("Chose a folder", "", false);
            //set a first array value into a class variables
            this.ImportFolderDir = paths[0];
        }

        //Set preferences to save a csv file 
        void GetSaveDataPath()
        {
            //Get current time
            DateTime dt = DateTime.Now;
            string data_csv = dt.ToString("yyyyMMddHHmmss");
            //Suggest file path to save data
            string dir = this.ImportFolderDir.Substring(0, this.ImportFolderDir.LastIndexOf(@"\") + 1);
            //Get data file path
            var paths = StandaloneFileBrowser.SaveFilePanel("Save File", dir, data_csv + "OutPutData", new[] { new ExtensionFilter("Text Files", "csv", "txt"), new ExtensionFilter("All Files", "*") });
            //normalize the path
            this.SaveDataPath = paths.Replace(@"\", @"\\");
        }

        public string GetFilename (string File)
        {
            string[] filepathSplit = File.Split('\\');
            return filepathSplit[filepathSplit.Length - 1]; 
        }

        public string GetSectionname (string File)
        {
            string[] filepathSplit = File.Split('\\');
            return filepathSplit[filepathSplit.Length - 2];
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
                x_value.ConvertAll(float.Parse),    // X coordinate
                y_value.ConvertAll(float.Parse),    // Y coordinate
                deg_value.ConvertAll(float.Parse)   // Yaw angle
            };
            return dataframe;
        }
    }
}
