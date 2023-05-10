using System.Text;
using System.Xml.Linq;

namespace MdFilesMerger
{
    internal class Merger
    {
        public FileInfo File { get; set; }
        public string Title { get; set; }

        public Merger(DirectoryInfo directory, string fileName = Program.MERGE_FILE_NAME)
        {
            Title = Program.MERGED_FILE_TITLE;
            
            File = new FileInfo(CreatePath(directory.FullName, fileName));
        }
        public Merger(string path)
        {
            Title = Program.MERGED_FILE_TITLE;

            if (!path.EndsWith(".md")) path += ".md";
            File = new FileInfo(path);
        }
        public bool SetFileName(string? fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            else
            {
                File = new FileInfo(CreatePath(File.DirectoryName, fileName));

                return true;
            }
        }
        public bool SetDirectory(string? path)
        {
            if (String.IsNullOrEmpty(path))
            {
                return false;
            }
            else
            {
                try
                {
                    DirectoryInfo dir = Directory.CreateDirectory(path.ToString());

                    File = new FileInfo(CreatePath(dir.FullName, File.Name));

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool SetTitle(string? title)
        {
            Title = title ?? string.Empty;
            return true;
        }

        private string CreatePath(string? directoryPath, string fileName)
        {
            StringBuilder path = new StringBuilder(directoryPath?.Trim());
            path.Append('\\');
            path.Append(fileName.Trim());
            if (!fileName.TrimEnd().EndsWith(".md"))
            {
                path.Append(".md");
            }

            return path.ToString();
        }
    }
}
