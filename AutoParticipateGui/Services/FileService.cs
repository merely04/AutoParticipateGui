using System.IO;
using System.IO.Compression;

namespace AutoParticipateGui.Services
{
    public class FileService
    {
        static FileService()
        {
            CreatePathIfNotExist(LogPath);
            CreatePathIfNotExist(TempPath);
            CreatePathIfNotExist(ScriptPath);
            CreatePathIfNotExist(UpdatePath);
        }

        private static void CreatePathIfNotExist(string path)
        {
            if (Directory.Exists(path) == false)
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch {}
            }
        }

        public static string TempPath => Path.Combine(Directory.GetCurrentDirectory(), "temp");
        
        public static string LogPath => Path.Combine(Directory.GetCurrentDirectory(), "temp");
        
        public static string ScriptPath => Path.Combine(LogPath, "script");
        
        public static string UpdatePath => Path.Combine(LogPath, "update");

        public static void UnzipFile(string fileName, string extractPath)
        {
            using (var archive = ZipFile.OpenRead(fileName))
            {
                foreach (var archiveEntry in archive.Entries)
                {
                    var fullPath = Path.Combine(extractPath, archiveEntry.FullName);
                    if (archiveEntry.Name == "")
                    {
                        Directory.CreateDirectory(fullPath);
                    }
                    else
                    {
                        archiveEntry.ExtractToFile(fullPath, true);
                    }
                }
            }
        }
    }
}