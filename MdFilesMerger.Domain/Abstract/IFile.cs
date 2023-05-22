namespace MdFilesMerger.Domain.Abstract
{
    public interface IFile : IItem, IComparable<IFile>
    {
        public string? GetFileName();
        public string? GetFullPath();
        public string? GetParentDirectory();
        public bool SetFileName(string fileName);
        public bool SetFullPath(string? path);
        public bool SetParentDirectory(string directoryPath);
    }
}
