namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    ///     Interface of main directory model
    /// </summary>
    public interface IMainDirectory : IDirectory
    {
        /// <summary>
        ///     Identification number of merged file associated with this main directory instance
        /// </summary>
        public int MergedFileId { get; set; }
    }
}