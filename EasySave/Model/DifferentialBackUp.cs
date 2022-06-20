using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace EasySave_V2.Model
{
    class DifferentialBackUp : BackUp
    {
        public DifferentialBackUp(string Name, string FileSource, string DestinationPath) : base(Name, FileSource, DestinationPath)
        {
            
            TotalFileSize = 0;
        }

        public override void MakeBackup()
        {
            var SourcePath = this.FileSource;
            var TargetPath = this.DestinationPath;

            DirectoryInfo SourceDirectory = new(SourcePath);
            DirectoryInfo TargetDirectory = new(TargetPath);

            // Take a snapshot of the file system.  
            IEnumerable<FileInfo> SourceFileList = SourceDirectory.GetFiles("*.*", SearchOption.AllDirectories);
            IEnumerable<FileInfo> TargetFileList = TargetDirectory.GetFiles("*.*", SearchOption.AllDirectories);

            FileCompare NewFileCompare = new();
            LogCreate NewLog = new(this);

            bool areIdentical = SourceFileList.SequenceEqual(TargetFileList, NewFileCompare);

            if (areIdentical == true)
            {
                Console.WriteLine("the two folders are the same");
                return;
            }
            //State NewSate = new(this);

            var DeletedFilesList = (from file in TargetFileList select file).Except(SourceFileList, NewFileCompare);

            int NumberFiles = 0;

            foreach (var DeletedFile in DeletedFilesList)
            {
                Console.WriteLine(DeletedFile.FullName);
                DeletedFile.Delete();
            }

            var UpdatedFilesList = (from file in SourceFileList select file).Except(TargetFileList, NewFileCompare);
            long FilesSize = 0;
            foreach (var UpdatedFIle in UpdatedFilesList)
            {
                FilesSize += UpdatedFIle.Length;
                NumberFiles++;
            }

            this.FilesNumber = NumberFiles;
            this.TotalFileSize = FilesSize;

            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(SourcePath, TargetPath));
            }

            int FileCompleted = 0;
            //NewSate.DoState(GetFilesNumber(), true);
            foreach (var UpdatedFIle in UpdatedFilesList)
            {
                var TimeTransfer = new Stopwatch();
                TimeTransfer.Start();

                File.Copy(UpdatedFIle.FullName, UpdatedFIle.FullName.Replace(SourcePath, TargetPath), true);
                FileCompleted++;
                //NewSate.DoState(GetFilesNumber() - FileCompleted, true);

                TimeTransfer.Stop();
                NewLog.GenLog(UpdatedFIle.FullName, UpdatedFIle.FullName.Replace(SourcePath, TargetPath), new FileInfo(UpdatedFIle.FullName).Length, TimeTransfer.ElapsedMilliseconds);

            }

            //NewSate.DoState(0, false);
        }
    }
}