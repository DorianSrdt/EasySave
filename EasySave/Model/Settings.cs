using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_V2.Model
{
    class Settings
    {
        public string Language { get; set; }
        public string LogExtension { get; set; }
        public List<string> ExtensionToCrypt { get; set; }
        public List<string> BusinessSoftwareFollow { get; set; }

        public Settings(string lang, string Log, List<string> Ext, List<string> BS)
        {
            Language = lang;
            LogExtension = Log;
            ExtensionToCrypt = Ext;
            BusinessSoftwareFollow = BS;
        }

        public void UpdateSettingsJson()
        {
            var JsonString = JObject.Parse(System.Text.Json.JsonSerializer.Serialize(this));
            string JsonToSave = JsonConvert.SerializeObject(JsonString, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(@"../../../Settings.json", JsonToSave);
        }
    }
}
