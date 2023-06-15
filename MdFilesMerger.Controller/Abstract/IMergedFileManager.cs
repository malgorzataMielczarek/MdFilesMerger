using MdFilesMerger.App.Concrete;
using MdFilesMerger.Controller.Concrete;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.Controller.Abstract
{
    /// <summary>
    ///     Interface with additional methods and properties of manager for merged file model.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IManager{T, TService}"/> -&gt; <see
    ///         cref="ICRUDManager{T, TService}"/> -&gt; IMergedFileManager <br/><b>
    ///         Implementations: </b><see cref="MergedFileManager"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="MergedFileService"> MdFilesMerger.App.Concrete.MergedFileService </seealso>
    /// <seealso cref="ICRUDManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.ICRUDManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="MergedFileManager"> MdFilesMerger.Controller.Concrete.MergedFileManager </seealso>
    /// <seealso cref="MergedFile"> MdFilesMerger.Domain.Entity.MergedFile </seealso>
    public interface IMergedFileManager : ICRUDManager<MergedFile, MergedFileService>
    {
        /// <summary>
        ///     The main directory manager.
        /// </summary>
        public MainDirectoryManager MainDirectoryManager { get; }

        /// <summary>
        ///     Merges appropriate files into one .md file specified by selected item.
        /// </summary>
        /// <remarks>
        ///     New file with specified path is created. If selected item has title set, file
        ///     contains it. If created file is associated with any of the selected files connected
        ///     with selected item, file is set as ignored. Next, all selected files connected with
        ///     this merged file are retrieved. If selected item has table of content of type other
        ///     then <see cref="TableOfContents.None"/>, then appropriate table of content is
        ///     appended to file. At last content of all files associated with retrieved selected
        ///     files is consecutively appended at the end of created file.
        ///     <para> Progress of the files merging is displayed in the console. </para>
        /// </remarks>
        public void Merge();

        /// <summary>
        ///     Updates the filename of file associated with currently selected item.
        /// </summary>
        public void UpdateFileName();

        /// <summary>
        ///     Updates the path of parent directory of file associated with currently selected item.
        /// </summary>
        public void UpdateParentDirectory();

        /// <summary>
        ///     Updates the table of contents type of selected item.
        /// </summary>
        /// <param name="newTableOfContents"> The new table of contents type. </param>
        public void UpdateTableOfContents(TableOfContents newTableOfContents);

        /// <summary>
        ///     Updates the title (header) of the currently selected merged file.
        /// </summary>
        public void UpdateTitle();
    }
}