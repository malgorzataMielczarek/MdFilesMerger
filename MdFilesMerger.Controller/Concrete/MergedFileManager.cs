using MdFilesMerger.Controller.Abstract;
using MdFilesMerger.Controller.Common;
using MdFilesMerger.Controller.Concrete;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;
using Microsoft.VisualBasic.FileIO;
using System.Text;

namespace MdFilesMerger.App.Concrete
{
    public class MergedFileManager : BaseManager<MergedFile, MergedFileService>, ICRUDManager<MergedFile, MergedFileService>
    {
        public MergedFileManager() : base(new MergedFileService())
        {
            SelectedItem = 1;
            MainDirectoryManager = new MainDirectoryManager();

            // set merged file parent directory to main directory
            string? path = MainDirectoryManager.Service.ReadById(SelectedItem)?.GetPath();
            if (!string.IsNullOrWhiteSpace(path))
            {
                Service.ReadById(SelectedItem)?.SetParentDirectory(path);
            }
        }

        public MainDirectoryManager MainDirectoryManager { get; }

        public bool Create(int userId)
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            return Delete(Service.ReadById(SelectedItem));
        }

        public bool Delete(int userId)
        {
            foreach (var file in GetFilteredList(userId))
            {
                if (!Delete(file))
                {
                    return false;
                }
            }

            return true;
        }

        protected override void DisplayItem(MergedFile? item)
        {
            if (item != null)
            {
                DisplaySetting("Nazwa tworzonego pliku", item.GetFileName());
                DisplaySetting("Położenie pliku", item.GetParentDirectory());
                DisplaySetting("Tytuł", item.Title);
            }
        }

        public override void DisplayTitle()
        {
            DisplayTitle("Połącz wybrane pliki");
        }

        public void Merge()
        {
            FileInfo? file = Service.CreateFile(SelectedItem);

            if (file != null)
            {
                string mergingMsg = "Scalanie plików";
                Console.WriteLine(mergingMsg);

                List<SelectedFile> selectedFiles = new List<SelectedFile>();

                foreach (var dir in MainDirectoryManager.Service.ReadByMergedFileId(SelectedItem))
                {
                    MainDirectoryManager.IgnoreFile(file, dir);
                    selectedFiles.AddRange(MainDirectoryManager.SelectedFileManager.Service.ReadByMainDirId(dir.Id));
                }

                Service.AppendTableOfContents(file, SelectedItem, selectedFiles);

                string newLine = Service.ReadById(SelectedItem)!.NewLineStyle;
                for (int i = 0; i < selectedFiles.Count; i++)
                {
                    MainDirectoryManager.SelectedFileManager.Service.AppendFile(selectedFiles[i], file, newLine);

                    // show progress
                    Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                    int procent = (i + 1) * 100 / selectedFiles.Count;
                    StringBuilder progress = new StringBuilder(mergingMsg);
                    progress.Append(' ', (Console.WindowWidth - mergingMsg.Length - procent.ToString().Length - 1) / 2);
                    progress.Append(procent);
                    progress.Append('%');
                    Console.WriteLine(progress.ToString());
                    Thread.Sleep(1);
                }
            }
        }

        public void UpdateFileName()
        {
            if (SelectedItem != -1)
            {
                var cursor = Console.GetCursorPosition();
                Console.Write("Podaj nazwę tworzonego pliku: ");
                while (true)
                {
                    string? name;
                    while (string.IsNullOrEmpty(name = Console.ReadLine()))
                    {
                        Console.SetCursorPosition(cursor.Left, cursor.Top);
                        Console.WriteLine("Nazwa pliku nie może być pusta.");
                        Console.Write("Podaj ponownie nazwę tworzonego pliku: ");
                    }

                    if (Service.UpdateFileName(SelectedItem, name) == SelectedItem)
                    {
                        break;
                    }
                    else
                    {
                        Console.SetCursorPosition(cursor.Left, cursor.Top);
                        Console.WriteLine("Podana nazwa zawiera niedozwolone znaki.");
                        Console.Write("Podaj ponownie nazwę tworzonego pliku: ");
                    }
                }
            }
        }

        public void UpdateParentDirectory()
        {
            if (SelectedItem != -1)
            {
                var cursor = Console.GetCursorPosition();
                Console.Write("Podaj ścieżkę do katalogu, w którym chcesz zapisać plik: ");
                while (true)
                {
                    string? path;
                    while (string.IsNullOrEmpty(path = Console.ReadLine()))
                    {
                        Console.SetCursorPosition(cursor.Left, cursor.Top);
                        Console.WriteLine("Ścieżka nie może być pusta.");
                        Console.Write("Podaj ponownie ścieżkę do katalogu, w którym chcesz zapisać plik: ");
                    }

                    if (Service.UpdateParentDirectory(SelectedItem, path) == SelectedItem)
                    {
                        break;
                    }
                    else
                    {
                        Console.SetCursorPosition(cursor.Left, cursor.Top);
                        Console.WriteLine("Podana ścieżka jest nieprawidłowa.");
                        Console.Write("Podaj ponownie ścieżkę do katalogu, w którym chcesz zapisać plik: ");
                    }
                }
            }
        }

        public void UpdateTitle()
        {
            if (SelectedItem != 1)
            {
                Console.Write("Podaj nagłówek (tytuł) tworzonego pliku: ");
                Service.UpdateTitle(SelectedItem, Console.ReadLine());
            }
        }

        public void UpdateTableOfContents(TableOfContents newTableOfContents)
        {
            if (SelectedItem == -1)
            {
                return;
            }

            Service.UpdateTableOfContents(SelectedItem, newTableOfContents);

            MergedFile? file = Service.ReadById(SelectedItem);
            if (file != null)
            {
                DisplayTitle("Podgląd wybranego spisu treści");
                if (file.TableOfContents == TableOfContents.None)
                {
                    Console.WriteLine("Brak");
                }
                else
                {
                    List<SelectedFile> selectedFiles = new List<SelectedFile>();
                    foreach (var mainDir in MainDirectoryManager.Service.ReadByMergedFileId(file.Id))
                    {
                        selectedFiles.AddRange(MainDirectoryManager.SelectedFileManager.Service.ReadByMainDirId(mainDir.Id));
                    }

                    Console.WriteLine(TableOfContentsService.CreateTOC(file, selectedFiles));
                }
                Console.WriteLine();
            }
        }

        protected override List<MergedFile> GetFilteredList(int userId) => Service.ReadByUserId(userId);

        private bool Delete(MergedFile? file)
        {
            if (file == null)
            {
                return false;
            }

            int id = file.Id;
            return MainDirectoryManager.Delete(id) && Service.Delete(file) == id;
        }

        private void DisplaySetting(string description, string? value)
        {
            Console.WriteLine($"{description}: {value}");
        }
    }
}