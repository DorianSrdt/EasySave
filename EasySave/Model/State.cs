using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;
using EasySave_V2.Service;
namespace EasySave_V2.Model
{
    class State
    {
        public string Name { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public string BackUpState { get; set; }
        public string CreationTime { get; set; }
        public string BackUpType { get; set; }
        public int TotalFilesToCopy { get; set; }
        public long TotalFilesSize { get; set; }
        public int FilesLeftToDo { get; set; }
        public float Progression { get; set; }
        private StateArray StateFile;
        private int Index { get; set; }
        //--- Constructor ---//
        public State(BackUp LinkedBackUp, int BackUpIndex = 7)
        {
            this.Name = LinkedBackUp.Name;
            this.SourcePath = LinkedBackUp.FileSource;
            this.DestinationPath = LinkedBackUp.DestinationPath;
            this.TotalFilesToCopy = LinkedBackUp.FilesNumber;
            this.TotalFilesSize = LinkedBackUp.TotalFileSize;
            this.CreationTime = DateTime.Now.ToString("g");
            this.BackUpType = LinkedBackUp is CompleteBackUp ? "Full BackUp" : "Differential BackUp";
            this.Index = -1;
            if (!File.Exists("../../../State.json"))
            {
                CreateStateFile();
            }
            this.StateFile = GetStateArray();

        }

        public State()
        {

        }

        //--- Method :  Create  State.json ---//
        private static void CreateStateFile()
        {
            string str = "{'BackUpList': []}";
            JObject json = JObject.Parse(str);
            string jsonIndented = JsonConvert.SerializeObject(json, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(@"../../../State.json", jsonIndented);
        }

        //--- Method : Find and Set Index if the already exist ---//
        private int FindIndex()
        {
            var ReadedJsonArray = this.StateFile.BackUpList.ToArray();

            for (int i = 0; i < ReadedJsonArray.Length; i++)
            {
                if (ReadedJsonArray[i].Name.ToString() == this.Name)
                {
                    break;
                }
            }

            return 1;
        }

        //--- Method : Update <state during Backup ---//
        public void DoState(int FilesLeftTooDo, bool Active)
        {
            this.FilesLeftToDo = FilesLeftTooDo;

            this.BackUpState = Active ? "ACTIVE" : "END";

            //this.Progression = (float)Math.Round((((float)this.TotalFilesToCopy - FilesLeftToDo) / this.TotalFilesToCopy) * 100, 0);

            SaveState();

        }

        private void SetIndexOfBackUp()
        {
            var List = ServiceBackUpList.GetBackUpListName();
            this.Index = List.IndexOf(this.Name);
        }

        //--- Method : Save Updated Backup ---//

        private void SaveState()
        {
            if (ServiceBackUpList.BackUpExist(this.Name))
            {
                if (this.Index == -1)
                {
                    this.SetIndexOfBackUp();
                }
                this.StateFile.BackUpList[this.Index] = this;
            }
            else
            {
                this.StateFile.BackUpList.Add(this);
            }
           
            var JsonString = JObject.Parse(JsonSerializer.Serialize(this.StateFile));
            string JsonToSave = JsonConvert.SerializeObject(JsonString, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(@"../../../State.json", JsonToSave);
        }

        //--- Method : Ask for the Index of the State ---// 
        private int PromptLocation()
        {
            Console.WriteLine(this.StateFile.BackUpList);
            int ind = 0;
            foreach (var BackUps in this.StateFile.BackUpList)
            {
                ind++;
                Console.WriteLine($"Emplacement n°{ind} : BackUp -> {BackUps.Name}");
            }
            Console.WriteLine("Choose back-up Location 1,2,3,4 or 5");
            int LocationChoice = Convert.ToInt32(Console.ReadLine());
            if (LocationChoice >= 0 && LocationChoice < 6)
            {
                return LocationChoice;
            }
            else return PromptLocation();

        }

        public StateArray GetStateArray()
        {
            var jsonString = File.ReadAllText("../../../State.json");

            return JsonSerializer.Deserialize<StateArray>(jsonString);
        }
    }

    class StateArray
    {
        public List<State> BackUpList { get; set; }
        public StateArray()
        {
            BackUpList = new List<State>();
        }
    }
}
