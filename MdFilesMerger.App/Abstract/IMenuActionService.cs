using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.App.Abstract
{
    /// <summary>
    ///     Interface with functionalities to get menu actions.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IService{T}"/> -&gt; IMenuActionService <br/><b>
    ///         Implementation: </b><see cref="Concrete.MenuActionService"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="Concrete.MenuActionService"> MdFilesMerger.App.Concrete.MenuActionService </seealso>
    public interface IMenuActionService : IService<MenuAction>
    {
        /// <summary>
        ///     Gets elements from <see cref="Concrete.MenuActionService._actions"/> with <see
        ///     cref="MenuAction.Menu"/><paramref name="menuType"/>.
        /// </summary>
        /// <param name="menuType"> Type of the menu. </param>
        /// <returns> List with object of menu <paramref name="menuType"/>. </returns>
        public List<MenuAction> ReadByMenuType(MenuType menuType);
    }
}