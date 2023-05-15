using System.Text;

namespace MdFilesMerger
{
    internal class Program
    {
        public const string NEW_LINE = "\n";

        static void Main(string[] args)
        {
            MainMenuService mainMenuService = new MainMenuService();
            MainDirectoryService mainDirectoryService = new MainDirectoryService();
            TableOfContents? tableOfContents = new TableOfContents();
            MergerService mergerService;

            // if default directory does not exist set it
            if (mainDirectoryService.MainDirectory == null)
            {
                DisplayTitle(mainDirectoryService.Title);

                while (!mainDirectoryService.ChangePath())
                {
                    Console.Clear();

                    DisplayTitle(mainDirectoryService.Title);

                    mainDirectoryService.DisplayErrorMessage(true);
                }
            }

            // create default MergerService
            mergerService = new MergerService(mainDirectoryService.MainDirectory);

            // Main menu
            MenuActionService? selectedActionService = null;
            bool isError = false;
            // Main program loop
            while (selectedActionService?.MenuAction.Description != "Exit")
            {
                ChangeView(mainDirectoryService);

                // select main menu action if not selected
                if (selectedActionService == null)
                {
                    DisplayTitle(mainMenuService.Title);

                    mainMenuService.DisplayMenu();

                    mainMenuService.DisplayErrorMessage(isError);

                    selectedActionService = mainMenuService.SelectAction();
                    isError = selectedActionService == null;
                }

                // if action selected perform it
                else
                {
                    switch (selectedActionService.MenuAction)
                    {
                        case { Id: 1, Menu: nameof(MainMenu) }:
                            DisplayTitle(mainDirectoryService.Title);

                            mainDirectoryService.DisplayErrorMessage(isError);

                            if (mainDirectoryService.ChangePath())
                            {
                                selectedActionService = null;
                                isError = false;

                                // when new main directory is set change default merged file directory
                                mergerService.Merger.SetDirectory(mainDirectoryService.GetPath());
                            }

                            else
                            {
                                isError = true;
                            }

                            break;

                        case { Id: 2, Menu: nameof(MainMenu) }:
                            DisplayTitle("Lista plików do scalenia");

                            mainDirectoryService.Files.DisplayListOfFiles();
                            selectedActionService = ExitOnEsc();
                            isError = false;

                            break;

                        case { Id: 3, Menu: nameof(MainMenu) } or { Menu: nameof(TableOfContents) }:
                            DisplayTitle(tableOfContents.Title);
                            if (selectedActionService.MenuAction.Menu == nameof(MainMenu))
                            {
                                tableOfContents.DisplayMenu();

                                tableOfContents.DisplayErrorMessage(isError);

                                var tableOfContentsAction = tableOfContents.SelectAction();

                                if (tableOfContentsAction == null)
                                {
                                    isError = true;
                                }

                                else
                                {
                                    isError = false;
                                    selectedActionService = tableOfContentsAction;
                                }
                            }

                            else
                            {
                                Console.WriteLine(tableOfContents.Create(mainDirectoryService.Files) ?? "Brak");
                                selectedActionService = ExitOnEsc();
                                isError = false;
                            }

                            break;

                        case { Id: 4, Menu: nameof(MainMenu) } or { Menu: nameof(MergerService) }:
                            DisplayTitle(mergerService.Title);
                            mergerService.DisplaySelectedSettings();
                            if (selectedActionService.MenuAction.Menu == nameof(MainMenu))
                            {
                                mergerService.DisplayMenu();
                                if (isError)
                                {
                                    mergerService.DisplayErrorMessage();
                                }

                                var action = mergerService.SelectSettingToChange();

                                if (action == null)
                                {
                                    isError = true;
                                }

                                else
                                {
                                    selectedActionService = action;
                                    isError = false;
                                }
                            }

                            else if (selectedActionService.MenuAction.Description == "Merge")
                            {
                                mergerService.MergeFiles(mainDirectoryService.Files, tableOfContents);
                                isError = false;
                                selectedActionService = null;
                            }

                            else
                            {
                                isError = !mergerService.ChangeSelectedSetting(selectedActionService, isError);

                                // if setting correctly changed return to merger menu view
                                if (!isError)
                                {
                                    selectedActionService = mainMenuService.MainMenu.SelectActionById(4);
                                }
                            }

                            break;
                    }
                }
            }
        }

        internal static void ChangeView(MainDirectoryService mainDirectoryService)
        {
            Console.Clear();
            mainDirectoryService.Display();
        }

        internal static void DisplayTitle(string title)
        {
            Console.WriteLine();

            int windowWidth = Console.WindowWidth;
            int numberOfHyphens = (windowWidth - title.Length) / 2;
            string hyphens = new string('-', numberOfHyphens);

            Console.Write(hyphens + title.ToUpper() + hyphens);

            if (windowWidth > numberOfHyphens * 2 + title.Length)
            {
                Console.Write("-");
            }

            Console.WriteLine("\n");
        }

        private static MenuActionService? ExitOnEsc()
        {
            Console.WriteLine();

            Console.Write("Wciśnij Enter aby wrócić do menu głównego lub Esc by zakończyć program.");

            var key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
            {
                return new MenuActionService(0, "Exit", nameof(MainMenu));
            }

            else
            {
                return null;
            }
        }
    }
}
