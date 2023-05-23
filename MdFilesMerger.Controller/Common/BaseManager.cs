using MdFilesMerger.App.Common;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.Controller.Common
{
    public abstract class BaseManager<T> : IManager<T> where T : BaseItem
    {
        public BaseService<T> _service;

        protected BaseManager(BaseService<T> service)
        {
            _service = service;
        }

        public abstract int AddItem();

        public abstract void DisplayItems(IReadOnlyList<T> items);

        public void DisplayMenu(IReadOnlyList<MenuAction> actions)
        {
            Console.WriteLine();
            for (int i = 1; i <= actions.Count; i++)
            {
                var action = actions[i];
                Console.WriteLine($"{i}. {action.Name}");
            }

            Console.WriteLine();
        }

        public virtual int RemoveItem(int id)
        {
            T? item = _service.GetItemById(id);

            if (item != null)
            {
                return _service.RemoveItem(item);
            }

            else
            {
                return -1;
            }
        }

        public virtual int SelectAction(IReadOnlyList<MenuAction> actions)
        {
            if (actions.Count > 0)
            {
                Console.Write("Podaj numer czynności, którą chcesz wykonać, wciśnij Esc aby zakończyć działanie programu");

                if (actions[0].Menu != MenuType.Main)
                {
                    Console.Write(" lub Enter, aby wrócić do menu głównego");
                }

                Console.Write(": ");

                var key = Console.ReadKey();

                return key.Key switch
                {
                    ConsoleKey.Escape => 0,
                    ConsoleKey.Enter => 1,
                    >= ConsoleKey.D1 and <= ConsoleKey.D9 => GetIdByIndex(key.Key - ConsoleKey.D0, actions),
                    _ => -1
                };
            }

            return -1;
        }

        public abstract int SelectItem(IReadOnlyList<T> items);

        public abstract int UpdateItem();

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
    }
}
