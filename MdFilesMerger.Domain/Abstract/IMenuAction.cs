using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    ///     Interface for menu action item.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IItem"/> -&gt; IMenuAction <br/><b> Implementations:
    ///         </b><see cref="Entity.MenuAction"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    /// <seealso cref="Entity.MenuAction"> MdFilesMerger.Domain.Entity.MenuAction </seealso>
    public interface IMenuAction : IItem
    {
        /// <summary>
        ///     Value assigning item to specific menu.
        /// </summary>
        MenuType Menu { get; set; }

        /// <summary>
        ///     Says what menu will be displayed when performing action described by this item will
        ///     by finish.
        /// </summary>
        MenuType NextMenu { get; set; }
    }
}