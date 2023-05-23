using MdFilesMerger.Controller.Common;

namespace MdFilesMerger.Controller.Abstract
{
    public interface ICRUDManager : IManager
    {
        public int AddItem();
        public int RemoveItem(int id);
        public int UpdateItem();
    }
}
