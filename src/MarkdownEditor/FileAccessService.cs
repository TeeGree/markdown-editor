namespace MarkdownEditor
{
    public interface IFileAccessService
    {
        string GetMarkdown();
    }
    public sealed class FileAccessService : IFileAccessService
    {
        private const string MarkdownFileName = "activeMarkdown.md";

        public string GetMarkdown()
        {
            string installLocationFileFilePath = Path.Combine(AppContext.BaseDirectory, MarkdownFileName);
            if (File.Exists(installLocationFileFilePath))
            {
                string markdown = File.ReadAllText(installLocationFileFilePath);
                return markdown;
            }
            return string.Empty;
        }
    }
}
