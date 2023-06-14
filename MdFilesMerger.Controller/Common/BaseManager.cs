using MdFilesMerger.App.Abstract;
using MdFilesMerger.Controller.Abstract;
using MdFilesMerger.Domain.Common;
using System.Diagnostics.CodeAnalysis;

namespace MdFilesMerger.Controller.Common
{
    public abstract class BaseManager<T, U> : IManager<T, U> where T : BaseItem where U : IService<T>
    {
        public int SelectedItem { get; protected set; }

        public U Service { get; }

        public BaseManager(U service)
        {
            SelectedItem = -1;
            Service = service;
        }

        public void Display()
        {
            DisplayItem(Service.ReadById(SelectedItem));
        }

        public abstract void DisplayTitle();

        public virtual void Select(int connectedItemId)
        {
            var list = GetFilteredList(connectedItemId);
            DisplayItems(list);
            SelectItem(list);
        }

        protected virtual void DisplayItem(T? item)
        {
            if (item != null)
            {
                Console.WriteLine(item.Name);
            }
        }

        protected virtual void DisplayItems(List<T> items)
        {
            Console.WriteLine();

            for (int i = 0; i < items.Count; i++)
            {
                T? item = items[i];

                Console.Write($"{i + 1}. ");
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

        protected int GetIdByIndex(int index, IReadOnlyList<T> list)
        {
            if (index >= 0 && index < list.Count)
            {
                return list[index].Id;
            }
            else
            {
                return -1;
            }
        }

        protected abstract List<T> GetFilteredList(int connectedItemId);

        protected virtual void SelectItem(IReadOnlyList<T> list)
        {
            var cursor = Console.GetCursorPosition();
            while (true)
            {
                Console.Write("Podaj numer elementu, który chcesz wybrać: ");

                if (int.TryParse(Console.ReadLine(), out int index))
                {
                    SelectedItem = GetIdByIndex(index - 1, list);

                    if (SelectedItem != -1)
                    {
                        return;
                    }
                }

                Console.SetCursorPosition(cursor.Left, cursor.Top);
                Console.WriteLine("Nie rozpoznano wybranego elementu. Wybierz ponownie, podając numer elementu.");
            }
        }
    }
}