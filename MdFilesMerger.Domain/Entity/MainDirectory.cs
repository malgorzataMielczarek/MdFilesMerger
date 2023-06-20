using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.Domain.Entity
{
    /// <summary>
    ///     Base directory for selected and ignored files.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseItem"/> -&gt; <see cref="BaseDirectory"/> -&gt;
    ///         MainDirectory <br/><b> Implements: </b><see cref="IComparable{BaseDirectory}"/>,
    ///         <see cref="IDirectory"/>, <see cref="IItem"/>, <see cref="IMainDirectory"/>
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     This directory will be searched for .md files. Saved paths of all selected and ignored
    ///     files, associated with this instance, will be relative to this instance path ( <see cref="BaseItem.Name"/>).
    /// </remarks>
    /// <seealso cref="BaseDirectory"> MdFilesMerger.Domain.Common.BaseDirectory </seealso>
    /// <seealso cref="BaseItem"> MdFilesMerger.Domain.Common.BaseItem </seealso>
    /// <seealso cref="IComparable{T}"> System.IComparable&lt;T&gt; </seealso>
    /// <seealso cref="IDirectory"> MdFilesMerger.Domain.Abstract.IDirectory </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    /// <seealso cref="IMainDirectory"> MdFilesMerger.Domain.Abstract.IMainDirectory </seealso>
    public sealed class MainDirectory : BaseDirectory, IMainDirectory
    {
        /// <inheritdoc/>
        public MainDirectory() : base()
        {
            MergedFileId = 0;
        }

        /// <inheritdoc/>
        public MainDirectory(int id) : base(id)
        {
            MergedFileId = 0;
        }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <remarks>
        ///     If <paramref name="path"/> is a valid path of existing directory, <see
        ///     cref="BaseItem.Name"/> is set to absolute path to this directory, that uses <see
        ///     langword="'/'"/> as directory separator character. Otherwise <see
        ///     cref="BaseItem.Name"/> is set to <see langword="null"/>.
        /// </remarks>
        /// <param name="path">
        ///     Path to the existing directory that you want to associate with this object. It can
        ///     be absolute or relative to the current directory.
        /// </param>
        public MainDirectory(string? path) : base()
        {
            SetPath(path);
            MergedFileId = 0;
        }

        /// <summary>
        ///     Sets <see cref="BaseItem.Id"/> to <paramref name="id"/>, <see cref="BaseItem.Name"/>
        ///     to adjusted <paramref name="path"/> or <see langword="null"/> and <see
        ///     cref="MergedFileId"/> to <paramref name="mergedFileId"/>.
        /// </summary>
        /// <remarks>
        ///     If <paramref name="path"/> is a valid path of existing directory, <see
        ///     cref="BaseItem.Name"/> is set to absolute path to this directory, that uses <see
        ///     langword="'/'"/> as directory separator character. Otherwise <see
        ///     cref="BaseItem.Name"/> is set to <see langword="null"/>.
        /// </remarks>
        /// <param name="id"> Identification number of item. </param>
        /// <param name="path">
        ///     Path to the existing directory that you want to associate with this object. It can
        ///     be absolute or relative to the current directory.
        /// </param>
        /// <param name="mergedFileId">
        ///     Identification number of <see cref="MergedFile"/> object associated with this instance.
        /// </param>
        public MainDirectory(int id, string? path, int mergedFileId) : base(id)
        {
            SetPath(path);
            MergedFileId = mergedFileId;
        }

        /// <summary>
        ///     Sets <see cref="BaseItem.Id"/> to <paramref name="id"/>, <see cref="BaseItem.Name"/>
        ///     to <paramref name="path"/>, <see cref="MergedFileId"/> to <paramref
        ///     name="mergedFileId"/> and <see cref="BaseDirectory.ModifiedDate"/> to <paramref name="modifiedDate"/>.
        /// </summary>
        /// <remarks>
        ///     No evaluation or adjusting is performed, so use this method only for already
        ///     evaluated and prepared data, for example from database.
        /// </remarks>
        /// <param name="id"> The item identification number. </param>
        /// <param name="path">
        ///     Valid absolute path to existing directory associated with this item. Path should use
        ///     <see langword="'/'"/> as directory separator.
        /// </param>
        /// <param name="mergedFileId">
        ///     Identification number of <see cref="MergedFile"/> object associated with this instance.
        /// </param>
        /// <param name="modifiedDate"> Date and time of last modification of this entity. </param>
        public MainDirectory(int id, string? path, int mergedFileId, DateTime modifiedDate) : base(id, path, modifiedDate)
        {
            MergedFileId = mergedFileId;
        }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <value> By default set to <see langword="0"/>. </value>
        public int MergedFileId { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj != null && obj is IMainDirectory other)
            {
                return this.Name == other.Name && this.MergedFileId == other.MergedFileId;
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        ///     Sets <see cref="BaseItem.Name"/> as absolute path of existing directory or <see
        ///     langword="null"/> if <paramref name="path"/> is invalid.
        /// </summary>
        /// <param name="path">
        ///     Path to the existing directory that you want to associate with this object. It can
        ///     be absolute or relative to the current directory.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if path was successfully set, or <see langword="false"/> if
        ///     given <paramref name="path"/> is invalid or points to a non-existent directory.
        /// </returns>
        public override bool SetPath(string? path)
        {
            DateTime oldModifiedDate = ModifiedDate;
            if (base.SetPath(path) && Directory.Exists(Name))
            {
                Name = Path.GetFullPath(Name).Replace('\\', '/');
                return true;
            }
            else
            {
                Name = null;
                ModifiedDate = oldModifiedDate;
                return false;
            }
        }
    }
}