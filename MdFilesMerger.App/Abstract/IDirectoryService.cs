using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.App.Abstract
{
    /// <summary>
    ///     Base interface to service collection of entities implementing <see cref="IDirectory"/> interface.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IService{T}"/> -&gt; <see cref="ICRUDService{T}"/>
    ///         -&gt; IDirectoryService&lt;T&gt; <br/><b> Implementations: </b><see
    ///          cref="Common.BaseDirectoryService{T}"/>, <see cref="Common.EditFileService{T}"/>,
    ///          <see cref="Common.RelativeFileService{T}"/>, <see
    ///          cref="Concrete.IgnoredFileService"/>, <see cref="Concrete.MainDirectoryService"/>
    ///     </para>
    /// </summary>
    /// <typeparam name="T"> Class implementing <see cref="IDirectory"/> interface. </typeparam>
    /// <seealso cref="ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="Common.BaseDirectoryService{T}"> MdFilesMerger.App.Common.BaseDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="Common.EditFileService{T}"> MdFilesMerger.App.Common.EditFileService&lt;T&gt; </seealso>
    /// <seealso cref="Common.RelativeFileService{T}"> MdFilesMerger.App.Common.RelativeFileService&lt;T&gt; </seealso>
    /// <seealso cref="Concrete.IgnoredFileService"> MdFilesMerger.App.Concrete.IgnoredFileService </seealso>
    /// <seealso cref="Concrete.MainDirectoryService"> MdFilesMerger.App.Concrete.MainDirectoryService </seealso>
    /// <seealso cref="IDirectory"> MdFilesMerger.Domain.Abstract.IDirectory </seealso>
    public interface IDirectoryService<T> : ICRUDService<T> where T : IDirectory
    {
        /// <summary>
        ///     Updates the path of directory/file associated with the element with specified
        ///     identification number.
        /// </summary>
        /// <param name="id"> The identification number of updated element. </param>
        /// <param name="path"> The new path. </param>
        /// <returns>
        ///     Identification number of updated element or <see langword="-1"/>, if update failed.
        /// </returns>
        public int UpdatePath(int id, string path);
    }
}