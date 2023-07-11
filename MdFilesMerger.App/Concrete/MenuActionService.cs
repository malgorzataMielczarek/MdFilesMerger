using MdFilesMerger.App.Abstract;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

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
    /// <seealso cref="IMenuAction"> MdFilesMerger.Domain.Abstract.IMenuAction </seealso>
    /// <seealso cref="MenuAction"> MdFilesMerger.Domain.Entity.MenuAction </seealso>
    public sealed class MenuActionService : IMenuActionService
    {
        private readonly List<IMenuAction> _actions;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MenuActionService"/> class and fills
        ///     <see cref="_actions"/> list.
        /// </summary>
        public MenuActionService(string lang = "EN")
        {
            _actions = new List<IMenuAction>();
            Initialization(lang);
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
        public IMenuAction? ReadById(int id)
        {
            return _actions.FirstOrDefault(a => a.Id == id);
        }

        /// <inheritdoc/>
        public List<IMenuAction> ReadByMenuType(MenuType menuType)
        {
            List<IMenuAction> result = new List<IMenuAction>();

            foreach (var item in _actions)
            {
                if (menuType == item.Menu)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        private void Initialization(string lang = "EN")
        {
            _actions.Add(new MenuAction(1, "Exit", 0, 0));
            _actions.Add(new MenuAction(2, "DisplayMainMenu", 0, MenuType.Main));

            string dirPath = File.ReadAllText("langPath.txt");
            string fileName = "menu" + lang.ToUpper() + ".txt";
            using StreamReader sr = File.OpenText(Path.Combine(dirPath, fileName));
            using JsonReader reader = new JsonTextReader(sr);

            JsonSerializer serializer = new JsonSerializer();
            var list = serializer.Deserialize<List<MenuAction>>(reader);

            if (list != null)
            {
                _actions.AddRange(list);
            }
        }
    }
}