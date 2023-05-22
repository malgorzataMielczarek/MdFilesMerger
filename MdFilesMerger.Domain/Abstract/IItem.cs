namespace MdFilesMerger.Domain.Abstract
{
    public interface IItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
