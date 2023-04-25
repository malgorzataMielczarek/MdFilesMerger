namespace MdFilesMerger
{
    internal class FileCustomInfo
    {
        public FileInfo FileInfo { get; private set; }
        public string[] SubDirectories { get; private set; }
        public FileCustomInfo(FileInfo fileInfo, string mainDirectory)
        {
            FileInfo = fileInfo;
            SubDirectories = GetSubDirectories(mainDirectory);
        }

        private string[] GetSubDirectories(string mainDirectory)
        {
            if(FileInfo.DirectoryName != null)
            {
                string dirs = FileInfo.DirectoryName.Replace(mainDirectory, "");
                return dirs.Split('\\', StringSplitOptions.TrimEntries & StringSplitOptions.RemoveEmptyEntries);
            }
            return Array.Empty<string>();
        }
    }
}
