using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.App.Abstract
{
    /// <summary>
    ///     Interface with methods for handling collection of selected file objects.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IService{T}"/> -&gt; <see cref="ICRUDService{T}"/>
    ///         -&gt; <see cref="IDirectoryService{T}"/> -&gt; <see cref="IRelativeFileService{T}"/>
    ///         -&gt; ISelectedFileService <br/><see cref="IService{T}"/> -&gt; <see cref="ICRUDService{T}"/>
    ///         -&gt; <see cref="IDirectoryService{T}"/> -&gt; <see cref="IEditFileService{T}"/>
    ///         -&gt; ISelectedFileService <br/><b> Implementations: </b><see cref="Concrete.SelectedFileService"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    /// <seealso cref="IDirectoryService{T}"> MdFilesMerger.App.Abstract.IDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="IEditFileService{T}"> MdFilesMerger.App.Abstract.IEditFileService&lt;T&gt; </seealso>
    /// <seealso cref="IRelativeFileService{T}"> MdFilesMerger.App.Abstract.IRelativeFileService&lt;T&gt; </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="Concrete.SelectedFileService"> MdFilesMerger.App.Concrete.SelectedFileService </seealso>
    /// <seealso cref="SelectedFile"> MdFilesMerger.Domain.Entity.SelectedFile </seealso>
    public interface ISelectedFileService : IRelativeFileService<SelectedFile>, IEditFileService<SelectedFile>
    {
        /// <summary>
        ///     Appends the content of the specified file to the destination file.
        /// </summary>
        /// <param name="id"> The identifier of object associated with the file to append. </param>
        /// <param name="destinationFile">
        ///     The destination file, to which the content of the file will be appended.
        /// </param>
        /// <param name="newLine"> The new line style, used in destination file. </param>
        public void AppendFile(int id, FileInfo? destinationFile, string newLine);

        /// <summary>
        ///     Appends the content of the specified file to the destination file.
        /// </summary>
        /// <param name="appendedFile"> The object associated with the file to append. </param>
        /// <param name="destinationFile">
        ///     The destination file, to which the content of the file will be appended.
        /// </param>
        /// <param name="newLine"> The new line style, used in destination file. </param>
        public void AppendFile(SelectedFile? appendedFile, FileInfo? destinationFile, string newLine);

        /// <summary>
        ///     Creates new objects associated with specified files and main directory and adds them
        ///     to the stored collection.
        /// </summary>
        /// <param name="list">
        ///     The collection of files that will be associated with added objects.
        /// </param>
        /// <param name="mainDirectory">
        ///     The main directory that all created objects will be connected with.
        /// </param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             Identification number of first object added to the stored collection.
        ///         </listheader>
        ///         <item>
        ///             Usually, it will be id of the created object associated with the first file
        ///             from the <paramref name="list"/> after adding it to the stored collection.
        ///         </item>
        ///         <item>
        ///             If that object wasn't successfully added (for example associated path wasn't
        ///             valid or lead to non existing or unaccessible file, object was <see
        ///             langword="null"/> or the collection already contains equal object), it will
        ///             be id of the first successfully added object.
        ///         </item>
        ///         <item>
        ///             Added object's id value will be one grater than max id value in the stored collection.
        ///         </item>
        ///         <item>
        ///             If no object was successfully added to the stored collection (for example
        ///             <paramref name="list"/> was empty or contained only <see langword="null"/>
        ///             values) method returns <see langword="-1"/>.
        ///         </item>
        ///     </list>
        /// </returns>
        public int CreateRange(IEnumerable<FileInfo> list, MainDirectory mainDirectory);
    }
}