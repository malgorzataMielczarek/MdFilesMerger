using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.Domain.Entity
{
    public class MenuAction : BaseItem, IMenuAction
    {
        public MenuType Menu { get; set; }

        public MenuAction(int id, string actionName, MenuType menu) : base(id, actionName)
        {
            Menu = menu;
        }
    }
}
