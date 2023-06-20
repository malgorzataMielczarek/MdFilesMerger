using MdFilesMerger.App.Abstract;
using MdFilesMerger.App.Common;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.App.Concrete
{
    /// <summary>
    ///     Class handling collection of <see cref="IIgnoredFile"/> objects associated with the .md
    ///     files ignored during merge, whose paths are stored as relative to connected main
    ///     directory path.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseService{T}"/> -&gt; <see cref="BaseDirectoryService{T}"/>
    ///         -&gt; <see cref="RelativeFileService{T}"/> -&gt; IgnoredFileService <br/><b>
    ///         Implements: </b><see cref="ICRUDService{T}"/>, <see cref="IDirectoryService{T}"/>,
    ///                     <see cref="IIgnoredFileService"/>, <see
    ///                     cref="IRelativeFileService{T}"/>, <see cref="IService{T}"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    /// <seealso cref="IDirectoryService{T}"> MdFilesMerger.App.Abstract.IDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="IIgnoredFileService"> MdFilesMerger.App.Abstract.IIgnoredFileService </seealso>
    /// <seealso cref="IRelativeFileService{T}"> MdFilesMerger.App.Abstract.IRelativeFileService&lt;T&gt; </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="BaseDirectoryService{T}"> MdFilesMerger.App.Common.BaseDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="BaseService{T}"> MdFilesMerger.App.Common.BaseService&lt;T&gt; </seealso>
    /// <seealso cref="RelativeFileService{T}"> MdFilesMerger.App.Common.RelativeFileService&lt;T&gt; </seealso>
    /// <seealso cref="IIgnoredFile"> MdFilesMerger.Domain.Abstract.IIgnoredFile </seealso>
    public sealed class IgnoredFileService : RelativeFileService<IIgnoredFile>, IIgnoredFileService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="IgnoredFileService"/> class.
        /// </summary>
        /// <param name="mainDirService"> The main directory service. </param>
        public IgnoredFileService(IMainDirectoryService mainDirService) : base(mainDirService) { }

        /// <summary>
        ///     Converts <see cref="IIgnoredFile"/> objects to <see cref="SelectedFile"/> objects.
        /// </summary>
        /// <param name="list"> The list of <see cref="IIgnoredFile"/> objects to convert. </param>
        /// <param name="mainDirectoryService"> The main directory service. </param>
        /// <returns> List of objects after conversion. </returns>
        public static List<SelectedFile> ToSelectedFile(List<IIgnoredFile> list, IMainDirectoryService mainDirectoryService)
        {
            var selectedFiles = new List<SelectedFile>();
            foreach (var file in list)
            {
                IMainDirectory? mainDirectory = mainDirectoryService.ReadById(file.MainDirId);
                selectedFiles.Add(file.ToSelectedFile(mainDirectory?.GetPath()));
            }

            return selectedFiles;
        }
    }
}