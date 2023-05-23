using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.App.Abstract
{
    public interface ICRUDService<T> : IService<T> where T : IItem
    {
        public int RemoveItem(T item);
        public int UpdateItem(T item);
    }
}
