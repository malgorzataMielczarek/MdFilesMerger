using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.Domain.Common
{
    public class BaseItem : IItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public BaseItem()
        {

        }

        public BaseItem(int id)
        {
            Id = id;
        }

        public BaseItem(int id, string? name) : this(id)
        {
            Name = name;
        }
    }
}
