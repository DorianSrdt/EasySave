using System.IO;

namespace EasySave_V2.Model
{
    class FileCompare : System.Collections.Generic.IEqualityComparer<FileInfo>
    {
        public FileCompare() { }

        public bool Equals(FileInfo File1, FileInfo File2)
        {
            return (File1.Name == File2.Name &&
                    File1.Length == File2.Length &&
                    File1.LastWriteTime == File2.LastWriteTime);
        }
        public int GetHashCode(FileInfo fi)
        {
            string s = $"{fi.Name}{fi.Length}";
            return s.GetHashCode();
        }
    }
}