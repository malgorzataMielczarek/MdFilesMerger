using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    ///     Interface for menu action item.
    /// </summary>
    public interface IMenuAction : IItem
    {
        /// <summary>
        ///     Value assigning item to specific menu.
        /// </summary>
        public MenuType Menu { get; set; }

        /// <summary>
        ///     Says what menu will be displayed when performing action described by this item will
        ///     by finish.
        /// </summary>
        public MenuType NextMenu { get; set; }
    }
}