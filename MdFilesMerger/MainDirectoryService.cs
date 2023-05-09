namespace MdFilesMerger
{
    internal class MainDirectoryService
    {
        public string Title => TITLE;
        public string ErrorMessage => ERROR_MESSAGE;
        public MainDirectoryService()
        {
            _mainDirectory = new MainDirectory();
        }

        public void Display()
        {
            Console.WriteLine($"Katalog główny: {_mainDirectory.Path}");
        }

        public bool SetPath()
        {
            Console.Write("Wprowadź ścieżkę dostępu do katalogu: ");
            string? mainDirectoryPath = Console.ReadLine();

            return _mainDirectory.SetMainDirectory(mainDirectoryPath);
        }

        public string GetPath()
        {
            return _mainDirectory.Path;
        }

        private MainDirectory _mainDirectory;
        private const string TITLE = "Ustaw katalog, w którym chcesz wyszukiwać pliki .md";
        private const string ERROR_MESSAGE = "Nie można znaleźć katalogu o podanej ścieżce. Upewnij się, że istnieje i ponownie wprowadź ścieżkę.";
    }
}
