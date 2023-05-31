namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    ///     Base interface for .md files related models with update option (for example with
    ///     property <see cref="Title"/> that can be updated - merged and selected file models).
    /// </summary>
    public interface IEditFile : IDirectory
    {
        /// <summary>
        ///     Date and time of last update or creation if entry wasn't modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        ///     Title (header) of the file.
        /// </summary>
        public string? Title { get; set; }
    }
}