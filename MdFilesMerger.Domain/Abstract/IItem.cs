namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    /// Base iterface for all models.
    /// </summary>
    public interface IItem
    {
        /// <value>Distinct identification number of element</value>
        public int Id { get; set; }

        /// <value>Description of element. It will be user name or path of directory/file</value>
        public string? Name { get; set; }
    }
}
