using MdFilesMerger.App.Abstract;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.App.Concrete
{
    /// <summary>
    ///     Class creating all <see cref="MenuAction"/> objects and implementing methods to get them.
    ///     <para>
    ///         <b> Inheritance: </b> MenuActionService <br/><b> Implements: </b><see
    ///         cref="IMenuActionService"/>, <see cref="IService{T}"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="IMenuActionService"> MdFilesMerger.App.Abstract.IMenuActionService </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    public sealed class MenuActionService : IMenuActionService
    {
        private readonly List<MenuAction> _actions;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MenuActionService"/> class and fills
        ///     <see cref="_actions"/> list.
        /// </summary>
        public MenuActionService()
        {
            _actions = new List<MenuAction>();
            Initialization();
        }

        /// <summary>
        ///     Gets the item from <see cref="Concrete.MenuActionService._actions"/> by
        ///     identification number ( <see cref="BaseItem.Id"/>).
        /// </summary>
        /// <param name="id"> The identification number of element that you are looking for. </param>
        /// <returns>
        ///     <see cref="MenuAction"/> object with <see cref="BaseItem.Id"/> equal <paramref
        ///     name="id"/>, if there is one, otherwise <see langword="null"/>.
        /// </returns>
        public MenuAction? ReadById(int id)
        {
            if (_actions.Count >= id && _actions[id - 1].Id == id)
            {
                return _actions[id - 1];
            }

            return null;
        }

        /// <inheritdoc/>
        public List<MenuAction> ReadByMenuType(MenuType menuType)
        {
            List<MenuAction> result = new List<MenuAction>();

            foreach (var item in _actions)
            {
                if (menuType == item.Menu)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        private void Add(string description, MenuType menu, MenuType nextMenu)
        {
            int id = _actions.Count + 1;
            _actions.Add(new MenuAction(id, description, menu, nextMenu));
        }

        private void Initialization()
        {
            _actions.Add(new MenuAction(1, "Exit", 0, 0));
            _actions.Add(new MenuAction(2, "DisplayMainMenu", 0, MenuType.Main));

            Add("Zmień katalog główny", MenuType.Main, MenuType.Main);
            Add("Wyświetl listę plików do scalenia", MenuType.Main, MenuType.Main);
            Add("Utwórz spis treści", MenuType.Main, MenuType.TableOfContents);
            Add("Scal pliki", MenuType.Main, MenuType.MergedFile);

            Add("Spis treści będący zwykłym tekstem", MenuType.TableOfContents, MenuType.Main);
            Add("Spis treści złożony z hiperlinków do odpowiednich paragrafów", MenuType.TableOfContents, MenuType.Main);
            Add("Bez spisu treści", MenuType.TableOfContents, MenuType.Main);

            Add("Zmień nazwę tworzonego pliku", MenuType.MergedFile, MenuType.MergedFile);
            Add("Zmień ścieżkę katalogu", MenuType.MergedFile, MenuType.MergedFile);
            Add("Zmień nagłówek", MenuType.MergedFile, MenuType.MergedFile);
        }
    }
}