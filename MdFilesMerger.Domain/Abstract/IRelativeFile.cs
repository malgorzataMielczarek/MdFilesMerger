using MdFilesMerger.Domain.Entity;
using System.Diagnostics.CodeAnalysis;

namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    ///     Interface for models associated with the <see cref="IMainDirectory"/> instance (selected
    ///     and ignored file models).
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IItem"/> -&gt; <see cref="IDirectory"/> -&gt;
    ///         IRelativeFile <br/><b> Implementations: </b><see cref="Common.RelativeFile"/>, <see
    ///         cref="Entity.IgnoredFile"/>, <see cref="Entity.SelectedFile"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="IDirectory"> MdFilesMerger.Domain.Abstract.IDirectory </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    /// <seealso cref="Common.RelativeFile"> MdFilesMerger.Domain.Common.RelativeFile </seealso>
    /// <seealso cref="Entity.IgnoredFile"> MdFilesMerger.Domain.Entity.IgnoredFile </seealso>
    /// <seealso cref="Entity.SelectedFile"> MdFilesMerger.Domain.Entity.SelectedFile </seealso>
    public interface IRelativeFile : IDirectory
    {
        /// <summary>
        ///     Identification number of the <see cref="Entity.MainDirectory"> MainDirectory </see>
        ///     object associated with the current instance.
        /// </summary>
        public int MainDirId { get; set; }

        /// <summary>
        ///     Gets absolute path of file associated with the current instance.
        /// </summary>
        [return: NotNullIfNotNull(nameof(Name))]
        public string? GetPath(string? mainDirPath);

        /// <summary>
        ///     Sets <see cref="IItem.Name"/> as path relative to <paramref name="mainDirPath"/> or
        ///     to <see langword="null"/>, if <paramref name="path"/> doesn't point to existing .md file.
        /// </summary>
        public bool SetPath([NotNullWhen(true)] string? path, string? mainDirPath);

        /// <summary>
        ///     Converts to <see cref="SelectedFile"/>.
        /// </summary>
        /// <param name="mainDirPath"> The main directory path. </param>
        /// <returns>
        ///     New <see cref="SelectedFile"/> object associated with the same file and connected
        ///     with the same <see cref="MainDirectory"/> object as this instance.
        /// </returns>
        public SelectedFile ToSelectedFile(string? mainDirPath);
    }
}