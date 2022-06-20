using System.Collections.Generic;

namespace EasySave_V2.Model
{
    class Log
    {
        //private BackUp
        public string BackUpName { get; set; }
        public string FileSource { get; set; }
        public string FileTarget { get; set; }
        public long FileSize { get; set; }
        public long FileTransferTime { get; set; }
        public string Date { get; set; }

        private static string Json { get; set; }
    }
    class LogArray
    {
        public List<Log> LogList { get; set; }
    }
}