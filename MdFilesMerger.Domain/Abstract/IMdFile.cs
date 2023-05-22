namespace MdFilesMerger.Domain.Abstract
{
    public interface IMdFile : IFile
    {
        public int MainDirId { get; set; }
    }
}
