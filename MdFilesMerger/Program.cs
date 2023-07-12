using MdFilesMerger.Controller.Concrete;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Controller.Abstract;

namespace MdFilesMerger
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string lang = ChooseLanguage();

            MenuActionManager menuActionManager = new MenuActionManager(lang);
            UserManager userMenager = new UserManager();
            IMergedFileManager mergedFileManager = userMenager.MergedFileManager;
            IMainDirectoryManager mainDirManager = mergedFileManager.MainDirectoryManager;
            ISelectedFileManager selectedFileMenager = mainDirManager.SelectedFileManager;
            IIgnoredFileManager ignoredFileManager = mainDirManager.IgnoredFileManager;

            IMenuAction? action = menuActionManager.Service.ReadById(menuActionManager.SelectedItem);

            while (menuActionManager.SelectedItem != 1)
            {
                Console.Write("Katalog główny: ");
                mainDirManager.Display();

                menuActionManager.DisplayTitle();

                if (action?.NextMenu == MenuType.MergedFile)
                {
                    Console.WriteLine("Ustawienia");
                    mergedFileManager.Display();
                }

                action = menuActionManager.Select();

                Console.Clear();

                switch (action?.Id)
                {
                    case 3:
                        mainDirManager.DisplayTitle();
                        mainDirManager.Delete();
                        mainDirManager.Create(mergedFileManager.SelectedItem);
                        menuActionManager.GoToMainMenu();
                        Console.Clear();
                        break;

                    case 4:
                        selectedFileMenager.DisplayTitle();
                        selectedFileMenager.Select(mainDirManager.SelectedItem);
                        menuActionManager.EnterOrEsc();
                        Console.Clear();
                        break;
                }

                if (action?.Menu == MenuType.TableOfContents)
                {
                    TableOfContents tableOfContents = action?.Id switch
                    {
                        7 => TableOfContents.Text,
                        8 => TableOfContents.Hyperlink,
                        _ => TableOfContents.None
                    };

                    mergedFileManager.UpdateTableOfContents(tableOfContents);
                    menuActionManager.EnterOrEsc();
                    Console.Clear();
                }

                if (action?.Menu == MenuType.MergedFile)
                {
                    Console.WriteLine("Ustawienia");
                    mergedFileManager.Display();

                    switch (action.Id)
                    {
                        case 10:
                            mergedFileManager.UpdateFileName();
                            break;

                        case 11:
                            mergedFileManager.UpdateParentDirectory();
                            break;

                        case 12:
                            mergedFileManager.UpdateTitle();
                            break;

                        case 13:
                            mergedFileManager.Merge();
                            break;
                    }

                    Console.Clear();
                }
            }
        }

        private static string ChooseLanguage()
        {
            string lang = "EN";
            string langDirPath = File.ReadAllText("langPath.txt");
            var languages = File.ReadAllText(Path.Combine(langDirPath, "languages.csv")).ToUpper().Split(',');
            if (languages.Length > 0)
            {
                Console.Write(File.ReadAllText(Path.Combine(langDirPath, "chooseLang.txt")));
                Console.Write(" (" + string.Join('/', languages) + "): ");
                string? chosenLang = Console.ReadLine()?.ToUpper();
                if (chosenLang != null && languages.Contains(chosenLang))
                {
                    lang = chosenLang;
                }
            }

            Console.Clear();

            return lang;
        }
    }
}