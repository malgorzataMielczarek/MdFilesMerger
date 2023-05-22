using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.Domain.Common
{
    public class MdFile : BaseFile, IMdFile
    {
        public int MainDirId { get; set; }

        public MdFile()
        {

        }

        public MdFile(int id, int mainDirectoryId) : base(id)
        {
            MainDirId = mainDirectoryId;
        }
    }
}
