using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.Domain.Common
{
    /// <summary>
    ///     Base class for selected and ignored file models.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseItem"/> -&gt; <see cref="BaseDirectory"/> -&gt;
    ///         <see cref="MdFile"/> -&gt; RelativeFile <br/><b> Implements: </b><see
    ///         cref="IComparable{BaseDirectory}"/>, <see cref="IDirectory"/>, <see cref="IItem"/>,
    ///         <see cref="IRelativeFile"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="BaseDirectory"> MdFilesMerger.Domain.Common.BaseDirectory </seealso>
    /// <seealso cref="BaseItem"> MdFilesMerger.Domain.Common.BaseItem </seealso>
    /// <seealso cref="MdFile"> MdFilesMerger.Domain.Common.MdFile </seealso>
    /// <seealso cref="IComparable{T}"> System.IComparable&lt;T&gt; </seealso>
    /// <seealso cref="IDirectory"> MdFilesMerger.Domain.Abstract.IDirectory </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    /// <seealso cref="IRelativeFile"> MdFilesMerger.Domain.Abstract.IRelativeFile </seealso>
    public class RelativeFile : MdFile, IRelativeFile
    {
        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        public RelativeFile() : base()
        {
            MainDirId = 0;
        }

        /// <inheritdoc/>
        public RelativeFile(int id) : base(id)
        {
            MainDirId = 0;
        }

        /// <summary>
        ///     Sets <see cref="BaseItem.Id"/> to <paramref name="id"/>, <see cref="BaseItem.Name"/>
        ///     to <paramref name="path"/> and <see cref="MainDirId"/> to <paramref name="mainDirId"/>.
        /// </summary>
        /// <remarks>
        ///     No evaluation or adjusting is performed, so use this method only for already
        ///     evaluated and prepared data, for example from database.
        /// </remarks>
        /// <param name="id"> The item identification number. </param>
        /// <param name="path">
        ///     Valid relative path to existing .md file associated with this item. Path should use
        ///     <see langword="'/'"/> as directory separator.
        /// </param>
        /// <param name="mainDirId">
        ///     <see cref="BaseItem.Id"/> of <see cref="MainDirectory"/> object associated with this item.
        /// </param>
        public RelativeFile(int id, string? path, int mainDirId) : base()
        {
            Id = id;
            Name = path;
            MainDirId = mainDirId;
        }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <value> By default set to <see langword="0"/> </value>
        public int MainDirId { get; set; }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <param name="mainDirPath">
        ///     Absolute or relative (to current directory) path of main directory, that the path
        ///     saved in <see cref="BaseItem.Name"/> is relative to.
        /// </param>
        /// <returns>
        ///     System specific absolute path created by combining <paramref name="mainDirPath"/>
        ///     and relative path saved in <see cref="BaseItem.Name"/>.
        /// </returns>
        public string? GetPath(string? mainDirPath)
        {
            if (string.IsNullOrWhiteSpace(mainDirPath))
            {
                return Name;
            }

            return Path.GetFullPath(GetPath() ?? string.Empty, Path.GetFullPath(mainDirPath));
        }

        /// <summary>
        ///     Sets <see cref="BaseItem.Name"/> to relative path with <see langword="'/'"/> as
        ///     directory separator.
        /// </summary>
        /// <remarks>
        ///     Set path is relative to <paramref name="mainDirPath"/>. If <paramref name="path"/>
        ///     doesn't point to existing .md file, <see cref="BaseItem.Name"/> is set to <see langword="null"/>.
        /// </remarks>
        /// <param name="path">
        ///     Absolute or relative (to current directory) path to existing .md file associated
        ///     with this instance.
        /// </param>
        /// <param name="mainDirPath">
        ///     Absolute or relative (to current directory) path to base directory, associated with
        ///     this instance.
        /// </param>
        /// <returns>
        ///     <see langword="true"/>, if <see cref="BaseItem.Name"/> was successfully set to path;
        ///     otherwise <see langword="false"/>.
        /// </returns>
        public bool SetPath(string? path, string? mainDirPath)
        {
            if (base.SetPath(path) && File.Exists(path = Path.GetFullPath(Name!)))
            {
                if (!string.IsNullOrWhiteSpace(mainDirPath))
                {
                    Name = Path.GetRelativePath(Path.GetFullPath(mainDirPath), path).Replace('\\', '/');
                }

                return true;
            }

            return false;
        }
    }
}