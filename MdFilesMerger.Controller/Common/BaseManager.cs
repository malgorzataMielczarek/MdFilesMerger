using MdFilesMerger.App.Common;
using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.Controller.Common
{
    public abstract class BaseManager<T> : IManager where T : BaseItem
    {
        public BaseService<T> _service;

        protected BaseManager(BaseService<T> service)
        {
            _service = service;
        }

        public void DisplayItem(int id)
        {
            DisplayItem(_service.GetItemById(id));
        }

        public abstract void DisplayTitle();

        public virtual int SelectItem()
        {
            var list = GetFilteredList();
            DisplayItems(list);

            return SelectItem(list);
        }

        protected abstract void DisplayItem(T? item);

        protected virtual void DisplayItems(IReadOnlyList<T> items)
        {
            Console.WriteLine();

            for (int i = 0; i < items.Count; i++)
            {
                T? item = items[i];

                Console.Write($"{i}. ");
                DisplayItem(item);
            }

            Console.WriteLine();
        }

        protected void DisplayTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return;
            }

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

        protected int GetIdByIndex(int index, IReadOnlyList<BaseItem> list)
        {
            if (index > 0 && index <= list.Count)
            {
                return list[index].Id;
            }

            else
            {
                return -1;
            }
        }

        protected abstract IReadOnlyList<T> GetFilteredList();

        protected virtual int SelectItem(IReadOnlyList<T> list)
        {
            Console.Write("Podaj numer elementu, który chcesz wybrać: ");

            if (int.TryParse(Console.ReadLine(), out int index))
            {
                return GetIdByIndex(index, list);
            }

            return -1;
        }
    }
}
