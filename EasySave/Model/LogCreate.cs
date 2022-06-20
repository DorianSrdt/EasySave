using System;
using System.IO;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Xml.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using EasySave_V2.Service;

namespace EasySave_V2.Model
{
    class LogCreate
    {
        private BackUp BackUp;
        private string Date;
        private LogArray ListLog;
        private static Mutex mut = new Mutex();

        public LogCreate(BackUp Data)
        {
            var Date = DateTime.Now.ToString("yyyy-MM-dd");
            BackUp = Data;
            this.Date = Date;
            if (GlobalSettings.LogExtension == "json")
            {
                if (!File.Exists($"../../../Log/JSON/{Date}.json"))
                {
                    CreateJSONLogFile();
                }
                ListLog = GetJSONLogList();
            }
            else
            {
                if (!File.Exists($"../../../Log/XML/{Date}.xml"))
                {
                    CreateXMLLogFile();
                }
            }
        }
        public void GenLog(string FileSource, string DestinationFile, long FileSize, long TransferTime)
        {

            if (GlobalSettings.LogExtension == "json")
            {
                Log DataLog = new Log
                {
                    Date = DateTime.Now.ToString("G"),
                    BackUpName = BackUp.Name,
                    FileSource = FileSource,
                    FileTarget = DestinationFile,
                    FileSize = FileSize,
                    FileTransferTime = TransferTime,
                };
                Console.WriteLine(this.ListLog.LogList);
                this.ListLog.LogList.Add(DataLog);
                var JsonString = JObject.Parse(JsonSerializer.Serialize(this.ListLog));
                string JsonToSave = JsonConvert.SerializeObject(JsonString, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(@$"../../../Log/JSON/{this.Date}.json", JsonToSave);
            }
            else
            {
                var doc = XDocument.Load($"../../../Log/XML/{Date}.xml");
                XElement Log = doc.Root;
                Log.Add(new XElement("Log",
                            new XElement("Date", DateTime.Now.ToString("G")),
                            new XElement("BackUpName", this.BackUp.Name),
                            new XElement("FileSource", FileSource),
                            new XElement("FileTarget", DestinationFile),
                            new XElement("FileSize", FileSize),
                            new XElement("FileTransferTime", TransferTime)
                            ));
                doc.Save($"../../../Log/XML/{Date}.xml");
            }


        }

        private void CreateJSONLogFile()
        {
            string str = "{'LogList': []}";
            JObject json = JObject.Parse(str);
            string jsonIndented = JsonConvert.SerializeObject(json, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText($@"../../../Log/JSON/{this.Date}.json", jsonIndented);
        }

        private void CreateXMLLogFile()
        {
            new XDocument(
                new XElement("root")
            )
            .Save($"../../../Log/XML/{this.Date}.xml");
        }

        private LogArray GetJSONLogList()
        {
            string jsonString = File.ReadAllText($"../../../Log/JSON/{this.Date}.json");

            return JsonSerializer.Deserialize<LogArray>(jsonString);
        }
    }
}
