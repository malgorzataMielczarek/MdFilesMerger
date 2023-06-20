using MdFilesMerger.App.Abstract;
using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.Controller.Abstract
{
    /// <summary>
    ///     Interface for ignored file model manager.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IManager{T, TService}"/> -&gt; <see
    ///         cref="ICRUDManager{T, TService}"/> -&gt; <see cref="IRelativeFileManager{T,
    ///         TService}"/> -&gt; IIgnoredFileManager <br/><b> Implementations: </b><see cref="Concrete.IgnoredFileManager"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="IIgnoredFileService"> MdFilesMerger.App.Abstract.IIgnoredFileService </seealso>
    /// <seealso cref="ICRUDManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.ICRUDManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IRelativeFileManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IRelativeFileManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="Concrete.IgnoredFileManager"> MdFilesMerger.Controller.Concrete.IgnoredFileManager </seealso>
    /// <seealso cref="IIgnoredFile"> MdFilesMerger.Domain.Abstract.IIgnoredFile </seealso>
    public interface IIgnoredFileManager : IRelativeFileManager<IIgnoredFile, IIgnoredFileService>
    {
    }
}