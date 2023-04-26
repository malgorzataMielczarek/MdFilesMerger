using System.Text;

namespace MdFilesMerger
{
    internal class Program
    {
        public const string MAIN_DIRECTORY_PATH = @"C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET";
        static void Main(string[] args)
        {
            string? mainDirectoryPath = SetMainDirectoryPath();
            int selectedFuncionality;
            ListOfMdFiles? listOfFiles = GetListOfMdFiles(mainDirectoryPath);
            TableOfContents tableOfContents;
            do
            {
                selectedFuncionality = DisplayMainMenu(mainDirectoryPath);
                switch(selectedFuncionality)
                {
                    case 1: mainDirectoryPath = ChangeMainDirectoryPath(mainDirectoryPath);
                        listOfFiles = GetListOfMdFiles(mainDirectoryPath);
                        break;
                    case 2: ChangeView(mainDirectoryPath);
                        listOfFiles.DisplayListOfFiles();
                        Console.WriteLine("Wciśnij Enter aby wrócić do menu głównego lub Esc by zakończyć program.");
                        var key = Console.ReadKey();
                        if (key.Key == ConsoleKey.Escape) selectedFuncionality = 0;
                        break;
                    case 3: tableOfContents = new TableOfContents(listOfFiles);
                        tableOfContents.DisplayMenu();
                        Console.WriteLine("Wciśnij Enter aby wrócić do menu głównego lub Esc by zakończyć program.");
                        key = Console.ReadKey();
                        if (key.Key == ConsoleKey.Escape) selectedFuncionality = 0;
                        break;
                    case 4: Console.WriteLine("Scalanie plików...");
                        Thread.Sleep(1000);
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
                bool isFirst = true;
                do
                {
                    Console.Clear();
                    DisplayTitle("Ustaw katalog, w którym chcesz wyszukiwać pliki .md");
                    Console.WriteLine("Domyślna ścieżka do katalogu: {0}", MAIN_DIRECTORY_PATH);
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
            return mainDirectoryPath;
        }
    }
}
