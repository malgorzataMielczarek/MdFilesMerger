namespace MdFilesMerger.Domain.Abstract
{
    public interface ISelectedFile : IMdFile
    {
        public DateTime ModifiedDate { get; set; }
        public string? Title { get; set; }
    }
}
