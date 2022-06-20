using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EasySave_V2.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasySave_V2.Service
{
    class GlobalSettings
    {
        public static string Language { get; set; }
        public static string LogExtension { get; set; }
        public static List<string> ExtensionToCrypt { get; set; }
        public static List<string> BusinessSoftwareFollow { get; set; }

    }
}
