using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.Domain.Entity
{
    /// <summary>
    ///     Representation of menu item.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseItem"/> -&gt; MenuAction <br/><b> Implements:
    ///         </b><see cref="IItem"/>, <see cref="IMenuAction"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="BaseItem"> MdFilesMerger.Domain.Entity.BaseItem </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    /// <seealso cref="IMenuAction"> MdFilesMerger.Domain.Abstract.IMenuAction </seealso>
    public sealed class MenuAction : BaseItem, IMenuAction
    {
        public MenuAction()
        {
        }

        /// <summary>
        ///     Sets <see cref="BaseItem.Id"> Id </see> to <see langword="0"/>, <see
        ///     cref="BaseItem.Name"> Name </see> to <paramref name="description"/>, <see
        ///     cref="Menu"> Menu </see> to <paramref name="menu"/> and <see cref="NextMenu">
        ///     NextMenu </see> to <paramref name="nextMenu"/>.
        /// </summary>
        /// <param name="description"> Description of action that will by displayed in menu. </param>
        /// <param name="menu"> Type of menu that this item is a part of. </param>
        /// <param name="nextMenu">
        ///     Menu that will be displayed after action described by this item will be performed.
        /// </param>
        public MenuAction(string description, MenuType menu, MenuType nextMenu) : base(0, description)
        {
            Menu = menu;
            NextMenu = nextMenu;
        }

        /// <summary>
        ///     Sets <see cref="BaseItem.Id"> Id </see> to <paramref name="id"/>, <see
        ///     cref="BaseItem.Name"> Name </see> to <paramref name="description"/>, <see
        ///     cref="Menu"> Menu </see> to <paramref name="menu"/> and <see cref="NextMenu">
        ///     NextMenu </see> to <paramref name="nextMenu"/>.
        /// </summary>
        /// <param name="id"> Unique identification number of action. </param>
        /// <param name="description"> Description of action that will by displayed in menu. </param>
        /// <param name="menu"> Type of menu that this item is a part of. </param>
        /// <param name="nextMenu">
        ///     Menu that will be displayed after action described by this item will be performed.
        /// </param>
        public MenuAction(int id, string description, MenuType menu, MenuType nextMenu) : base(id, description)
        {
            Menu = menu;
            NextMenu = nextMenu;
        }

        /// <inheritdoc/>
        public MenuType Menu { get; set; }

        /// <inheritdoc/>
        public MenuType NextMenu { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj != null && obj is IMenuAction other)
            {
                return this.Name == other.Name && this.Menu == other.Menu;
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode() => base.GetHashCode();
    }
}