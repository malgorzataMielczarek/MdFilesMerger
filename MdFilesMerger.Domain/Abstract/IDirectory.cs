namespace MdFilesMerger.Domain.Abstract
{
    public interface IDirectory : IFile
    {
        public int UserId { get; set; }
    }
}
