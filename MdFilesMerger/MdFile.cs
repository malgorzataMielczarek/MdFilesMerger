using System.Text;

namespace MdFilesMerger
{
    internal class MdFile
    {
        public FileInfo FileInfo { get; private set; }
        public string[] SubDirectories { get; private set; }
        public MdFile(FileInfo fileInfo, string mainDirectory)
        {
            FileInfo = fileInfo;
            SubDirectories = GetSubDirectories(mainDirectory);
        }

        private string[] GetSubDirectories(string mainDirectory)
        {
            if (FileInfo.DirectoryName != null)
            {
                string dirs = FileInfo.DirectoryName.Replace(mainDirectory, "");
                var directories = dirs.Split('\\', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                return directories;
            }
            return Array.Empty<string>();
        }
        //Open the file and read first not empty line of text from it. If it is a header (starts with '#') return it. Else return file name, without extension.
        //If file header is a link return only text part
        public string GetFileHeader()
        {
            return GetFileHeader(FileInfo);
        }
        public static string GetFileHeader(FileInfo file)
        {
            string header = "";
            using(FileStream fs = file.Open(FileMode.Open, FileAccess.Read))
            {
                byte[] buf = new byte[1024];
                int c;
                while ((c = fs.Read(buf, 0, buf.Length)) > 0)
                {
                    string text = Encoding.UTF8.GetString(buf, 0, c);
                    if (header.Length > 0 || text[0] == '#')
                    {
                        int index = text.IndexOf('\n');
                        if (index == -1)
                        {
                            header += text;
                        }
                        else
                        {
                            header += text[..index];
                            break;
                        }
                    }
                    else
                    {
                        header = file.Name.Replace(file.Extension, "");
                        break;
                    }
                }
            }
            return Helpers.ConvertHyperlinkHeaderToTextHeader(header);
        }
        public string? GetMainDirectoryPath()
        {
            string? mainDirectoryPath = FileInfo.DirectoryName;
            if (mainDirectoryPath != null)
            {
                if (SubDirectories.Length > 0)
                {
                    int index = mainDirectoryPath.IndexOf("\\" + SubDirectories[0] + "\\");
                    if (index == -1 && mainDirectoryPath.EndsWith("\\" + SubDirectories[0])) 
                        index = mainDirectoryPath.LastIndexOf("\\" + SubDirectories[0]);
                    if (index == -1 && mainDirectoryPath.StartsWith(SubDirectories[0] + "\\"))
                        index = 0;
                    if (index == -1) index = mainDirectoryPath.IndexOf(SubDirectories[0]);
                    mainDirectoryPath = mainDirectoryPath.Remove(index);
                }
            }
            return mainDirectoryPath;
        }
    }
}
