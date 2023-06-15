using MdFilesMerger.Controller.Concrete;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            MenuActionManager menuActionManager = new MenuActionManager();
            UserManager userMenager = new UserManager();
            MergedFileManager mergedFileManager = userMenager.MergedFileManager;
            MainDirectoryManager mainDirManager = mergedFileManager.MainDirectoryManager;
            SelectedFileManager selectedFileMenager = mainDirManager.SelectedFileManager;
            IgnoredFileManager ignoredFileManager = mainDirManager.IgnoredFileManager;

            MenuAction? action = menuActionManager.Service.ReadById(menuActionManager.SelectedItem);

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

                switch (action?.Name)
                {
                    case "Zmień katalog główny":
                        mainDirManager.DisplayTitle();
                        mainDirManager.Delete();
                        mainDirManager.Create(mergedFileManager.SelectedItem);
                        menuActionManager.GoToMainMenu();
                        Console.Clear();
                        break;

                    case "Wyświetl listę plików do scalenia":
                        selectedFileMenager.DisplayTitle();
                        selectedFileMenager.Select(mainDirManager.SelectedItem);
                        menuActionManager.EnterOrEsc();
                        Console.Clear();
                        break;
                }

                if (action?.Menu == MenuType.TableOfContents)
                {
                    TableOfContents tableOfContents = action?.Name switch
                    {
                        "Spis treści będący zwykłym tekstem" => TableOfContents.Text,
                        "Spis treści złożony z hiperlinków do odpowiednich paragrafów" => TableOfContents.Hyperlink,
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

                    switch (action.Name)
                    {
                        case "Zmień nazwę tworzonego pliku":
                            mergedFileManager.UpdateFileName();
                            break;

                        case "Zmień ścieżkę katalogu":
                            mergedFileManager.UpdateParentDirectory();
                            break;

                        case "Zmień nagłówek":
                            mergedFileManager.UpdateTitle();
                            break;

                        case "Scal pliki":
                            mergedFileManager.Merge();
                            break;
                    }

                    Console.Clear();
                }
            }
        }
    }
}