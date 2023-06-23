using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.App.Abstract
{
    /// <summary>
    ///     Interface with methods for handling collection of <see cref="IMergedFile"/> objects.
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
    /// <seealso cref="IMergedFile"> MdFilesMerger.Domain.Abstract.IMergedFile </seealso>
    public interface IMergedFileService : IEditFileService<IMergedFile>
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
        void AppendTableOfContents(FileInfo destinationFile, int mergedFileId, List<ISelectedFile> selectedFiles);

        /// <summary>
        ///     Creates the file associated with specified <see cref="IMergedFile"/> object.
        /// </summary>
        /// <remarks>
        ///     If the file already exists, it is deleted. New file is created and fill with
        ///     specified header.
        /// </remarks>
        /// <param name="id">
        ///     The identifier of the object associated with the merged file to create.
        /// </param>
        /// <returns>
        ///     <see cref="FileInfo"/> of created file or <see langword="null"/> if no file was
        ///     specified, or specified file couldn't be created.
        /// </returns>
        FileInfo? CreateFile(int id);

        /// <summary>
        ///     Gets, from the stored collection of <see cref="IMergedFile"/> objects, elements with
        ///     the specified user identifier.
        /// </summary>
        /// <param name="userId"> The searched user identifier. </param>
        /// <returns>
        ///     The list of <see cref="IMergedFile"/> objects with the specified user identifier. If
        ///     there is no objects with <see cref="IMergedFile.UserId"/> equal <paramref
        ///     name="userId"/>, returned list is empty.
        /// </returns>
        List<IMergedFile> ReadByUserId(int userId);

        /// <summary>
        ///     Updates the name of the file associated with specified object.
        /// </summary>
        /// <param name="id"> The identifier of merged file to update. </param>
        /// <param name="fileName">
        ///     New name of the associated .md file. It doesn't have to include extension.
        /// </param>
        /// <returns>
        ///     The identifier of updated object or <see langword="-1"/>, if there is no <see
        ///     cref="IMergedFile"/> with specified id in the stored collection or <paramref
        ///     name="fileName"/> is not a valid file name.
        /// </returns>
        int UpdateFileName(int id, string fileName);

        /// <summary>
        ///     Updates the new line style that will be used in specified merged file.
        /// </summary>
        /// <param name="id"> The identifier of merged file to update. </param>
        /// <param name="newLineStyle"> The new new line style. </param>
        /// <returns>
        ///     The identifier of updated object or <see langword="-1"/>, if there is no <see
        ///     cref="IMergedFile"/> with specified id in the stored collection.
        /// </returns>
        int UpdateNewLineStyle(int id, string newLineStyle);

        /// <summary>
        ///     Updates the parent directory of the file associated with specified object.
        /// </summary>
        /// <param name="id"> The identifier of merged file to update. </param>
        /// <param name="directoryPath"> The new directory path. </param>
        /// <returns>
        ///     The identifier of updated object or <see langword="-1"/>, if there is no <see
        ///     cref="IMergedFile"/> with specified id in the stored collection or <paramref
        ///     name="directoryPath"/> is not a valid path.
        /// </returns>
        int UpdateParentDirectory(int id, string directoryPath);

        /// <summary>
        ///     Updates the type of table of contents for specified merged file.
        /// </summary>
        /// <param name="id"> The identifier of merged file to update. </param>
        /// <param name="tableOfContents"> The new type of table of contents. </param>
        /// <returns>
        ///     The identifier of updated object or <see langword="-1"/>, if there is no <see
        ///     cref="IMergedFile"/> with specified id in the stored collection.
        /// </returns>
        int UpdateTableOfContents(int id, TableOfContents tableOfContents);

        /// <summary>
        ///     Updates the header of table of contents for specified merged file.
        /// </summary>
        /// <param name="id"> The identifier of merged file to update. </param>
        /// <param name="header"> The new header. </param>
        /// <returns>
        ///     The identifier of updated object or <see langword="-1"/>, if there is no <see
        ///     cref="IMergedFile"/> with specified id in the stored collection.
        /// </returns>
        int UpdateTOCHeader(int id, string? header);
    }
}