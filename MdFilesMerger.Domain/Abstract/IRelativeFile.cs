using System.Diagnostics.CodeAnalysis;

namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    ///     Interface for models associated with the <see cref="IMainDirectory"/> instance (selected
    ///     and ignored file models).
    /// </summary>
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
    }
}