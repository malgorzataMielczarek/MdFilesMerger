using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.App.Abstract
{
    /// <summary>
    ///     Interface to service collection of <see cref="MainDirectory"/> objects.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IService{T}"/> -&gt; <see cref="ICRUDService{T}"/>
    ///         -&gt; <see cref="IDirectoryService{T}"/> -&gt; IMainDirectoryService <br/><b>
    ///         Implementations: </b><see cref="Concrete.MainDirectoryService"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    /// <seealso cref="IDirectoryService{T}"> MdFilesMerger.App.Abstract.IDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="Concrete.MainDirectoryService"> MdFilesMerger.App.Concrete.MainDirectoryService </seealso>
    /// <seealso cref="MainDirectory"> MdFilesMerger.Domain.Entity.MainDirectory </seealso>
    public interface IMainDirectoryService : IDirectoryService<MainDirectory>
    {
        /// <summary>
        ///     Finds all .md files in specified main directory and it's subdirectories.
        /// </summary>
        /// <param name="id">
        ///     The identifier of <see cref="MainDirectory"/> object associated with directory
        ///     searched for .md files.
        /// </param>
        /// <returns> Collection of found .md files. </returns>
        IEnumerable<FileInfo> FindAllFiles(int id);

        /// <summary>
        ///     Finds all .md files in specified main directory and it's subdirectories.
        /// </summary>
        /// <param name="mainDirectory">
        ///     The <see cref="MainDirectory"/> object associated with directory searched for .md files.
        /// </param>
        /// <returns> Collection of found .md files. </returns>
        IEnumerable<FileInfo> FindAllFiles(MainDirectory mainDirectory);

        /// <summary>
        ///     Gets, from the stored collection of <see cref="MainDirectory"/> objects, elements
        ///     with the specified merged file identifier.
        /// </summary>
        /// <param name="mergedFileId"> The searched merged file identifier. </param>
        /// <returns>
        ///     The list of <see cref="MainDirectory"/> objects with the specified merged file
        ///     identifier. If there is no objects with <see cref="MainDirectory.MergedFileId"/>
        ///     equal <paramref name="mergedFileId"/>, returned list is empty.
        /// </returns>
        public List<MainDirectory> ReadByMergedFileId(int mergedFileId);
    }
}