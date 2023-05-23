using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.Domain.Entity
{
    public class MenuAction : BaseItem, IMenuAction
    {
        public MenuType Menu { get; set; }

        public MenuAction(string description, MenuType menu) : base(0, description)
        {
            Menu = menu;
        }

        public MenuAction(int id, string description, MenuType menu) : base(id, description)
        {
            Menu = menu;
        }
    }
}
