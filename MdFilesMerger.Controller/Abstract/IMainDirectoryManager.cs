using MdFilesMerger.App.Abstract;
using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.Controller.Abstract
{
    /// <summary>
    ///     Interface of manager for main directory model.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IManager{T, TService}"/> -&gt; <see
    ///         cref="ICRUDManager{T, TService}"/> -&gt; IMainDirectoryManager <br/><b>
    ///         Implementations: </b><see cref="Concrete.MainDirectoryManager"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="IMainDirectoryService"> MdFilesMerger.App.Abstract.IMainDirectoryService </seealso>
    /// <seealso cref="ICRUDManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.ICRUDManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="Concrete.MainDirectoryManager"> MdFilesMerger.Controller.Concrete.MainDirectoryManager </seealso>
    /// <seealso cref="IMainDirectory"> MdFilesMerger.Domain.Abstract.IMainDirectory </seealso>
    public interface IMainDirectoryManager : ICRUDManager<IMainDirectory, IMainDirectoryService>
    {
        /// <summary>
        ///     The ignored file manager instance whose service is connected with service of this manager.
        /// </summary>
        IIgnoredFileManager IgnoredFileManager { get; }

        /// <summary>
        ///     The selected file manager instance whose service is connected with service of this manager.
        /// </summary>
        ISelectedFileManager SelectedFileManager { get; }

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
        void IgnoreFile(FileInfo file);

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
        void IgnoreFile(FileInfo file, IMainDirectory? mainDirectory);

        /// <summary>
        ///     Updates the path of selected item.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> if path was successfully updated; otherwise <see langword="false"/>.
        /// </returns>
        bool UpdatePath();
    }
}