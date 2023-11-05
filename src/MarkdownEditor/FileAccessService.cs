namespace MarkdownEditor
{
    public interface IFileAccessService
    {
        string GetMarkdown();
        void PutMarkdown(string markdown);
    }
    public sealed class FileAccessService : IFileAccessService
    {
        private const string MarkdownFileName = "activeMarkdown.md";

        public string GetMarkdown()
        {
            string activeMarkdownFilePath = Path.Combine(AppContext.BaseDirectory, MarkdownFileName);
            if (File.Exists(activeMarkdownFilePath))
            {
                string markdown = File.ReadAllText(activeMarkdownFilePath);
                return markdown;
            }
            return string.Empty;
        }

        public void PutMarkdown(string markdown)
        {
            string activeMarkdownFilePath = Path.Combine(AppContext.BaseDirectory, MarkdownFileName);
            File.WriteAllText(activeMarkdownFilePath, markdown);
        }
    }
}
