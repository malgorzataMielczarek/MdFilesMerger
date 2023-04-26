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
            if(FileInfo.DirectoryName != null)
            {
                string dirs = FileInfo.DirectoryName.Replace(mainDirectory, "");
                var directories = dirs.Split('\\', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                return directories;
            }
            return Array.Empty<string>();
        }
        //TODO: Open the file and read first not empty line of text from it. If it is a header (starts with '#') return it. Else return file name, without extension.
        public string GetFileHeader()
        {
            return GetFileHeader(FileInfo);
        }
        public static string GetFileHeader(FileInfo file)
        {
            return file.Name.Replace(file.Extension, "");
        }
    }
}
