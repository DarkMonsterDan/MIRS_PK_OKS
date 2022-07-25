using System.IO;

namespace TestFramework.IO
{
    public interface IFileService
    {
        string WorkPath { get; }
        IFileService GetDirectory(string path);
        string[] Files { get; }
        string[] Directories { get; }
        string CreateFile(string fileName);
        FileStream OpenWrite(string fileName);
        void SaveFile(string fileName, byte[] bytes);
    }

    public class FileService : IFileService
    {
        private int fileCount = 0;

        public FileService(string path)
        {
            WorkPath = path;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public string WorkPath { get; }

        public IFileService GetDirectory(string path)
        {
            if (!Path.IsPathRooted(path))
                return new FileService(Path.Combine(WorkPath, path));

            return new FileService(path);
        }

        public string[] Files => Directory.GetFiles(WorkPath);
        public string[] Directories => Directory.GetDirectories(WorkPath);
        public bool DirectoryExists => Directory.Exists(WorkPath);

        public string CreateFile(string fileName)
        {
            var newFileName = fileName;
            var extension = Path.GetExtension(fileName);
            var name = Path.GetFileNameWithoutExtension(fileName);
            while (File.Exists(Path.Combine(WorkPath, newFileName)))
            {
                fileCount++;                
                newFileName = $"{name}_{fileCount}{extension}";
            }
            var path = Path.Combine(WorkPath, newFileName);
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            using (File.Create(path))
            {
            }
            return path;
        }

        public FileStream OpenWrite(string fileName)
        {
            var filePath = Path.Combine(WorkPath, fileName);
            if (!File.Exists(filePath))
                filePath = CreateFile(fileName);

            return File.OpenWrite(fileName);
        }

        public void SaveFile(string fileName, byte[] bytes)
        {
            using (var fileStream = OpenWrite(fileName))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
