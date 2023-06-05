using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.Domain.Entity
{
    /// <summary>
    ///     Implementation of ignored file model associated with .md file located in main directory,
    ///     whose content won't be included in created merged file.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseItem"/> -&gt; <see cref="BaseDirectory"/> -&gt;
    ///         <see cref="MdFile"/> -&gt; <see cref="RelativeFile"/> -&gt; IgnoredFile <br/><b>
    ///         Implements: </b><see cref="IComparable{BaseDirectory}"/>, <see
    ///                     cref="Abstract.IDirectory"/>, <see cref="Abstract.IItem"/>, <see cref="Abstract.IRelativeFile"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="BaseDirectory"> MdFilesMerger.Domain.Common.BaseDirectory </seealso>
    /// <seealso cref="BaseItem"> MdFilesMerger.Domain.Common.BaseItem </seealso>
    /// <seealso cref="MdFile"> MdFilesMerger.Domain.Common.MdFile </seealso>
    /// <seealso cref="RelativeFile"> MdFilesMerger.Domain.Common.RelativeFile </seealso>
    /// <seealso cref="IComparable{T}"> System.IComparable&lt;T&gt; </seealso>
    /// <seealso cref="Abstract.IDirectory"> MdFilesMerger.Domain.Abstract.IDirectory </seealso>
    /// <seealso cref="Abstract.IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    /// <seealso cref="Abstract.IRelativeFile"> MdFilesMerger.Domain.Abstract.IRelativeFile </seealso>
    public sealed class IgnoredFile : RelativeFile
    {
        /// <inheritdoc/>
        public IgnoredFile()
        { }

        /// <inheritdoc/>
        public IgnoredFile(int id) : base(id) { }

        /// <inheritdoc/>
        public IgnoredFile(int id, string? path, int mainDirId, DateTime modifiedDate) : base(id, path, mainDirId, modifiedDate) { }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj != null && obj is IgnoredFile other)
            {
                return this.Name == other.Name && this.MainDirId == other.MainDirId;
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode() => base.GetHashCode();
    }
}