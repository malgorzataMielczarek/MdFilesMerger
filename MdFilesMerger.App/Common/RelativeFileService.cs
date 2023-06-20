using MdFilesMerger.App.Abstract;
using MdFilesMerger.App.Concrete;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;

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
    /// <seealso cref="IRelativeFile"> MdFilesMerger.Domain.Abstract.IRelativeFile </seealso>
    /// <seealso cref="RelativeFile"> MdFilesMerger.Domain.Common.RelativeFile </seealso>
    public class RelativeFileService<T> : BaseDirectoryService<T>, IRelativeFileService<T> where T : class, IRelativeFile
    {
        /// <summary>
        ///     The main directory service.
        /// </summary>
        protected readonly IMainDirectoryService _mainDirService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RelativeFileService{T}"/> class.
        /// </summary>
        /// <param name="mainDirService"> The main directory service. </param>
        public RelativeFileService(IMainDirectoryService mainDirService)
        {
            _mainDirService = mainDirService;
        }

        /// <inheritdoc/>
        public List<FileInfo> FindNewFiles(IEnumerable<FileInfo> list, IMainDirectory mainDirectory)
        {
            var files = new List<FileInfo>();
            if (!string.IsNullOrWhiteSpace(mainDirectory?.Name))
            {
                foreach (var file in list)
                {
                    if (!string.IsNullOrWhiteSpace(file?.Name))
                    {
                        T? item = Activator.CreateInstance(typeof(T), file, mainDirectory) as T;
                        if (GetEqual(item) == null)
                        {
                            files.Add(file);
                        }
                    }
                }
            }

            return files;
        }

        /// <inheritdoc/>
        public string? GetFullPath(int id)
        {
            return GetFullPath(ReadById(id));
        }

        /// <inheritdoc/>
        public string? GetFullPath(T? relativeFile)
        {
            if (relativeFile != null)
            {
                IMainDirectory? mainDirectory = _mainDirService.ReadById(relativeFile.MainDirId);

                return relativeFile.GetPath(mainDirectory?.GetPath());
            }

            return null;
        }

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

        /// <summary>
        ///     Updates the relative path of .md file associated with the element with specified
        ///     identification number.
        /// </summary>
        /// <param name="id"> The identification number of updated element. </param>
        /// <param name="path">
        ///     Absolute or relative (to current directory) path to existing .md file, that you want
        ///     to associate with the element being updated.
        /// </param>
        /// <returns>
        ///     Identification number of updated element or <see langword="-1"/>, if update failed.
        /// </returns>
        public override int UpdatePath(int id, string path)
        {
            T? file = ReadById(id);

            if (file != null)
            {
                string? oldPath = file.Name;
                if (file.SetPath(path, _mainDirService.ReadById(file.MainDirId)?.GetPath()))
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