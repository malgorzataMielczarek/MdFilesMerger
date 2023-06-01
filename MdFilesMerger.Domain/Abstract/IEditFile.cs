namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    ///     Base interface for .md files related models with update option (for example with
    ///     property <see cref="Title"/> that can be updated - merged and selected file models).
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IItem"/> -&gt; <see cref="IDirectory"/> -&gt;
    ///         IEditFile <br/><b> Implementations: </b><see cref="Entity.SelectedFile"/>, <see cref="Entity.MergedFile"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="IDirectory"> MdFilesMerger.Domain.Abstract.IDirectory </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    /// <seealso cref="Entity.MergedFile"> MdFilesMerger.Domain.Entity.MergedFile </seealso>
    /// <seealso cref="Entity.SelectedFile"> MdFilesMerger.Domain.Entity.SelectedFile </seealso>
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