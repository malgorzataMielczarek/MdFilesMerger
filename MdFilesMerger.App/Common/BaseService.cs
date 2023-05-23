using MdFilesMerger.App.Abstract;
using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.App.Common
{
    public class BaseService<T> : IService<T> where T : BaseItem
    {
        protected List<T> _items;

        public BaseService()
        {
            _items = new List<T>();
        }

        public BaseService(List<T> items)
        {
            _items = items;
        }

        public virtual int AddItem(T item)
        {
            if (item.Id == 0 || GetItemById(item.Id) != null)
            {
                item.Id = GetNewId();
            }

            _items.Add(item);

            return item.Id;
        }

        // returns id of first added element
        public int AddRange(List<T> items)
        {
            int id = -1;
            foreach (var item in items)
            {
                AddItem(item);

                if (id == -1)
                {
                    id = item.Id;
                }
            }

            return id;
        }

        public T? GetItemById(int id)
        {
            foreach (var item in _items)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }

        public IReadOnlyList<T> GetItemsByName(string name)
        {
            List<T> list = new List<T>();
            foreach (var item in _items)
            {
                if (item.Name == name)
                    list.Add(item);
            }

            return list;
        }

        public IReadOnlyList<T> GetAllItems()
        {
            return _items;
        }

        public virtual int GetNewId()
        {
            int newId = 0;

            foreach (var item in _items)
            {
                if (item.Id > newId)
                {
                    newId = item.Id;
                }
            }

            newId++;

            return newId;
        }
    }
}
