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

        public bool Display()
        {
            if(String.IsNullOrEmpty(_path))
            {
                return false;
            }
            else
            {
                Console.WriteLine("Domyślna ścieżka do katalogu: {0}", _path);
                return true;
            }
        }

        public bool SetMainDirectory(string path)
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
