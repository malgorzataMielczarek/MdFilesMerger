using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.App.Abstract
{
    public interface IService<T> where T : IItem
    {
        public int AddItem(T item);
        public int AddRange(List<T> items);
        public T? GetItemById(int id);
        public IReadOnlyList<T> GetItemsByName(string name);
        public IReadOnlyList<T> GetAllItems();
        public int GetNewId();
    }
}
