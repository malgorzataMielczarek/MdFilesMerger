using MdFilesMerger.App.Abstract;
using MdFilesMerger.Controller.Abstract;
using MdFilesMerger.Controller.Common;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.App.Concrete;
using Newtonsoft.Json;

namespace MdFilesMerger.Controller.Concrete
{
    /// <summary>
    ///     Manager for menu action model.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseManager{T, TService}"/> -&gt; MenuActionManager
    ///         <br/><b> Implements: </b><see cref="IManager{T, TService}"/>, <see cref="IMenuActionManager"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="IMenuActionService"> MdFilesMerger.App.Abstract.IMenuActionService </seealso>
    /// <seealso cref="IManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IMenuActionManager"> MdFilesMerger.Controller.Abstract.IMenuActionManager </seealso>
    /// <seealso cref="BaseManager{T, TService}">
    ///     MdFilesMerger.Controller.Common.BaseManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IMenuAction"> MdFilesMerger.Domain.Abstract.IMenuAction </seealso>
    public sealed class MenuActionManager : BaseManager<IMenuAction, IMenuActionService>, IMenuActionManager
    {
        private string _lang;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MenuActionManager"/> class and all it's properties.
        /// </summary>
        /// <remarks>
        ///     Sets <see cref="BaseManager{T, TService}.Service"/> to newly created <see
        ///     cref="MenuActionService"/> object and <see cref="BaseManager{T,
        ///     TService}.SelectedItem"/> to <see langword="2"/> ("DisplayMainMenu" menu action).
        /// </remarks>
        public MenuActionManager(string lang = "EN") : base(new MenuActionService(lang))
        {
            SelectedItem = 2;
            _lang = lang;
        }

        /// <summary>
        ///     Displays appropriate manager title, based on displayed menu (selected items <see
        ///     cref="IMenuAction.NextMenu"/> vale).
        /// </summary>
        public override void DisplayTitle()
        {
            MenuType menuType = Service.ReadById(SelectedItem)?.NextMenu ?? MenuType.Main;

            string langPath = File.ReadAllText("langPath.txt");
            string fileName = "menuTitle" + _lang + ".txt";
            using StreamReader sr = File.OpenText(Path.Combine(langPath, fileName));
            using JsonReader reader = new JsonTextReader(sr);
            JsonSerializer serializer = new JsonSerializer();
            var titles = serializer.Deserialize<Dictionary<MenuType, string>>(reader);
            string title = titles?.GetValueOrDefault(menuType) ?? string.Empty;

            DisplayTitle(title);
        }

        /// <inheritdoc/>
        public void EnterOrEsc()
        {
            Console.Write("Wciśnij Enter aby wrócić do menu głównego lub Esc by zakończyć program.");

            var key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
            {
                SelectedItem = 1;
            }
            else
            {
                SelectedItem = 2;
            }
        }

        /// <inheritdoc/>
        public void GoToMainMenu()
        {
            SelectedItem = 2;
        }

        /// <inheritdoc/>
        public IMenuAction? Select()
        {
            Select(0);

            return Service.ReadById(SelectedItem);
        }

        /// <summary>
        ///     Gets menu actions that belong to specified menu.
        /// </summary>
        /// <param name="connectedItemId">
        ///     The value of this parameter doesn't matter in this implementation. It has set
        ///     default value, so it can be omitted all together.
        /// </param>
        /// <returns>
        ///     List of menu action objects with <see cref="IMenuAction.Menu"/> value equal <see
        ///     cref="IMenuAction.NextMenu"/> value of currently selected item or <see
        ///     cref="MenuType.Main"/> if no object is currently selected.
        /// </returns>
        protected override List<IMenuAction> GetFilteredList(int connectedItemId = 0)
        {
            MenuType menuType = Service.ReadById(SelectedItem)?.NextMenu ?? MenuType.Main;

            return Service.ReadByMenuType(menuType);
        }

        /// <inheritdoc/>
        protected override void SelectItem(IReadOnlyList<IMenuAction> list)
        {
            if (list.Count > 0)
            {
                var cursor = Console.GetCursorPosition();
                while (true)
                {
                    Console.Write("Podaj numer czynności, którą chcesz wykonać, wciśnij Esc aby zakończyć działanie programu");

                    if (list[0].Menu != MenuType.Main)
                    {
                        Console.Write(" lub Enter, aby wrócić do menu głównego");
                    }

                    Console.Write(": ");

                    var key = Console.ReadKey();

                    SelectedItem = key.Key switch
                    {
                        ConsoleKey.Escape => 1,
                        ConsoleKey.Enter => 2,
                        >= ConsoleKey.D1 and <= ConsoleKey.D9 => GetIdByIndex(key.Key - ConsoleKey.D1, list),
                        _ => -1
                    };

                    if (SelectedItem == -1)
                    {
                        Console.SetCursorPosition(cursor.Left, cursor.Top);
                        Console.WriteLine("Nie rozpoznano wybranej czynności. Wybierz ponownie co chcesz zrobić.");
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}