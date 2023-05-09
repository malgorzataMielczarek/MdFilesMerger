using System.Text;

namespace MdFilesMerger
{
    internal class Program
    {
        public const string MAIN_DIRECTORY_PATH = @"..\..\..\..\..\KursZostanProgramistaASPdotNET";
        public const string MERGED_FILE_TITLE = "Kurs \"Zostań programistą ASP.NET\" - notatki";
        public const string MERGE_FILE_NAME = "README.md";
        public const string NEW_LINE = "\n";
        static void Main(string[] args)
        {
            //if default directory does not exist set it
            MainDirectoryService mainDirectoryService = new MainDirectoryService();
            if(String.IsNullOrEmpty(mainDirectoryService.GetPath()))
            {
                DisplayTitle(mainDirectoryService.Title);

                while(!mainDirectoryService.SetPath())
                {
                    Console.Clear();

                    DisplayTitle(mainDirectoryService.Title);
                    
                    Console.WriteLine(mainDirectoryService.ErrorMessage);
                }
            }

            //display main menu
            string? mainDirectoryPath = SetMainDirectoryPath();
            int selectedFuncionality;
            TableOfContents tableOfContents = new TableOfContents(GetListOfMdFiles(mainDirectoryPath));
            do
            {
                selectedFuncionality = DisplayMainMenu(mainDirectoryPath);
                switch(selectedFuncionality)
                {
                    case 1: mainDirectoryPath = ChangeMainDirectoryPath(mainDirectoryPath);
                        tableOfContents = new TableOfContents(GetListOfMdFiles(mainDirectoryPath));
                        break;
                    case 2: ChangeView(mainDirectoryPath);
                        tableOfContents.ListOfFiles.DisplayListOfFiles();
                        Console.WriteLine("Wciśnij Enter aby wrócić do menu głównego lub Esc by zakończyć program.");
                        var key = Console.ReadKey();
                        if (key.Key == ConsoleKey.Escape) selectedFuncionality = 0;
                        break;
                    case 3: tableOfContents.DisplayMenu();
                        Console.WriteLine("Wciśnij Enter aby wrócić do menu głównego lub Esc by zakończyć program.");
                        key = Console.ReadKey();
                        if (key.Key == ConsoleKey.Escape) selectedFuncionality = 0;
                        break;
                    case 4: DisplayMergeSettings(mainDirectoryPath, tableOfContents);
                        break;
                }
            }
            while (selectedFuncionality != 0);
        }
        internal static void DisplayTitle(string title)
        {
            Console.WriteLine();
            int windowWidth = Console.WindowWidth;
            int numberOfHyphens = (windowWidth - title.Length) / 2;
            string hyphens = new string('-', numberOfHyphens);
            if (windowWidth > numberOfHyphens * 2 + title.Length)
                Console.Write("-");
            Console.WriteLine(hyphens + title.ToUpper() + hyphens + "\n");
            Console.WriteLine();
        }
        internal static void ChangeView(string? mainDirectoryPath = null)
        {
            Console.Clear();
            if (mainDirectoryPath == null) mainDirectoryPath = MAIN_DIRECTORY_PATH;
            Console.WriteLine("Katalog główny, w którym wyszukiwane są pliki do scalenia: {0}\n", mainDirectoryPath);
        }
        private static ListOfMdFiles GetListOfMdFiles(string mainDirectoryPath)
        {
            var listOfFiles = new ListOfMdFiles();
            var mainDirectory = Directory.CreateDirectory(mainDirectoryPath);
            foreach(FileInfo file in mainDirectory.EnumerateFiles("*.md", SearchOption.AllDirectories))
            {
                listOfFiles.Add(new MdFile(file, mainDirectoryPath));
            }
            listOfFiles.Sort();
            return listOfFiles;
        }
        private static string SetMainDirectoryPath()
        {
            string? mainDirectoryPath = MAIN_DIRECTORY_PATH;
            string? change;
            if (Directory.Exists(MAIN_DIRECTORY_PATH))
            {
                mainDirectoryPath = new DirectoryInfo(MAIN_DIRECTORY_PATH).FullName;
                bool isFirst = true;
                do
                {
                    Console.Clear();
                    DisplayTitle("Ustaw katalog, w którym chcesz wyszukiwać pliki .md");
                    Console.WriteLine("Domyślna ścieżka do katalogu: {0}", mainDirectoryPath);
                    if (isFirst)
                    {
                        Console.Write("Czy chcesz ją zmienić? (t/n) ");
                        isFirst = false;
                    }
                    else
                    {
                        Console.WriteLine("Nie rozumiem Twojej odpowiedzi.\nCzy chcesz zmienić domyślną ścieżkę katalogu?");
                        Console.Write("Wpisz \"t\", jeśli tak lub \"n\", jeśli nie, aby odpowiedzieć na pytanie: ");
                    }
                    change = Console.ReadLine();
                    if (change != null) change = change.ToLower();
                    Console.WriteLine();
                }
                while (!(change == "t" || change == "n"));
            }
            else change = "t";
            if (change == "t")
            {
                mainDirectoryPath = ChangeMainDirectoryPath(mainDirectoryPath);
            }
            return mainDirectoryPath;
        }
        private static int DisplayMainMenu(string? mainDirectoryPath = null)
        {
            int result = -2;
            const int numberOfMenuItems = 4;
            while (result < 0)
            {
                ChangeView(mainDirectoryPath);
                DisplayTitle("Menu główne");
                Console.WriteLine("1. Zmień katalog główny");
                Console.WriteLine("2. Wyświetl listę plików do scalenia");
                Console.WriteLine("3. Utwórz spis treści");
                Console.WriteLine("4. Scal pliki");
                Console.WriteLine();
                if(result == -1)
                {
                    Console.Write("Nie rozumiem co chcesz zrobić. ");
                }
                Console.Write("Podaj numer czynności z powyższego menu, którą chcesz wykonać lub wciśnij Esc, aby zakończyć działanie programu: ");
                var key = Console.ReadKey();
                if(key.Key == ConsoleKey.Escape) result = 0;
                else
                {
                    string input = key.KeyChar.ToString();
                    input += Console.ReadLine();
                    if(int.TryParse(input, out result))
                    {
                        if (result < 0 || result > numberOfMenuItems) result = -1;
                    }
                    else
                    {
                        result = -1;
                    }
                }
            }
            return result;
        }
        private static string ChangeMainDirectoryPath(string? mainDirectoryPath)
        {
            if (mainDirectoryPath == null) mainDirectoryPath = MAIN_DIRECTORY_PATH;
            bool isReenter = false;
            do
            {
                ChangeView(mainDirectoryPath);
                DisplayTitle("Ustaw katalog, w którym chcesz wyszukiwać pliki .md");

                if (isReenter) Console.WriteLine("Nie można znaleźć katalogu o podanej ścieżce. Upewnij się, że istnieje i ponownie wprowadź ścieżkę.");
                Console.Write("Wprowadź ścieżkę dostępu do katalogu: ");
                mainDirectoryPath = Console.ReadLine();

                isReenter = true;
            }
            while (mainDirectoryPath == null || mainDirectoryPath.Trim().Length <= 0 || !Directory.Exists(mainDirectoryPath));
            return new DirectoryInfo(mainDirectoryPath).FullName;
        }
        private static void DisplayMergeSettings(string mainDirectoryPath, TableOfContents tableOfContents)
        {
            string fileName = MERGE_FILE_NAME;
            string filePath = mainDirectoryPath;
            string title = MERGED_FILE_TITLE;
            bool changeSettings = true;
            do
            {
                Program.ChangeView(mainDirectoryPath);
                Program.DisplayTitle("Połącz wybrane pliki");
                Console.WriteLine("Wybrane pliki zostaną połączone w plik: {0}", fileName);
                Console.WriteLine("Który zostanie zapisany w katalogu: {0}", filePath);
                Console.WriteLine("Nowy plik będzie mieć nagłówek: {0}", title);
                Console.WriteLine();
                Console.WriteLine("Jeżeli chcesz zmienić któreś z tych ustawień wybierz odpowiedni numer z poniższego menu.\n");
                Console.WriteLine("1. Zmień nazwę tworzonego pliku");
                Console.WriteLine("2. Zmień ścieżkę katalogu");
                Console.WriteLine("3. Zmień nagłówek");
                Console.WriteLine();
                Console.Write("Podaj numer ustawienia (1 - 3), które chcesz zmienić lub wciśnij Enter aby połączyć pliki z wybranymi ustawieniami: ");
                string? option = Console.ReadLine();
                if(string.IsNullOrEmpty(option))
                    changeSettings = false;
                else if(int.TryParse(option, out int value) && value >= 1 && value <= 3)
                {
                    switch(value)
                    {
                        case 1: fileName = ChangeMergeFileName(); break;
                        case 2: filePath = ChangeMergeFilePath(); break;
                        case 3: title = ChangeMergeFileHeader(); break;
                    }
                }
            }
            while (changeSettings);

            Program.ChangeView(mainDirectoryPath);
            Program.DisplayTitle("Połącz wybrane pliki");
            Console.WriteLine("Scalanie plików...");
            Merger merger = new Merger(tableOfContents, fileName, filePath) { Title = title };
            merger.MergeFiles();
            Thread.Sleep(1000);
        }
        private static string ChangeMergeFileName()
        {
            Console.Clear();
            Program.DisplayTitle("Połącz wybrane pliki");
            Console.Write("Podaj nazwę tworzonego pliku: ");
            string? name;
            while (string.IsNullOrEmpty(name = Console.ReadLine())) ;
            return name;
        }
        private static string ChangeMergeFilePath()
        {
            Console.Clear();
            Program.DisplayTitle("Połącz wybrane pliki");
            Console.Write("Podaj ścieżkę do katalogu, w którym chcesz zapisać plik: ");
            string? path;
            do
            {
                path = Console.ReadLine();
                try
                { 
                    DirectoryInfo dir = Directory.CreateDirectory(path);
                    path = dir.FullName;
                }
                catch
                {
                    Console.Write("\rNie można zapisać pliku w podanej lokalizacji. Wybierz inną lokalizację: ");
                    path = null;
                }
            }
            while (path == null) ;
            return path;
        }
        private static string ChangeMergeFileHeader()
        {
            Console.Clear();
            Program.DisplayTitle("Połącz wybrane pliki");
            Console.Write("Podaj nagłówek tworzonego pliku: ");
            return Console.ReadLine() ?? String.Empty;
        }
    }
}
