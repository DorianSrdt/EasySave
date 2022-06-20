using EasySave_V2.Model;
using EasySave_V2.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace EasySave_V2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStartUp(object sender, StartupEventArgs e)
        {

            StreamReader Reader = new StreamReader("../../../Settings.json");
            string ReadedJson = Reader.ReadToEnd();
            var ReadedSettings = JObject.Parse(ReadedJson);

            GlobalSettings.ExtensionToCrypt = ReadedSettings["ExtensionToCrypt"].ToObject<List<string>>();
            GlobalSettings.BusinessSoftwareFollow = ReadedSettings["BusinessSoftwareFollow"].ToObject<List<string>>();
            GlobalSettings.Language = ReadedSettings["Language"].ToString();
            GlobalSettings.LogExtension = ReadedSettings["LogExtension"].ToString();

            string[] LanguageList = Directory.GetFiles(@"../../../Translate/", "*.json");

            GlobalLangParam.ListLanguage = new List<string>();

            foreach (var Languages in LanguageList)
            {
                var Lang = Languages.Substring(Languages.LastIndexOf(@"/") + 1).Replace(".json", "");
                GlobalLangParam.ListLanguage.Add(Lang);
            }

            var LangDictionary = File.ReadAllText($"../../../Translate/{GlobalSettings.Language}.json");

            GlobalLangParam.LangageDictionary = JsonConvert.DeserializeObject<Language>(LangDictionary);
        }
    }
}
