using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.Domain.Entity
{
    public class MainDirectory : BaseFile, IDirectory
    {
        public int UserId { get; set; }

        public MainDirectory(): base()
        {

        }

        public MainDirectory(int id) : base(id)
        {

        }

        public override bool SetFullPath(string? path)
        {
            path = path?.Trim();

            if (Directory.Exists(path))
            {
                Name = Path.GetFullPath(path).Replace('\\', '/');
                return true;
            }

            else
            {
                Name = null;
                return false;
            }
        }
    }
}
