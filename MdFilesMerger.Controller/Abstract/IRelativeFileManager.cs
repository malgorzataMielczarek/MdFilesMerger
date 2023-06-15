using MdFilesMerger.App.Common;
using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.Controller.Abstract
{
    /// <summary>
    ///     Interface with additional methods for relative file models managers.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IManager{T, TService}"/> -&gt; <see
    ///         cref="ICRUDManager{T, TService}"/> -&gt; IRelativeFileManager&lt;T, TService&gt;
    ///         <br/><b> Implementations: </b><see cref="Common.RelativeFileManager{T, TService}"/>,
    ///         <see cref="Concrete.IgnoredFileManager"/>, <see cref="Concrete.SelectedFileManager"/>
    ///     </para>
    /// </summary>
    /// <typeparam name="T"> Type of relative file model. </typeparam>
    /// <typeparam name="TService">
    ///     Service handling collection of object of specified model.
    /// </typeparam>
    /// <seealso cref="RelativeFileService{T}"> MdFilesMerger.App.Common.RelativeFileService&lt;T&gt; </seealso>
    /// <seealso cref="ICRUDManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.ICRUDManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="Common.RelativeFileManager{T, TService}">
    ///     MdFilesMerger.Controller.Common.RelativeFileManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="Concrete.IgnoredFileManager"> MdFilesMerger.Controller.Concrete.IgnoredFileManager </seealso>
    /// <seealso cref="Concrete.SelectedFileManager"> MdFilesMerger.Controller.Concrete.SelectedFileManager </seealso>
    /// <seealso cref="RelativeFile"> MdFilesMerger.Domain.Common.RelativeFile </seealso>
    public interface IRelativeFileManager<T, TService> : ICRUDManager<T, TService> where T : RelativeFile where TService : RelativeFileService<T>
    {
        /// <summary>
        ///     Sets selected item to specified identifier.
        /// </summary>
        /// <param name="id"> The identifier of element to select. </param>
        void SelectItem(int id);
    }
}