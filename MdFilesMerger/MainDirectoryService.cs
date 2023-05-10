namespace MdFilesMerger
{
    internal class MainDirectoryService
    {
        public string Title { get; }
        public ListOfMdFiles Files => _files;
        public MainDirectoryService()
        {
            Title = "Ustaw katalog, w którym chcesz wyszukiwać pliki .md";
            _mainDirectory = new MainDirectory();
            _files = new ListOfMdFiles();
            SetFiles();
        }

        public void Display()
        {
            Console.WriteLine($"Katalog główny: {_mainDirectory.Info?.FullName}");
        }
        public void DisplayErrorMessage(bool isError)
        {
            if(isError)
            {
                Console.WriteLine("Nie można znaleźć katalogu o podanej ścieżce. Upewnij się, że istnieje i ponownie wprowadź ścieżkę.");
            }
        }
        public string GetPath()
        {
            return _mainDirectory.Info?.FullName ?? String.Empty;
        }

        public bool ChangePath()
        {
            Console.Write("Wprowadź ścieżkę dostępu do katalogu: ");

            bool isSuccess = SetPath(Console.ReadLine());
            
            if(isSuccess)
            {
                SetFiles();
            }

            return isSuccess;
        }

        private MainDirectory _mainDirectory;
        private ListOfMdFiles _files;

        private bool SetPath(string? mainDirectoryPath)
        {
            return _mainDirectory.SetMainDirectory(mainDirectoryPath);
        }
        private void SetFiles()
        {
            _files.Clear();
            if (_mainDirectory.Info != null)
            {
                foreach (FileInfo file in _mainDirectory.Info.EnumerateFiles("*.md", SearchOption.AllDirectories))
                {
                    _files.Add(new MdFile(file, GetPath()));
                }
                _files.Sort();
            }
        }
    }
}
