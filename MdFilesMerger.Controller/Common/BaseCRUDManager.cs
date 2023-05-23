using MdFilesMerger.App.Common;
using MdFilesMerger.Controller.Abstract;
using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.Controller.Common
{
    public abstract class BaseCRUDManager<T> : BaseManager<T>, ICRUDManager where T : BaseItem
    {
        protected BaseCRUDManager(BaseService<T> service) : base(service)
        {
        }

        public abstract int AddItem();

        public virtual int RemoveItem(int id)
        {
            T? item = _service.GetItemById(id);

            if (item != null)
            {
                return _service.RemoveItem(item);
            }

            else
            {
                return -1;
            }
        }

        public abstract int UpdateItem();
    }
}
