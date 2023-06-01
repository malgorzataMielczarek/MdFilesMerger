using MdFilesMerger.Domain.Abstract;
using System.Diagnostics.CodeAnalysis;

namespace MdFilesMerger.Domain.Common
{
    /// <summary>
    ///     Base class for all models associated with .md files (merged, selected and ignored file models).
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseItem"/> -&gt; <see cref="BaseDirectory"/> -&gt;
    ///         MdFile <br/><b> Implements: </b><see cref="IComparable{BaseDirectory}"/>, <see
    ///         cref="IDirectory"/>, <see cref="IItem"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="BaseDirectory"> MdFilesMerger.Domain.Common.BaseDirectory </seealso>
    /// <seealso cref="BaseItem"> MdFilesMerger.Domain.Common.BaseItem </seealso>
    /// <seealso cref="IComparable{T}"> System.IComparable&lt;T&gt; </seealso>
    /// <seealso cref="IDirectory"> MdFilesMerger.Domain.Abstract.IDirectory </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    public class MdFile : BaseDirectory, IDirectory
    {
        /// <inheritdoc/>
        public MdFile() : base() { }

        /// <inheritdoc/>
        public MdFile(int id) : base(id) { }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <remarks>
        ///     If <paramref name="path"/> is a valid path to .md file, <see cref="BaseItem.Name"/>
        ///     is set to path to this file, that uses <see langword="'/'"/> as directory separator
        ///     character. Otherwise <see cref="BaseItem.Name"/> is set to <see langword="null"/>.
        /// </remarks>
        /// <param name="path">
        ///     Path to the .md file associated with this item. It can include extension or not.
        /// </param>
        public MdFile(string? path) : base(path)
        {
            SetPath(path);
        }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <remarks>
        ///     If <paramref name="path"/> is a valid path to .md file, <see cref="BaseItem.Name"/>
        ///     is set to path to this file, that uses <see langword="'/'"/> as directory separator
        ///     character. Otherwise <see cref="BaseItem.Name"/> is set to <see langword="null"/>.
        /// </remarks>
        /// <param name="id"> This item identification number. </param>
        /// <param name="path">
        ///     Path to the .md file associated with this item. It can include extension or not.
        /// </param>
        public MdFile(int id, string? path) : base(id, path)
        {
            SetPath(path);
        }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <remarks>
        ///     <inheritdoc/> If <paramref name="path"/> doesn't end with ".md" it adds this
        ///     extension to <paramref name="path"/>.
        /// </remarks>
        /// <param name="path">
        ///     Valid absolute or relative path to the .md file, that you want to associate with
        ///     this item.
        /// </param>
        /// <returns> <inheritdoc/> </returns>
        public override bool SetPath([NotNullWhen(true)] string? path)
        {
            if (!path?.EndsWith(".md") ?? true)
            {
                path += ".md";
            }

            return base.SetPath(path);
        }
    }
}