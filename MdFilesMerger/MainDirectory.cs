namespace MdFilesMerger
{
    internal class MainDirectory
    {
        public DirectoryInfo? Info => _info;

        public MainDirectory()
        {
            SetMainDirectory(Program.MAIN_DIRECTORY_PATH);
        }

        public bool SetMainDirectory(string? path)
        {
            if(Directory.Exists(path))
            {
                _info = new DirectoryInfo(path);

                return true;
            }
            else
            {
                return false;
            }
        }


        private DirectoryInfo? _info;
    }
}
