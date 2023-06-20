using MdFilesMerger.App.Abstract;
using MdFilesMerger.App.Common;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Entity;
using System.Reflection;

namespace MdFilesMerger.App.Concrete
{
    /// <summary>
    ///     Service handling private <see cref="List{T}"/> list of <see cref="MainDirectory"/>
    ///     objects associated with existing directories, whose absolute paths they store and where
    ///     .md files will be searched.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseService{T}"/> -&gt; <see
    ///         cref="BaseDirectoryService{T}"/> -&gt; MainDirectoryService <br/><b> Implements:
    ///         </b><see cref="ICRUDService{T}"/>, <see cref="IDirectoryService{T}"/>, <see
    ///         cref="IMainDirectoryService"/>, <see cref="IService{T}"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    /// <seealso cref="IDirectoryService{T}"> MdFilesMerger.App.Abstract.IDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="IMainDirectoryService"> MdFilesMerger.App.Abstract.IMainDirectoryService </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="BaseDirectoryService{T}"> MdFilesMerger.App.Common.BaseDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="BaseService{T}"> MdFilesMerger.App.Common.BaseService&lt;T&gt; </seealso>
    /// <seealso cref="IMainDirectory"> MdFilesMerger.Domain.Abstract.IMainDirectory </seealso>
    /// <seealso cref="MainDirectory"> MdFilesMerger.Domain.Entity.MainDirectory </seealso>
    public sealed class MainDirectoryService : BaseDirectoryService<IMainDirectory>, IMainDirectoryService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MainDirectoryService"/> class.
        /// </summary>
        /// <remarks>
        ///     If there is directory with default path ( <i>
        ///     "pathToThisSolutionMainDirectory/../KursZostanProgramistaASPdotNET" </i>) on the
        ///     disk, creates associated with it <see cref="MainDirectory"/> object on the stored list.
        /// </remarks>
        public MainDirectoryService()
        {
            Initialize();
        }

        /// <inheritdoc/>
        public IEnumerable<FileInfo> FindAllFiles(int id)
        {
            IMainDirectory? mainDirectory = ReadById(id);
            if (mainDirectory != null)
            {
                return FindAllFiles(mainDirectory);
            }

            return Array.Empty<FileInfo>();
        }

        /// <inheritdoc/>
        public IEnumerable<FileInfo> FindAllFiles(IMainDirectory mainDirectory)
        {
            string? mainDirPath = mainDirectory?.GetPath();

            if (mainDirPath != null)
            {
                return new DirectoryInfo(mainDirPath).EnumerateFiles("*.md", new EnumerationOptions { IgnoreInaccessible = true, RecurseSubdirectories = true });
            }

            return Array.Empty<FileInfo>();
        }

        /// <inheritdoc/>
        public List<IMainDirectory> ReadByMergedFileId(int mergedFileId)
        {
            List<IMainDirectory> result = new List<IMainDirectory>();

            foreach (var directory in _items)
            {
                if (directory.MergedFileId == mergedFileId)
                {
                    result.Add(directory);
                }
            }
            result.Sort();

            return result;
        }

        private static string? GetDefaultPath()
        {
            string appName = Assembly.GetExecutingAssembly().GetName().Name ?? "MdFilesMerger";
            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);

            appName = Path.GetFileNameWithoutExtension(appName);

            while (dir.Name != appName)
            {
                if (dir.Parent == null)
                {
                    return null;
                }
                else
                {
                    dir = dir.Parent;
                }
            }

            string? parentPath = dir.Parent?.Parent?.FullName;

            if (parentPath != null)
            {
                return Path.Combine(parentPath, "KursZostanProgramistaASPdotNET");
            }
            else
            {
                return null;
            }
        }

        private void Initialize()
        {
            MainDirectory dir = new MainDirectory(1) { MergedFileId = 1 };
            if (dir.SetPath(GetDefaultPath()))
            {
                Create(dir);
            }
        }
    }
}