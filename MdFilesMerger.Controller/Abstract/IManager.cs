using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.Controller.Common
{
    public interface IManager<T> where T : IItem
    {
        public int AddItem();
        public void DisplayItems(IReadOnlyList<T> items);
        public void DisplayMenu(IReadOnlyList<MenuAction> actions);
        public int RemoveItem(int id);
        public int SelectAction(IReadOnlyList<MenuAction> actions);
        public int SelectItem(IReadOnlyList<T> items);
        public int UpdateItem();
    }
}
