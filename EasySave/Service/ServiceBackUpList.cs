using EasySave_V2.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace EasySave_V2.Service
{
    class ServiceBackUpList
    {
        private static Mutex mut = new Mutex();
        public static List<string> GetBackUpListName()
        {
            mut.WaitOne();
            var jsonString = File.ReadAllText("../../../State.json");
            

            var Array = System.Text.Json.JsonSerializer.Deserialize<StateArray>(jsonString);
            mut.ReleaseMutex();
            List<string> ListBackUpName = new List<string>();
            foreach (var BackUpList in Array.BackUpList)
            {
                ListBackUpName.Add(BackUpList.Name);
            }
            return ListBackUpName;
            
        }

        public static List<State> GetBackUpList()
        {
            var jsonString = File.ReadAllText("../../../State.json");

            return System.Text.Json.JsonSerializer.Deserialize<StateArray>(jsonString).BackUpList;

        }

        public static bool BackUpExist(string Name)
        {

            var List = GetBackUpListName();

            return List.Contains(Name, StringComparer.OrdinalIgnoreCase) ? true : false;

        }

        public static void DeleteBackUp(int IndexBackUpToDelete)
        {
            var jsonString = File.ReadAllText("../../../State.json");

            var Array = System.Text.Json.JsonSerializer.Deserialize<StateArray>(jsonString);
            var OutputPath = Array.BackUpList[IndexBackUpToDelete].DestinationPath;
            Directory.Delete(OutputPath);

            Array.BackUpList[IndexBackUpToDelete].Name = "";
            Array.BackUpList[IndexBackUpToDelete].BackUpState = "END";
            Array.BackUpList[IndexBackUpToDelete].DestinationPath = "";
            Array.BackUpList[IndexBackUpToDelete].FilesLeftToDo = 0;
            Array.BackUpList[IndexBackUpToDelete].Progression = 0;
            Array.BackUpList[IndexBackUpToDelete].SourcePath = "";
            Array.BackUpList[IndexBackUpToDelete].TotalFilesSize = 0;
            Array.BackUpList[IndexBackUpToDelete].TotalFilesToCopy = 0;


            var JsonString = JObject.Parse(System.Text.Json.JsonSerializer.Serialize(Array));
            string JsonToSave = JsonConvert.SerializeObject(JsonString, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(@"../../../State.json", JsonToSave);
        }
    }
}
