using MdFilesMerger.App.Abstract;
using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.App.Common
{
    /// <summary>
    ///     Base implementation of <see cref="IDirectoryService{T}"/> for <see
    ///     cref="BaseDirectory"/> objects.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseService{T}"/> -&gt;
    ///         BaseDirectoryService&lt;T&gt; <br/><b> Implements: </b><see
    ///         cref="ICRUDService{T}"/>, <see cref="IDirectoryService{T}"/>, <see cref="IService{T}"/>
    ///     </para>
    /// </summary>
    /// <typeparam name="T"> </typeparam>
    /// <seealso cref="ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    /// <seealso cref="IDirectoryService{T}"> MdFilesMerger.App.Abstract.IDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="BaseService{T}"> MdFilesMerger.App.Common.BaseService&lt;T&gt; </seealso>
    /// <seealso cref="BaseDirectory"> MdFilesMerger.Domain.Common.BaseDirectory </seealso>
    public class BaseDirectoryService<T> : BaseService<T>, IDirectoryService<T> where T : BaseDirectory
    {
        /// <inheritdoc/>
        public int UpdatePath(int id, string path)
        {
            T? item = ReadById(id);

            if (item != null)
            {
                string? oldPath = item.Name;
                if (item.SetPath(path))
                {
                    return item.Id;
                }
                else
                {
                    item.Name = oldPath;
                }
            }

            return -1;
        }
    }
}