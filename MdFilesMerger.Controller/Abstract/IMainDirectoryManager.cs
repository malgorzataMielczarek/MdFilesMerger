using MdFilesMerger.App.Concrete;
using MdFilesMerger.Controller.Concrete;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.Controller.Abstract
{
    /// <summary>
    ///     Interface of manager for main directory model.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IManager{T, TService}"/> -&gt; <see
    ///         cref="ICRUDManager{T, TService}"/> -&gt; IMainDirectoryManager <br/><b>
    ///         Implementations: </b><see cref="MainDirectoryManager"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="MainDirectoryService"> MdFilesMerger.App.Concrete.MainDirectoryService </seealso>
    /// <seealso cref="ICRUDManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.ICRUDManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="MainDirectoryManager"> MdFilesMerger.Controller.Concrete.MainDirectoryManager </seealso>
    /// <seealso cref="MainDirectory"> MdFilesMerger.Domain.Entity.MainDirectory </seealso>
    public interface IMainDirectoryManager : ICRUDManager<MainDirectory, MainDirectoryService>
    {
        /// <summary>
        ///     The ignored file manager instance whose service is connected with service of this manager.
        /// </summary>
        public IgnoredFileManager IgnoredFileManager { get; }

        /// <summary>
        ///     The selected file manager instance whose service is connected with service of this manager.
        /// </summary>
        public SelectedFileManager SelectedFileManager { get; }

        /// <summary>
        ///     Moves file from selected to ignored collection.
        /// </summary>
        /// <remarks>
        ///     If specified file is associated with selected file object connected with selected
        ///     item it is deleted from selected file service collection, converted to ignored file
        ///     object and added to ignored file service collection. If there is no appropriate
        ///     object in selected file service collection, nothing happens.
        /// </remarks>
        /// <param name="file"> The file to ignore. </param>
        public void IgnoreFile(FileInfo file);

        /// <summary>
        ///     Moves file from selected to ignored collection.
        /// </summary>
        /// <remarks>
        ///     If specified file is associated with selected file object connected with <paramref
        ///     name="mainDirectory"/> it is deleted from selected file service collection,
        ///     converted to ignored file object and added to ignored file service collection. If
        ///     there is no appropriate object in selected file service collection, nothing happens.
        /// </remarks>
        /// <param name="file"> The file to ignore. </param>
        /// <param name="mainDirectory"> The main directory that file is connected with. </param>
        public void IgnoreFile(FileInfo file, MainDirectory? mainDirectory);

        /// <summary>
        ///     Updates the path of selected item.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> if path was successfully updated; otherwise <see langword="false"/>.
        /// </returns>
        public bool UpdatePath();
    }
}