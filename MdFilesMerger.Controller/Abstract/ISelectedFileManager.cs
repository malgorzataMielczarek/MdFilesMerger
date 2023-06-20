using MdFilesMerger.App.Abstract;
using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.Controller.Abstract
{
    /// <summary>
    ///     Interface with additional methods of manager for selected file model.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IManager{T, TService}"/> -&gt; <see
    ///         cref="ICRUDManager{T, TService}"/> -&gt; <see cref="IRelativeFileManager{T,
    ///         TService}"/> -&gt; ISelectedFileManager <br/><b> Implementations: </b><see cref="Concrete.SelectedFileManager"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="ISelectedFileService"> MdFilesMerger.App.Abstract.ISelectedFileService </seealso>
    /// <seealso cref="ICRUDManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.ICRUDManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IRelativeFileManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IRelativeFileManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="Concrete.SelectedFileManager"> MdFilesMerger.Controller.Concrete.SelectedFileManager </seealso>
    /// <seealso cref="ISelectedFile"> MdFilesMerger.Domain.Abstract.ISelectedFile </seealso>
    public interface ISelectedFileManager : IRelativeFileManager<ISelectedFile, ISelectedFileService>
    {
        /// <summary>
        ///     Updates the title (table of contents entry) of selected item.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> if title was successfully updated; otherwise <see langword="false"/>.
        /// </returns>
        bool UpdateTitle();
    }
}