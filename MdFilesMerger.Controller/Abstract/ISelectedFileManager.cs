using MdFilesMerger.App.Concrete;
using MdFilesMerger.Domain.Entity;

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
    /// <seealso cref="SelectedFileService"> MdFilesMerger.App.Concrete.SelectedFileService </seealso>
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
    /// <seealso cref="SelectedFile"> MdFilesMerger.Domain.Entity.SelectedFile </seealso>
    public interface ISelectedFileManager : IRelativeFileManager<SelectedFile, SelectedFileService>
    {
        /// <summary>
        ///     Updates the title (table of contents entry) of selected item.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> if title was successfully updated; otherwise <see langword="false"/>.
        /// </returns>
        public bool UpdateTitle();
    }
}