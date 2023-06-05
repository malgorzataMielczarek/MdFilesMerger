namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    ///     Base interface for models related with .md files involved in the merge (merged and
    ///     selected file models).
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
        ///     Title (header) of the file.
        /// </summary>
        public string? Title { get; set; }
    }
}