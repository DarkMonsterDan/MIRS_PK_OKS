using System.IO;

namespace TestFramework.IO
{
    public interface IFile
    {
        string FileName { get; }
    }
    public interface ITextFile : IFile
    {
        void AppendLine(string text);
        void Append(string text);
    }

    public class TextFile : ITextFile
    {
        public TextFile(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; }

        public void Append(string text)
        {
            using (var fileStream = new FileStream(FileName, FileMode.Append))
            using (var writer = new StreamWriter(fileStream))
            {
                writer.Write(text);
            }
        }

        public void AppendLine(string text)
        {
            using (var fileStream = new FileStream(FileName, FileMode.Append))
            using (var writer = new StreamWriter(fileStream))
            {
                writer.WriteLine(text);
            }
        }
    }
}
