namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    ///     Base interface for all models.
    /// </summary>
    public interface IItem
    {
        /// <summary>
        ///     Distinct identification number of item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Description of item. It will be description of menu element, user name or path of directory/file.
        /// </summary>
        public string? Name { get; set; }
    }
}