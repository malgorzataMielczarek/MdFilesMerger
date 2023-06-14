using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.App.Abstract
{
    /// <summary>
    ///     Interface with methods for handling collection of <see cref="MergedFile"/> objects.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IService{T}"/> -&gt; <see cref="ICRUDService{T}"/>
    ///         -&gt; <see cref="IDirectoryService{T}"/> -&gt; <see cref="IEditFileService{T}"/>
    ///         -&gt; IMergedFileService <br/><b>
    ///         Implementations: </b><see cref="Concrete.MergedFileService"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    /// <seealso cref="IDirectoryService{T}"> MdFilesMerger.App.Abstract.IDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="IEditFileService{T}"> MdFilesMerger.App.Abstract.IEditFileService&lt;T&gt; </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="Concrete.MergedFileService"> MdFilesMerger.App.Concrete.MergedFileService </seealso>
    /// <seealso cref="MergedFile"> MdFilesMerger.Domain.Concrete.MergedFile </seealso>
    public interface IMergedFileService : IEditFileService<MergedFile>
    {
        /// <summary>
        ///     Appends the table of contents at the end of the file.
        /// </summary>
        /// <param name="destinationFile">
        ///     The destination file, where table of contents text will be added.
        /// </param>
        /// <param name="mergedFileId">
        ///     The identifier of the merged file associated with <paramref name="destinationFile"/>.
        /// </param>
        /// <param name="selectedFiles">
        ///     The list of all selected files that will be merged into the merged file. It is used
        ///     to create table of contents.
        /// </param>
        void AppendTableOfContents(FileInfo destinationFile, int mergedFileId, List<SelectedFile> selectedFiles);

        /// <summary>
        ///     Creates the file associated with specified <see cref="MergedFile"/> object.
        /// </summary>
        /// <remarks>
        ///     If the file already exists, it is deleted. New file is created and fill with
        ///     specified header.
        /// </remarks>
        /// <param name="mergedFile"> The merged file to create. </param>
        /// <returns>
        ///     <see cref="FileInfo"/> of created file or <see langword="null"/> if no file was
        ///     specified, or specified file couldn't be created.
        /// </returns>
        public FileInfo? CreateFile(MergedFile? mergedFile);

        /// <summary>
        ///     Gets, from the stored collection of <see cref="MergedFile"/> objects, elements with
        ///     the specified user identifier.
        /// </summary>
        /// <param name="userId"> The searched user identifier. </param>
        /// <returns>
        ///     The list of <see cref="MergedFile"/> objects with the specified user identifier. If
        ///     there is no objects with <see cref="MergedFile.UserId"/> equal <paramref
        ///     name="userId"/>, returned list is empty.
        /// </returns>
        public List<MergedFile> ReadByUserId(int userId);

        /// <summary>
        ///     Updates the name of the file associated with specified object.
        /// </summary>
        /// <param name="id"> The identifier of merged file to update. </param>
        /// <param name="fileName">
        ///     New name of the associated .md file. It doesn't have to include extension.
        /// </param>
        /// <returns>
        ///     The identifier of updated object or <see langword="-1"/>, if there is no <see
        ///     cref="MergedFile"/> with specified id in the stored collection or <paramref
        ///     name="fileName"/> is not a valid file name.
        /// </returns>
        public int UpdateFileName(int id, string fileName);

        /// <summary>
        ///     Updates the new line style that will be used in specified merged file.
        /// </summary>
        /// <param name="id"> The identifier of merged file to update. </param>
        /// <param name="newLineStyle"> The new new line style. </param>
        /// <returns>
        ///     The identifier of updated object or <see langword="-1"/>, if there is no <see
        ///     cref="MergedFile"/> with specified id in the stored collection.
        /// </returns>
        public int UpdateNewLineStyle(int id, string newLineStyle);

        /// <summary>
        ///     Updates the parent directory of the file associated with specified object.
        /// </summary>
        /// <param name="id"> The identifier of merged file to update. </param>
        /// <param name="directoryPath"> The new directory path. </param>
        /// <returns>
        ///     The identifier of updated object or <see langword="-1"/>, if there is no <see
        ///     cref="MergedFile"/> with specified id in the stored collection or <paramref
        ///     name="directoryPath"/> is not a valid path.
        /// </returns>
        public int UpdateParentDirectory(int id, string directoryPath);

        /// <summary>
        ///     Updates the type of table of contents for specified merged file.
        /// </summary>
        /// <param name="id"> The identifier of merged file to update. </param>
        /// <param name="tableOfContents"> The new type of table of contents. </param>
        /// <returns>
        ///     The identifier of updated object or <see langword="-1"/>, if there is no <see
        ///     cref="MergedFile"/> with specified id in the stored collection.
        /// </returns>
        public int UpdateTableOfContents(int id, TableOfContents tableOfContents);

        /// <summary>
        ///     Updates the header of table of contents for specified merged file.
        /// </summary>
        /// <param name="id"> The identifier of merged file to update. </param>
        /// <param name="header"> The new header. </param>
        /// <returns>
        ///     The identifier of updated object or <see langword="-1"/>, if there is no <see
        ///     cref="MergedFile"/> with specified id in the stored collection.
        /// </returns>
        public int UpdateTOCHeader(int id, string? header);
    }
}