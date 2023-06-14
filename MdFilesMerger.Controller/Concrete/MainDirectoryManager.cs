using MdFilesMerger.App.Concrete;
using MdFilesMerger.Controller.Abstract;
using MdFilesMerger.Controller.Common;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.Controller.Concrete
{
    public class MainDirectoryManager : BaseManager<MainDirectory, MainDirectoryService>, ICRUDManager<MainDirectory, MainDirectoryService>
    {
        public MainDirectoryManager() : base(new MainDirectoryService())
        {
            Initialize();
            SelectedFileManager = new SelectedFileManager(Service);
            IgnoredFileManager = new IgnoredFileManager(Service);
        }

        public SelectedFileManager SelectedFileManager { get; }
        public IgnoredFileManager IgnoredFileManager { get; }

        private void Initialize()
        {
            if (Service.IsEmpty())
            {
                DisplayTitle();
                Create(1);
                Console.Clear();
            }
            else
            {
                SelectedItem = 1;
            }
        }

        protected override void DisplayItem(MainDirectory? item)
        {
            if (item != null)
            {
                Console.WriteLine(item.GetPath());
            }
        }

        public override void DisplayTitle()
        {
            DisplayTitle("Ustaw katalog, w którym chcesz wyszukiwać pliki .md");
        }

        protected override List<MainDirectory> GetFilteredList(int mergedFileId) => Service.ReadByMergedFileId(mergedFileId);

        public bool Create(int mergedFileId)
        {
            var cursor = Console.GetCursorPosition();
            SelectedItem = -1;
            MainDirectory dir = new MainDirectory() { MergedFileId = mergedFileId };

            while (true)
            {
                Console.Write("Wprowadź ścieżkę dostępu do katalogu: ");
                string? path = Console.ReadLine();

                if (dir.SetPath(path))
                {
                    SelectedItem = Service.Create(dir);
                }
                else
                {
                    SelectedItem = -1;
                }

                if (SelectedItem == -1)
                {
                    Console.SetCursorPosition(cursor.Left, cursor.Top);
                    Console.WriteLine("Nie można znaleźć katalogu o podanej ścieżce. Upewnij się, że istnieje i ponownie wprowadź ścieżkę.");
                }
                else
                {
                    SelectedFileManager.Service.CreateRange(Service.FindAllFiles(SelectedItem), Service.ReadById(SelectedItem)!);
                    break;
                }
            }

            return SelectedItem != -1;
        }

        public bool Delete()
        {
            if (SelectedItem != -1 && Delete(Service.ReadById(SelectedItem)))
            {
                SelectedItem = -1;
                return true;
            }

            return false;
        }

        public bool Delete(int mergedFileId)
        {
            foreach (var item in GetFilteredList(mergedFileId))
            {
                if (!Delete(item))
                {
                    return false;
                }
            }

            SelectedItem = -1;
            return true;
        }

        public bool UpdatePath()
        {
            if (SelectedItem == -1)
            {
                return false;
            }

            var cursor = Console.GetCursorPosition();
            while (true)
            {
                Console.Write("Nowa ścieżka dostępu do katalogu: ");
                string? path = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(path) && Service.UpdatePath(SelectedItem, path) == SelectedItem)
                {
                    return true;
                }
                else
                {
                    Console.SetCursorPosition(cursor.Left, cursor.Top);
                    Console.WriteLine("Nie można znaleźć katalogu o podanej ścieżce. Upewnij się, że istnieje i ponownie wprowadź ścieżkę.");
                }
            }
        }

        private bool Delete(MainDirectory? mainDirectory)
        {
            if (mainDirectory == null)
            {
                return false;
            }

            int mainId = mainDirectory.Id;

            return SelectedFileManager.Delete(mainId) && IgnoredFileManager.Delete(mainId) && Service.Delete(mainDirectory) == mainId;
        }

        public void IgnoreFile(FileInfo file, MainDirectory? mainDirectory)
        {
            if (mainDirectory == null || string.IsNullOrWhiteSpace(file?.FullName))
            {
                return;
            }

            SelectedFile? selectedFile = new SelectedFile(file, mainDirectory);
            if ((selectedFile = SelectedFileManager.Service.GetEqual(selectedFile)) != null)
            {
                SelectedFileManager.SelectItem(selectedFile.Id);
                if (SelectedFileManager.Delete())

                {
                    int id = IgnoredFileManager.Service.Create(selectedFile.ToIgnoredFile());
                    IgnoredFileManager.SelectItem(id);
                }
            }
        }

        public void IgnoreFile(FileInfo file)
        {
            MainDirectory? mainDirectory = Service.ReadById(SelectedItem);
            IgnoreFile(file, mainDirectory);
        }
    }
}