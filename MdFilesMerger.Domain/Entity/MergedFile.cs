using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.Domain.Entity
{
    /// <summary>
    ///     Implementation of merged file model.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseItem"/> -&gt; <see cref="BaseDirectory"/> -&gt;
    ///         <see cref="MdFile"/> -&gt; MergedFile <br/><b> Implements: </b><see
    ///         cref="IComparable{BaseDirectory}"/>, <see cref="IDirectory"/>, <see
    ///         cref="IEditFile"/>, <see cref="IItem"/>, <see cref="IMergedFile"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="BaseDirectory"> MdFilesMerger.Domain.Common.BaseDirectory </seealso>
    /// <seealso cref="BaseItem"> MdFilesMerger.Domain.Common.BaseItem </seealso>
    /// <seealso cref="MdFile"> MdFilesMerger.Domain.Common.MdFile </seealso>
    /// <seealso cref="IComparable{T}"> System.IComparable&lt;T&gt; </seealso>
    /// <seealso cref="IDirectory"> MdFilesMerger.Domain.Abstract.IDirectory </seealso>
    /// <seealso cref="IEditFile"> MdFilesMerger.Domain.Abstract.IEditFile </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    /// <seealso cref="IMergedFile"> MdFilesMerger.Domain.Abstract.IMergedFile </seealso>
    public sealed class MergedFile : MdFile, IMergedFile
    {
        /// <inheritdoc/>
        public MergedFile() : base()
        {
            MergeDate = null;
            ModifiedDate = DateTime.Now;
            NewLineStyle = "\n";
            TableOfContents = TableOfContents.None;
            Title = "Kurs \"Zostań programistą ASP.NET\" - notatki";
            TOCHeader = "## Spis treści";
            UserId = 0;
        }

        /// <summary>
        ///     Sets <see cref="BaseItem.Id"/> to <paramref name="id"/>, <see cref="UserId"/> to
        ///     <paramref name="userId"/> and rest properties to its default values.
        /// </summary>
        /// <param name="id"> Identification number of this item. </param>
        /// <param name="userId">
        ///     Identification number of <see cref="User"/> object connected with this item.
        /// </param>
        public MergedFile(int id, int userId) : this()
        {
            Id = id;
            UserId = userId;
        }

        /// <summary>
        ///     Sets all properties accordingly.
        /// </summary>
        /// <remarks>
        ///     No evaluation or adjusting is performed, so use it only for previously checked and
        ///     adjusted data, for example from database.
        /// </remarks>
        /// <param name="id"> Identification number of item. </param>
        /// <param name="path"> Absolute path to .md file, where merged file will be created. </param>
        /// <param name="mergeDate">
        ///     Date and time of last creation of merged file associated with this instance or <see
        ///     langword="null"/> if file wasn't created yet.
        /// </param>
        /// <param name="modifiedDate"> Date and time of last modification of this entity. </param>
        /// <param name="newLineStyle">
        ///     String that will be placed at the end of each line of text placed in created merged
        ///     file and is treated as a newline character.
        /// </param>
        /// <param name="tableOfContents">
        ///     Type of table of contents that will be placed at the beginning of created merged file.
        /// </param>
        /// <param name="title">
        ///     Text that will be placed as first line of created merged file. Title (header) of the file.
        /// </param>
        /// <param name="tOCHeader">
        ///     Text that will be placed in created merged file in the line below <paramref
        ///     name="title"/> and before content of table of contents.
        /// </param>
        /// <param name="userId">
        ///     Identification number of <see cref="User"/> associated with this instance.
        /// </param>
        public MergedFile(int id, string? path, DateTime? mergeDate, DateTime modifiedDate, string newLineStyle, TableOfContents tableOfContents, string? title, string? tOCHeader, int userId) : base(id)
        {
            MergeDate = mergeDate;
            ModifiedDate = modifiedDate;
            Name = path;
            NewLineStyle = newLineStyle;
            TableOfContents = tableOfContents;
            Title = title;
            TOCHeader = tOCHeader;
            UserId = userId;
        }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <value> By default it is set to <see langword="null"/>. </value>
        public DateTime? MergeDate { get; set; }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <value> By default set to <see cref="DateTime.Now"/>. </value>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <value> By default it is set to <c> "\n" </c>, but it can by any string. </value>
        public string NewLineStyle { get; set; }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <value> By default it is set to <see cref="TableOfContents.None"/>. </value>
        public TableOfContents TableOfContents { get; set; }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <value>
        ///     By default it is set to <c> "Kurs \"Zostań programistą ASP.NET\" - notatki" </c>.
        /// </value>
        public string? Title { get; set; }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <value>
        ///     By default it is set to <c> "## Spis treści" </c> (second level header with "Spis
        ///     treści" text).
        /// </value>
        public string? TOCHeader { get; set; }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <value> By default set to <see langword="0"/>. </value>
        public int UserId { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj != null && obj is MergedFile other)
            {
                return this.Name == other.Name && this.UserId == other.UserId;
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode() => base.GetHashCode();

        /// <inheritdoc/>
        public string? GetParentDirectory()
        {
            return Path.GetDirectoryName(Name);
        }

        /// <inheritdoc/>
        public bool SetFileName(string fileName)
        {
            string directory = GetParentDirectory() ?? string.Empty;

            return SetPath(Path.Combine(directory, fileName.Trim()));
        }

        /// <inheritdoc/>
        public bool SetParentDirectory(string directoryPath)
        {
            string fileName = Path.GetFileName(Name) ?? string.Empty;

            return SetPath(Path.Combine(directoryPath.Trim(), fileName));
        }
    }
}