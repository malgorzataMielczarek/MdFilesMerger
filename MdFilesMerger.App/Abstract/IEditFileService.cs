using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.App.Abstract
{
    /// <summary>
    ///     Interface with additional methods for handling collection of entities implementing <see
    ///     cref="IEditFile"/> interface.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IService{T}"/> -&gt; <see cref="ICRUDService{T}"/>
    ///         -&gt; <see cref="IDirectoryService{T}"/> -&gt; IEditFileService&lt;T&gt; <br/><b>
    ///         Implementations: </b><see cref="Common.EditFileService{T}"/>
    ///     </para>
    /// </summary>
    /// <typeparam name="T"> Class implementing <see cref="IEditFile"/> interface. </typeparam>
    /// <seealso cref="ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    /// <seealso cref="IDirectoryService{T}"> MdFilesMerger.App.Abstract.IDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="Common.EditFileService{T}"> MdFilesMerger.App.Common.EditFileService&lt;T&gt; </seealso>
    /// <seealso cref="IEditFile"> MdFilesMerger.Domain.Abstract.IEditFile </seealso>
    public interface IEditFileService<T> : IDirectoryService<T> where T : IEditFile
    {
        /// <summary>
        ///     Updates the title.
        /// </summary>
        /// <param name="id"> The identifier of object to update. </param>
        /// <param name="title"> The new title. </param>
        /// <returns>
        ///     Identification number of updated item, or <see langword="-1"/>, if update failed
        ///     (for example there are no item with specified id in the collection).
        /// </returns>
        public int UpdateTitle(int id, string? title);
    }
}