using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.App.Abstract
{
    /// <summary>
    ///     Interface for handling collection of ignored files objects.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IService{T}"/> -&gt; <see cref="ICRUDService{T}"/>
    ///         -&gt; <see cref="IDirectoryService{T}"/> -&gt; <see cref="IRelativeFileService{T}"/>
    ///         -&gt; IIgnoredFileService <br/><b> Implementations: </b><see cref="Concrete.IgnoredFileService"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    /// <seealso cref="IDirectoryService{T}"> MdFilesMerger.App.Abstract.IDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="IRelativeFileService{T}"> MdFilesMerger.App.Abstract.IRelativeFileService&lt;T&gt; </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="Concrete.IgnoredFileService"> MdFilesMerger.App.Concrete.IgnoredFileService </seealso>
    /// <seealso cref="IIgnoredFile"> MdFilesMerger.Domain.Abstract.IIgnoredFile </seealso>
    public interface IIgnoredFileService : IRelativeFileService<IIgnoredFile>
    {
    }
}