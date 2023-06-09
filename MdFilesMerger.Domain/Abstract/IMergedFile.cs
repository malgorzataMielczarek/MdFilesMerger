namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    ///     Interface for merged file model.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IItem"/> -&gt; <see cref="IDirectory"/> -&gt; <see
    ///         cref="IEditFile"/> -&gt; IMergedFile <br/><b> Implementations: </b><see cref="Entity.MergedFile"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="IDirectory"> MdFilesMerger.Domain.Abstract.IDirectory </seealso>
    /// <seealso cref="IEditFile"> MdFilesMerger.Domain.Abstract.IEditFile </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    /// <seealso cref="Entity.MergedFile"> MdFilesMerger.Domain.Entity.MergedFile </seealso>
    public interface IMergedFile : IEditFile
    {
        /// <summary>
        ///     Last date and time of creating the merged file. If merged file wasn't created yet,
        ///     it is set to <see langword="null"/>.
        /// </summary>
        public DateTime? MergeDate { get; set; }

        /// <summary>
        ///     String that will be put at the end of each line of text in created merged file.
        /// </summary>
        public string NewLineStyle { get; set; }

        /// <summary>
        ///     Type of the table of contents that will by placed at the beginning of created merged file.
        /// </summary>
        public Common.TableOfContents TableOfContents { get; set; }

        /// <summary>
        ///     Header placed in the merged file before the table of contents content.
        /// </summary>
        public string? TOCHeader { get; set; }

        /// <summary>
        ///     Identification number of <see cref="Entity.User"/> associated with this item.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     Extract parent directory path from path stored in <see cref="IItem.Name"/>.
        /// </summary>
        /// <returns>
        ///     Path of parent directory of the file whose path is stored in <see cref="IItem.Name"/>.
        /// </returns>
        public string? GetParentDirectory();

        /// <summary>
        ///     Changes filename in path saved in <see cref="IItem.Name"/>, without changing parent
        ///     directory path.
        /// </summary>
        /// <param name="fileName"> New filename. </param>
        /// <returns>
        ///     <see langword="true"/> if filename was successfully changed; otherwise <see langword="false"/>.
        /// </returns>
        public bool SetFileName(string fileName);

        /// <summary>
        ///     Changes parent directory of associated merged file.
        /// </summary>
        /// <remarks>
        ///     Changes parent directory in path stored in <see cref="IItem.Name"/> without changing filename.
        /// </remarks>
        /// <param name="directoryPath"> New parent directory path. </param>
        /// <returns>
        ///     <see langword="true"/> if parent directory path was successfully changed; otherwise
        ///     <see langword="false"/>
        /// </returns>
        public bool SetParentDirectory(string directoryPath);
    }
}