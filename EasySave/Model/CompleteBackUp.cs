using EasySave_V2.Service;
using System.Diagnostics;
using System.IO;

namespace EasySave_V2.Model
{
    public class CompleteBackUp : BackUp
    {
        public CompleteBackUp(string Name, string FileSource, string DestinationPath) : base(Name, FileSource, DestinationPath)
        {
            if (FileSource != "")
            {
                TotalFileSize = DirSize(new DirectoryInfo(FileSource));
            }
            
        }

        public override void MakeBackup()
        {
            var SourcePath = this.FileSource;
            var TargetPath = this.DestinationPath;
            if (FileSource == "" || DestinationPath == "")
            {
                return;
            }
            
            ///We delete all the files and folders that could be present due to an old backup
            DirectoryInfo DirectoryToClean = new(TargetPath);
            LogCreate NewLog = new(this);
            foreach (FileInfo file in DirectoryToClean.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in DirectoryToClean.GetDirectories())
            {
                dir.Delete(true);
            }

            //Now we recreate the folder structure of the source in the destination
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(SourcePath, TargetPath));
            }

            //int FiletoCopy = Directory.GetFiles(SourcePath, "*", SearchOption.AllDirectories).Length;

            this.FilesNumber = Directory.GetFiles(SourcePath, "*", SearchOption.AllDirectories).Length;

            int FileCompleted = 0;
            State NewSate = new(this);
            NewSate.DoState(this.FilesNumber, true);
            //Finally we copy all the files and rename them with the same names as in the source folder
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories))
            {

                var FileExtension = newPath.Substring(newPath.LastIndexOf('.') - 1);
                var TimeTransfer = new Stopwatch();
                TimeTransfer.Start();
                if (GlobalSettings.ExtensionToCrypt.Contains(FileExtension))
                {
                    Process.Start(@"../../../Assets/CryptoSoft.exe", @$"{SourcePath} {TargetPath}");

                }
                else
                {
                    File.Copy(newPath, newPath.Replace(SourcePath, TargetPath), true);
                }
                
                FileCompleted++;
                NewSate.DoState(this.FilesNumber - FileCompleted, true);

                TimeTransfer.Stop();
                NewLog.GenLog(newPath, newPath.Replace(SourcePath, TargetPath), new FileInfo(newPath).Length, TimeTransfer.ElapsedMilliseconds);

            }

            NewSate.DoState(0, false);
        }

        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }
    }
}

