namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    ///     Interface of main directory model
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IItem"/> -&gt; <see cref="IDirectory"/> -&gt;
    ///         IMainDirectory <br/><b> Implementations: </b><see cref="Entity.MainDirectory"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    /// <seealso cref="IDirectory"> MdFilesMerger.Domain.Abstract.IDirectory </seealso>
    /// <seealso cref="Entity.MainDirectory"> MdFilesMerger.Domain.Entity.MainDirectory </seealso>
    public interface IMainDirectory : IDirectory
    {
        /// <summary>
        ///     Identification number of merged file associated with this main directory instance
        /// </summary>
        int MergedFileId { get; set; }
    }
}