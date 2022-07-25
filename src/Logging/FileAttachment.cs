
namespace TestFramework.Logging
{
    public interface IAttachment
    {
        string Type { get; }
        string Title { get; }
        string Content { get; }
    }

    public class FileAttachment : IAttachment
    {
        public FileAttachment(string title, string fileName)
        {
            Type = "File";
            Title = title;
            Content = fileName;
        }

        public string Type { get; }
        public string Title { get; }
        public string Content { get; }
    }
}
