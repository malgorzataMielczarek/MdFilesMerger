using System.Diagnostics.CodeAnalysis;

namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    ///     Base interface for all directory or file related models.
    /// </summary>
    public interface IDirectory : IItem
    {
        /// <summary>
        ///     Gets directory/file path held in <see cref="IItem.Name"/> property.
        /// </summary>
        [return: NotNullIfNotNull(nameof(Name))]
        public string? GetPath();

        /// <summary>
        ///     Sets <see cref="IItem.Name"/> as <paramref name="path"/>.
        /// </summary>
        public bool SetPath([NotNullWhen(true)] string? path);
    }
}