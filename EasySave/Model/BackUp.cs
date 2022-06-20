using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EasySave_V2.Model
{
    public abstract class BackUp
    {
        public string Name { get; set; }
        public string FileSource { get; set; }
        public string DestinationPath { get; set; }
        public long TotalFileSize { get; set; }
        public int FilesNumber { get; set; }

        public BackUp(string Name, string FileSource, string DestinationPath)
        {
            this.Name = Name;
            this.FileSource = FileSource.EndsWith(@"\") ? FileSource : FileSource + @"\";
            this.DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";
        }

        public abstract void MakeBackup();

        public static void DeleteBackUp(string BAckUpName)
        {
            var jsonString = File.ReadAllText("../../../State.json");

            var Array = System.Text.Json.JsonSerializer.Deserialize<StateArray>(jsonString);

            State BackUptoDelete = null;
            foreach (var BackUpList in Array.BackUpList)
            {
                if (BackUpList.Name == BAckUpName)
                {
                    BackUptoDelete = BackUpList;
                };
            }
            if (BackUptoDelete is not null)
            {
                Array.BackUpList.Remove(BackUptoDelete);

                Directory.Delete(BackUptoDelete.DestinationPath);
            }

            var JsonString = JObject.Parse(System.Text.Json.JsonSerializer.Serialize(Array));
            string JsonToSave = JsonConvert.SerializeObject(JsonString, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(@"../../../State.json", JsonToSave);
        }
    }
}
