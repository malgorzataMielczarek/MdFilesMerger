using MdFilesMerger.App.Common;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.App.Concrete
{
    /// <summary>
    ///     Class handling collection of <see cref="IgnoredFile"/> objects associated with the .md
    ///     files ignored during merge, whose paths are stored as relative to connected main
    ///     directory path.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseService{T}"/> -&gt; <see cref="BaseDirectoryService{T}"/>
    ///         -&gt; <see cref="RelativeFileService{T}"/> -&gt; IgnoredFileService <br/><b>
    ///         Implements: </b><see cref="Abstract.ICRUDService{T}"/>, <see
    ///                     cref="Abstract.IDirectoryService{T}"/>, <see
    ///                     cref="Abstract.IRelativeFileService{T}"/>, <see cref="Abstract.IService{T}"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="Abstract.ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    /// <seealso cref="Abstract.IDirectoryService{T}"> MdFilesMerger.App.Abstract.IDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="Abstract.IRelativeFileService{T}"> MdFilesMerger.App.Abstract.IRelativeFileService&lt;T&gt; </seealso>
    /// <seealso cref="Abstract.IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="BaseDirectoryService{T}"> MdFilesMerger.App.Common.BaseDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="BaseService{T}"> MdFilesMerger.App.Common.BaseService&lt;T&gt; </seealso>
    /// <seealso cref="RelativeFileService{T}"> MdFilesMerger.App.Common.RelativeFileService&lt;T&gt; </seealso>
    public sealed class IgnoredFileService : RelativeFileService<IgnoredFile>
    {
    }
}