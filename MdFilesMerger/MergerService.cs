using System.Text;

namespace MdFilesMerger
{
    internal class MergerService
    {

        private readonly MenuActionService[] actions;

        public string Title { get; }
        public Merger Merger { get; }

        public MergerService(DirectoryInfo? directory, string fileName = "README.md") : this()
        {
           Merger = new Merger(directory, fileName);  
        }

        public MergerService(string path):this()
        {
            Merger = new Merger(path);
        }

        private MergerService()
        {
            Title = "Połącz wybrane pliki";

            actions = new MenuActionService[3];
            string menu = this.GetType().Name;
            actions[0] = new MenuActionService(1, "Zmień nazwę tworzonego pliku", menu);
            actions[1] = new MenuActionService(2, "Zmień ścieżkę katalogu", menu);
            actions[2] = new MenuActionService(3, "Zmień nagłówek", menu);
        }

        public bool ChangeSelectedSetting(MenuActionService selectedSetting, bool isError = false)
        {
            if(selectedSetting.MenuAction.Menu != this.GetType().Name)
            {
                return false;
            }

            switch (selectedSetting.MenuAction.Id)
            {
                case 1: 
                    return ChangeMergeFileName();
                case 2:
                    return ChangeMergeDirectory(isError);
                case 3: 
                    return ChangeMergeTitle();
                default:
                    return false;
            }
        }

        public void DisplayErrorMessage()
        {
            Console.WriteLine("Nie rozpoznano wybranej opcji.");
        }

        public void DisplayMenu()
        {
            Console.WriteLine("Zmień ustawienia");

            foreach (var action in actions)
            {
                action.Display();
            }

            Console.WriteLine();
        }

        public void DisplaySelectedSettings()
        {
            Console.WriteLine("Ustawienia");

            DisplaySetting("Nazwa tworzonego pliku", Merger.File.Name);
            DisplaySetting("Położenie pliku", Merger.File.DirectoryName);
            DisplaySetting("Tytuł", Merger.Title);

            Console.WriteLine();
        }

        public void MergeFiles(ListOfMdFiles listOfFiles, TableOfContents? tableOfContents)
        {
            string mergingMsg = "Scalanie plików";
            Console.WriteLine(mergingMsg);

            if (Merger.File.Exists)
            {
                // If file that will contain marged files is on the list of files to merge, remove it
                if (listOfFiles != null && listOfFiles.Count > 0 && listOfFiles.Where(mdFile => mdFile.FileInfo.FullName == Merger.File.FullName).Any())
                {
                    MdFile mdFile = listOfFiles.First(mdFl => mdFl.FileInfo.FullName == Merger.File.FullName);
                    listOfFiles.Remove(mdFile);
                }

                // remove old file
                Merger.File.Delete();
            }

            using (StreamWriter streamWriter = Merger.File.CreateText())
            {
                // set new line format
                streamWriter.NewLine = Program.NEW_LINE;

                // Enter title if exists
                if (!string.IsNullOrWhiteSpace(Merger.Title))
                {
                    string firstLine = "# " + Merger.Title;
                    streamWriter.WriteLine(firstLine);
                }

                if (listOfFiles != null)
                {
                    // Enter table of contents if exists
                    if (tableOfContents != null && tableOfContents.Type != MdFilesMerger.TableOfContents.Types.None)
                    {
                        streamWriter.WriteLine(tableOfContents.Create(listOfFiles));
                    }
                }

                streamWriter.Close();
            }

            // Enter files content
            for (int i = 0; i < (listOfFiles?.Count ?? 0); i++)
            {
                MdFile file = listOfFiles[i];

                file.AppendTo(Merger.File);

                // show progress
                Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                int procent = (i + 1) * 100 / listOfFiles.Count;
                StringBuilder progress = new StringBuilder(mergingMsg);
                progress.Append(' ', (Console.WindowWidth - mergingMsg.Length - procent.ToString().Length - 1) / 2);
                progress.Append(procent);
                progress.Append('%');
                Console.WriteLine(progress.ToString());
                Thread.Sleep(1);
            }
        }

        public MenuActionService? SelectSettingToChange()
        {
            Console.Write("Podaj numer ustawienia (1 - 3), które chcesz zmienić lub wciśnij Enter aby połączyć pliki z wybranymi ustawieniami: ");
            string? option = Console.ReadLine();

            // If pressed Enter - selected option actions.Length + 1, merge files
            if (string.IsNullOrEmpty(option))
            {
                return new MenuActionService(actions.Length + 1, "Merge", this.GetType().Name);
            }

            // chose valid option
            else if (int.TryParse(option, out int value) && value >= 1 && value <= actions.Length)
            {
                return actions[value - 1];
            }

            // return null, if typed invalid data
            else
            {
                return null;
            }
        }

        private bool ChangeMergeDirectory(bool isError)
        {
            if (isError)
            {
                Console.Write("Nie można zapisać pliku w podanej lokalizacji. Wybierz inną lokalizację: ");
            }

            else
            {
                Console.Write("Podaj ścieżkę do katalogu, w którym chcesz zapisać plik: ");
            }

            return Merger.SetDirectory(Console.ReadLine());
        }

        private bool ChangeMergeFileName()
        {
            Console.Write("Podaj nazwę tworzonego pliku: ");
            
            return Merger.SetFileName(Console.ReadLine());
        }

        private bool ChangeMergeTitle()
        {
            Console.Write("Podaj nagłówek (tytuł) tworzonego pliku: ");

            return Merger.SetTitle(Console.ReadLine());
        }

        private void DisplaySetting(string description, string? value)
        {
            Console.WriteLine($"{description}: {value}");
        }
    }
}
