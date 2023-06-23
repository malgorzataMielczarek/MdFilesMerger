using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.App.Abstract
{
    /// <summary>
    ///     Interface to service collection of <see cref="IMainDirectory"/> objects.
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
    /// <seealso cref="IMainDirectory"> MdFilesMerger.Domain.Abstract.IMainDirectory </seealso>
    public interface IMainDirectoryService : IDirectoryService<IMainDirectory>
    {
        /// <summary>
        ///     Finds all .md files in specified main directory and it's subdirectories.
        /// </summary>
        /// <param name="id">
        ///     The identifier of <see cref="IMainDirectory"/> object associated with directory
        ///     searched for .md files.
        /// </param>
        /// <returns> Collection of found .md files. </returns>
        IEnumerable<FileInfo> FindAllFiles(int id);

        /// <summary>
        ///     Finds all .md files in specified main directory and it's subdirectories.
        /// </summary>
        /// <param name="mainDirectory">
        ///     The <see cref="IMainDirectory"/> object associated with directory searched for .md files.
        /// </param>
        /// <returns> Collection of found .md files. </returns>
        IEnumerable<FileInfo> FindAllFiles(IMainDirectory mainDirectory);

        /// <summary>
        ///     Gets, from the stored collection of <see cref="IMainDirectory"/> objects, elements
        ///     with the specified merged file identifier.
        /// </summary>
        /// <param name="mergedFileId"> The searched merged file identifier. </param>
        /// <returns>
        ///     The list of <see cref="IMainDirectory"/> objects with the specified merged file
        ///     identifier. If there is no objects with <see cref="IMainDirectory.MergedFileId"/>
        ///     equal <paramref name="mergedFileId"/>, returned list is empty.
        /// </returns>
        List<IMainDirectory> ReadByMergedFileId(int mergedFileId);
    }
}