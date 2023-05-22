using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.Controller.Common
{
    public interface IMenager<T> where T : IItem
    {
        public T? GetChangedValue();
        public MenuAction PerformAction();
    }
}
