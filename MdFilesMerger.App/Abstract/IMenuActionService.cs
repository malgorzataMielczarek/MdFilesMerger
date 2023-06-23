using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Abstract;

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
    /// <seealso cref="IMenuAction"> MdFilesMerger.Domain.Abstract.IMenuAction </seealso>
    public interface IMenuActionService : IService<IMenuAction>
    {
        /// <summary>
        ///     Gets elements from collection with <see cref="IMenuAction.Menu"/> equal <paramref name="menuType"/>.
        /// </summary>
        /// <param name="menuType"> Type of the menu. </param>
        /// <returns> List with object of menu <paramref name="menuType"/>. </returns>
        List<IMenuAction> ReadByMenuType(MenuType menuType);
    }
}