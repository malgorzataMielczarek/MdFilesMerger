using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.App.Abstract
{
    /// <summary>
    ///     Interface with additional methods for handling collection of objects associated with .md
    ///     files, whose paths are stored as relative to connected main directory path.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IService{T}"/> -&gt; <see cref="ICRUDService{T}"/>
    ///         -&gt; <see cref="IDirectoryService{T}"/> -&gt; IRelativeFileService&lt;T&gt;
    ///          <br/><b> Implementations: </b><see cref="Common.RelativeFileService{T}"/>, <see cref="Concrete.IgnoredFileService"/>
    ///     </para>
    /// </summary>
    /// <typeparam name="T"> Type implementing <see cref="IRelativeFile"/> interface. </typeparam>
    /// <seealso cref="ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    /// <seealso cref="IDirectoryService{T}"> MdFilesMerger.App.Abstract.IDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="Common.RelativeFileService{T}"> MdFilesMerger.App.Common.RelativeFileService&lt;T&gt; </seealso>
    /// <seealso cref="Concrete.IgnoredFileService"> MdFilesMerger.App.Concrete.IgnoredFileService </seealso>
    /// <seealso cref="IRelativeFile"> MdFilesMerger.Domain.Abstract.IRaletiveFile </seealso>
    public interface IRelativeFileService<T> : IDirectoryService<T> where T : IRelativeFile
    {
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

        /// <summary>
        ///     Updates the relative path of .md file associated with the element with specified
        ///     identification number.
        /// </summary>
        /// <param name="id"> The identification number of updated element. </param>
        /// <param name="path">
        ///     Absolute or relative (to current directory) path to existing .md file, that you want
        ///     to associate with the element being updated.
        /// </param>
        /// <param name="mainDirPath">
        ///     Absolute or relative (to current directory) path to the base directory, associated
        ///     with the element being updated.
        /// </param>
        /// <returns>
        ///     Identification number of updated element or <see langword="-1"/>, if update failed.
        /// </returns>
        public int UpdatePath(int id, string path, string mainDirPath);
    }
}