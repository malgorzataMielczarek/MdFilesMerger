using MdFilesMerger.App.Abstract;
using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.App.Common
{
    public class BaseCRUDService<T> : BaseService<T>, ICRUDService<T> where T : BaseItem
    {
        public int RemoveItem(T item)
        {
            _items.Remove(item);

            return item.Id;
        }

        public int UpdateItem(T item)
        {
            for (int i = item.Id; i < _items.Count; i++)
            {
                if (_items[i].Id == item.Id)
                {
                    _items[i] = item;
                    return item.Id;
                }
            }

            for (int i = item.Id - 1; i >= 0; i--)
            {
                if (_items[i].Id == item.Id)
                {
                    _items[i] = item;
                    return item.Id;
                }
            }

            return -1;
        }

    }
}
