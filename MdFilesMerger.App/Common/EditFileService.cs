using MdFilesMerger.App.Abstract;
using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.App.Common
{
    /// <summary>
    ///     Implementation of additional methods for handling collection of entities implementing
    ///     <see cref="IEditFile"/> interface.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseService{T}"/> -&gt; <see cref="BaseDirectoryService{T}"/>
    ///         -&gt; EditFileService&lt;T&gt; <br/><b>
    ///         Implements: </b><see cref="ICRUDService{T}"/>, <see cref="IDirectoryService{T}"/>,
    ///                     <see cref="IEditFileService{T}"/>, <see cref="IService{T}"/>
    ///     </para>
    /// </summary>
    /// <typeparam name="T"> Class implementing <see cref="IEditFile"/> interface. </typeparam>
    /// <seealso cref="ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    /// <seealso cref="IDirectoryService{T}"> MdFilesMerger.App.Abstract.IDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="IEditFileService{T}"> MdFilesMerger.App.Abstract.IEditFileService&lt;T&gt; </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="BaseDirectoryService{T}"> MdFilesMerger.App.Common.BaseDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="BaseService{T}"> MdFilesMerger.App.Common.BaseService&lt;T&gt; </seealso>
    /// <seealso cref="IEditFile"> MdFilesMerger.Domain.Abstract.IEditFile </seealso>
    public class EditFileService<T> : BaseDirectoryService<T>, IEditFileService<T> where T : class, IEditFile
    {
        /// <inheritdoc/>
        public int UpdateTitle(int id, string? title)
        {
            T? file = ReadById(id);

            if (file != null)
            {
                file.Title = title;
                file.ModifiedDate = DateTime.Now;

                return file.Id;
            }

            return -1;
        }
    }
}