namespace MdFilesMerger.Domain.Abstract
{
    public interface IUser : IItem
    {
        public string? Password { get; set; }
    }
}
