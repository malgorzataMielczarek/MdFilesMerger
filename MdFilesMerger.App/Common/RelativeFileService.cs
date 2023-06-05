using MdFilesMerger.App.Abstract;
using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.App.Common
{
    /// <summary>
    ///     Implementation of additional methods for handling collection of objects associated with
    ///     .md files, whose paths are stored as relative to connected main directory path.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseService{T}"/> -&gt; <see cref="BaseDirectoryService{T}"/>
    ///         -&gt; RelativeFileService&lt;T&gt; <br/><b> Implements: </b><see
    ///          cref="ICRUDService{T}"/>, <see cref="IDirectoryService{T}"/>, <see
    ///          cref="IRelativeFileService{T}"/>, <see cref="IService{T}"/>
    ///     </para>
    /// </summary>
    /// <typeparam name="T"> <see cref="RelativeFile"/> or type derived from it. </typeparam>
    /// <seealso cref="ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    /// <seealso cref="IDirectoryService{T}"> MdFilesMerger.App.Abstract.IDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="IRelativeFileService{T}"> MdFilesMerger.App.Abstract.IRelativeFileService&lt;T&gt; </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="BaseDirectoryService{T}"> MdFilesMerger.App.Common.BaseDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="BaseService{T}"> MdFilesMerger.App.Common.BaseService&lt;T&gt; </seealso>
    /// <seealso cref="RelativeFile"> MdFilesMerger.Domain.Common.RelativeFile </seealso>
    public class RelativeFileService<T> : BaseDirectoryService<T>, IRelativeFileService<T> where T : RelativeFile
    {
        /// <inheritdoc/>
        public List<T> ReadByMainDirId(int mainDirId)
        {
            List<T> list = new List<T>();

            foreach (var file in _items)
            {
                if (file.MainDirId == mainDirId)
                {
                    list.Add(file);
                }
            }

            list.Sort();

            return list;
        }

        /// <inheritdoc/>
        public int UpdatePath(int id, string path, string mainDirPath)
        {
            T? file = ReadById(id);

            if (file != null)
            {
                string? oldPath = file.Name;
                if (file.SetPath(path, mainDirPath))
                {
                    return file.Id;
                }
                else
                {
                    file.Name = oldPath;
                }
            }

            return -1;
        }
    }
}