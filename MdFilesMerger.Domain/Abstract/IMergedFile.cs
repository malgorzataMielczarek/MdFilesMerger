using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.Domain.Abstract
{
    public interface IMergedFile : ISelectedFile, IDirectory
    {
        public string NewLineStyle { get; set; }
        public TableOfContent TableOfContent { get; set; }
        public string? TOCHeader { get; set; }
    }
}
