namespace MdFilesMerger
{
    internal class MainDirectory
    {
        public string Path => _path;

        public MainDirectory()
        {
            _path = String.Empty;

            SetMainDirectory(Program.MAIN_DIRECTORY_PATH);
        }

        public bool SetMainDirectory(string? path)
        {
            if(Directory.Exists(path))
            {
                var directory = new DirectoryInfo(path);
                _path = directory.FullName;

                return true;
            }
            else
            {
                return false;
            }
        }


        private string _path;
    }
}
