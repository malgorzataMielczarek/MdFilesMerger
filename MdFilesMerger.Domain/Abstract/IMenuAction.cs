using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.Domain.Abstract
{
    public interface IMenuAction : IItem
    {
        public MenuType Menu { get; set; }
    }
}
