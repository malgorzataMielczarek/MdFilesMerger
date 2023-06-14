using MdFilesMerger.App.Abstract;
using MdFilesMerger.App.Common;
using MdFilesMerger.App.Concrete;
using MdFilesMerger.Controller.Abstract;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.Controller.Common
{
    public abstract class RelativeFileMenager<T, U> : BaseManager<T, U>, ICRUDManager<T, U> where T : RelativeFile where U : RelativeFileService<T>
    {
        public RelativeFileMenager(U service) : base(service)
        {
        }

        protected override List<T> GetFilteredList(int mainDirectoryId) => Service.ReadByMainDirId(mainDirectoryId);

        public override void Select(int mainDirectoryId)
        {
            DisplayItems(GetFilteredList(mainDirectoryId));
        }

        protected override void DisplayItem(T? item)
        {
            if (item != null)
            {
                Console.WriteLine(item.GetPath());
            }
        }

        public bool Create(int mainDiectoryId)
        {
            if (mainDiectoryId == -1)
            {
                return false;
            }
            T? file = Activator.CreateInstance(typeof(T), 0, mainDiectoryId) as T;
            if (file == null)
            {
                return false;
            }
            int id = Service.Create(file);
            if (id == -1)
            {
                return false;
            }

            var cursor = Console.GetCursorPosition();

            while (true)
            {
                Console.WriteLine("Podaj ścieżkę do pliku (absolutną lub względną do katalogu głównego): ");
                string? path = Console.ReadLine();

                string errorMsg;

                if (!string.IsNullOrWhiteSpace(path))
                {
                    if (Service.UpdatePath(id, path) == id)
                    {
                        SelectedItem = id;
                        return true;
                    }
                    else
                    {
                        errorMsg = "Plik o podanej ścieżce nie istnieje.";
                    }
                }
                else
                {
                    errorMsg = "Ścieżka do pliku nie może być pusta.";
                }

                Console.SetCursorPosition(cursor.Left, cursor.Top);
                Console.WriteLine(errorMsg);
            }
        }

        public bool Delete()
        {
            if (SelectedItem != -1 && Service.Delete(SelectedItem) == SelectedItem)
            {
                SelectedItem = -1;
                return true;
            }

            return false;
        }

        public bool Delete(int mainDirectoryId)
        {
            foreach (var file in GetFilteredList(mainDirectoryId))
            {
                int id = file.Id;
                if (Service.Delete(file) != id)
                {
                    return false;
                }
            }

            SelectedItem = -1;
            return true;
        }

        public void SelectItem(int id)
        {
            SelectedItem = id;
        }
    }
}