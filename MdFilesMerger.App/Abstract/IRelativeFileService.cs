using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.App.Abstract
{
    /// <summary>
    ///     Interface with additional methods for handling collection of objects associated with .md
    ///     files, whose paths are stored as relative to connected main directory path.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IService{T}"/> -&gt; <see cref="ICRUDService{T}"/>
    ///         -&gt; <see cref="IDirectoryService{T}"/> -&gt; IRelativeFileService&lt;T&gt;
    ///          <br/><b> Implementations: </b><see cref="Common.RelativeFileService{T}"/>, <see
    ///          cref="Concrete.IgnoredFileService"/>, <see cref="Concrete.SelectedFileService"/>
    ///     </para>
    /// </summary>
    /// <typeparam name="T"> Type implementing <see cref="IRelativeFile"/> interface. </typeparam>
    /// <seealso cref="ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    /// <seealso cref="IDirectoryService{T}"> MdFilesMerger.App.Abstract.IDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="Common.RelativeFileService{T}"> MdFilesMerger.App.Common.RelativeFileService&lt;T&gt; </seealso>
    /// <seealso cref="Concrete.IgnoredFileService"> MdFilesMerger.App.Concrete.IgnoredFileService </seealso>
    /// <seealso cref="Concrete.SelectedFileService"> MdFilesMerger.App.Concrete.SelectedFileService </seealso>
    /// <seealso cref="IRelativeFile"> MdFilesMerger.Domain.Abstract.IRaletiveFile </seealso>
    public interface IRelativeFileService<T> : IDirectoryService<T> where T : IRelativeFile
    {
        /// <summary>
        ///     In the list of files connected with specified main directory, finds files, that
        ///     aren't associated with any element in the stored collection.
        /// </summary>
        /// <param name="list"> The list of files. </param>
        /// <param name="mainDirectory"> The main directory for files on the list. </param>
        /// <returns> List of files not associated with any element in the stored collection. </returns>
        public List<FileInfo> FindNewFiles(IEnumerable<FileInfo> list, MainDirectory mainDirectory);

        /// <summary>
        ///     Gets the full path of file associated with specified object.
        /// </summary>
        /// <param name="id"> The identifier of object. </param>
        /// <returns> Absolute path of file associated with specified object. </returns>
        public string? GetFullPath(int id);

        /// <summary>
        ///     Gets the full path of file associated with specified object.
        /// </summary>
        /// <param name="relativeFile"> The specified object. </param>
        /// <returns> Absolute path of file associated with specified object. </returns>
        public string? GetFullPath(T? relativeFile);

        /// <summary>
        ///     Gets, from the stored collection of <typeparamref name="T"/> objects, elements with
        ///     the specified main directory identifier.
        /// </summary>
        /// <param name="mainDirId"> The searched main directory identifier. </param>
        /// <returns>
        ///     The list of <typeparamref name="T"/> objects with the specified main directory
        ///     identifier. If there is no objects with <see cref="IRelativeFile.MainDirId"/> equal
        ///     <paramref name="mainDirId"/>, returned list is empty.
        /// </returns>
        public List<T> ReadByMainDirId(int mainDirId);
    }
}