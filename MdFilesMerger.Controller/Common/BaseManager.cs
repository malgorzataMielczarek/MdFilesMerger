using MdFilesMerger.App.Abstract;
using MdFilesMerger.Controller.Abstract;
using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.Controller.Common
{
    /// <summary>
    ///     Base, abstract class for all managers.
    ///     <para>
    ///         <b> Inheritance: </b> BaseManager&lt;T, TService&gt; <br/><b> Implements: </b><see
    ///         cref="IManager{T, TService}"/>
    ///     </para>
    /// </summary>
    /// <typeparam name="T"> Type of managed model. </typeparam>
    /// <typeparam name="TService">
    ///     Type of service handling collection of specified models.
    /// </typeparam>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="IManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    public abstract class BaseManager<T, TService> : IManager<T, TService> where T : IItem where TService : IService<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseManager{T, TService}"/> class and
        ///     all its properties.
        /// </summary>
        /// <remarks>
        ///     <see cref="SelectedItem"/> is set to <see langword="-1"/> and <see cref="Service"/>
        ///     to specified value.
        /// </remarks>
        /// <param name="service"> The instance of service associated with this class. </param>
        public BaseManager(TService service)
        {
            SelectedItem = -1;
            Service = service;
        }

        /// <inheritdoc/>
        public int SelectedItem { get; protected set; }

        /// <inheritdoc/>
        public TService Service { get; }

        /// <inheritdoc/>
        public void Display()
        {
            DisplayItem(Service.ReadById(SelectedItem));
        }

        /// <inheritdoc/>
        public abstract void DisplayTitle();

        /// <inheritdoc/>
        public virtual void Select(int connectedItemId)
        {
            var list = GetFilteredList(connectedItemId);
            DisplayItems(list);
            SelectItem(list);
        }

        /// <summary>
        ///     Displays the information about specified item.
        /// </summary>
        /// <param name="item"> The item to display. </param>
        protected virtual void DisplayItem(T? item)
        {
            if (item != null)
            {
                Console.WriteLine(item.Name);
            }
        }

        /// <summary>
        ///     Displays all items from the list as numbered list.
        /// </summary>
        /// <param name="items"> The list of items to display. </param>
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

        /// <summary>
        ///     Formats specified text as a manager's title and displays it in the console.
        /// </summary>
        /// <param name="title"> The text of manager's title. </param>
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

        /// <summary>
        ///     Gets elements from the collection that are connected with element with specified identifier.
        /// </summary>
        /// <param name="connectedItemId"> The identifier of the connected item. </param>
        /// <returns> List of received elements. </returns>
        protected abstract List<T> GetFilteredList(int connectedItemId);

        /// <summary>
        ///     Gets the element by it's index on the specified list.
        /// </summary>
        /// <param name="index"> The index of the element. </param>
        /// <param name="list"> The list containing the element on position <paramref name="index"/>. </param>
        /// <returns> The identification number of the element. </returns>
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

        /// <summary>
        ///     Asks user to select the item from the list and sets it as selected item.
        /// </summary>
        /// <param name="list"> The list of items to select from. </param>
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