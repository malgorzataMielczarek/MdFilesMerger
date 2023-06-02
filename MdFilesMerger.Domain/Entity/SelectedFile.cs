using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MdFilesMerger.Domain.Entity
{
    /// <summary>
    ///     Implementation of selected file model associated with located in main directory .md
    ///     file, whose content will be included in created merged file.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseItem"/> -&gt; <see cref="BaseDirectory"/> -&gt;
    ///         <see cref="MdFile"/> -&gt; <see cref="RelativeFile"/> -&gt; SelectedFile <br/><b>
    ///         Implements: </b><see cref="IComparable{BaseDirectory}"/>, <see cref="IDirectory"/>,
    ///                     <see cref="IEditFile"/>, <see cref="IItem"/>, <see cref="IRelativeFile"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="BaseDirectory"> MdFilesMerger.Domain.Common.BaseDirectory </seealso>
    /// <seealso cref="BaseItem"> MdFilesMerger.Domain.Common.BaseItem </seealso>
    /// <seealso cref="MdFile"> MdFilesMerger.Domain.Common.MdFile </seealso>
    /// <seealso cref="RelativeFile"> MdFilesMerger.Domain.Common.RelativeFile </seealso>
    /// <seealso cref="IComparable{T}"> System.IComparable&lt;T&gt; </seealso>
    /// <seealso cref="IDirectory"> MdFilesMerger.Domain.Abstract.IDirectory </seealso>
    /// <seealso cref="IEditFile"> MdFilesMerger.Domain.Abstract.IEditFile </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    /// <seealso cref="IRelativeFile"> MdFilesMerger.Domain.Abstract.IRelativeFile </seealso>
    public sealed class SelectedFile : RelativeFile, IEditFile
    {
        /// <inheritdoc/>
        public SelectedFile() : base()
        {
            ModifiedDate = DateTime.Now;
            Title = null;
        }

        /// <inheritdoc/>
        public SelectedFile(int id) : base(id)
        {
            ModifiedDate = DateTime.Now;
            Title = null;
        }

        /// <summary>
        ///     Sets all properties to values of appropriate arguments.
        /// </summary>
        /// <remarks>
        ///     No evaluation or adjusting is performed, so use this method only for already
        ///     evaluated and prepared data, for example from database.
        /// </remarks>
        /// <param name="id"> Identification number of item. </param>
        /// <param name="path">
        ///     Valid relative path to existing .md file associated with this item. Path should use
        ///     <see langword="'/'"/> as directory separator.
        /// </param>
        /// <param name="mainDirId">
        ///     <see cref="BaseItem.Id"/> of <see cref="MainDirectory"/> object associated with this item.
        /// </param>
        /// <param name="modifiedDate"> Date and time of last modification of this entity. </param>
        /// <param name="title">
        ///     Text put as table of contents entry connected with contend of associated .md file.
        /// </param>
        public SelectedFile(int id, string? path, int mainDirId, DateTime modifiedDate, string? title) : base(id, path, mainDirId)
        {
            ModifiedDate = modifiedDate;
            Title = title;
        }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <value> By default it is set to <see langword="null"/>. </value>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        ///     Text that will be put in table of contents (if it will be included) as header
        ///     pointing in created merged file to content of associated with this instance file.
        /// </summary>
        /// <value>
        ///     By default set to <see langword="null"/> or, if <see cref="BaseItem.Name"/> is set
        ///     to associated .md file path, to text of header of this file (if exists) or filename
        ///     without extension (if file doesn't have header).
        /// </value>
        public string? Title { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj != null && obj is SelectedFile other)
            {
                return this.Name == other.Name && this.MainDirId == other.MainDirId;
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <remarks>
        ///     <inheritdoc/>
        ///     <para>
        ///         If <see cref="BaseItem.Name"/> was successfully set to path and <see
        ///         cref="Title"/> is <see langword="null"/>, <see cref="Title"/> is set to
        ///         appropriate default value.
        ///     </para>
        /// </remarks>
        /// <param name="path"> <inheritdoc/> </param>
        /// <returns> <inheritdoc/> </returns>
        public override bool SetPath([NotNullWhen(true)] string? path)
        {
            bool result = base.SetPath(path);
            if (result && Title == null)
            {
                Title = GetFileHeader();
            }

            return result;
        }

        // Open the file and read first not empty line of text from it. If it is a header (starts
        // with '#') return it. Else return filename, without extension. If file header is a link
        // return only text part
        private string GetFileHeader()
        {
            string header = "";

            if (!string.IsNullOrWhiteSpace(Name))
            {
                // as file title
                using (StreamReader streamReader = new StreamReader(new FileInfo(Name).OpenRead()))
                {
                    // get first not empty line
                    while (string.IsNullOrWhiteSpace(header = streamReader.ReadLine() ?? "")) { }
                    streamReader.Close();
                }

                // as filename
                if (string.IsNullOrWhiteSpace(header) || header[0] != '#')
                {
                    header = Path.GetFileNameWithoutExtension(Name);
                }
                else
                {
                    StringBuilder sb = new StringBuilder(header.TrimEnd());
                    while (char.IsWhiteSpace(sb[0]) || sb[0] == '#')
                    {
                        sb.Remove(0, 1);
                    }

                    return sb.ToString();
                }
            }

            return header;
        }
    }
}