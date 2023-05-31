using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.Domain.Entity
{
    /// <summary>
    ///     Implementation of ignored file model associated with .md file located in main directory,
    ///     whose content won't be included in created merged file.
    /// </summary>
    public sealed class IgnoredFile : RelativeFile
    {
        /// <inheritdoc/>
        public IgnoredFile()
        { }

        /// <inheritdoc/>
        public IgnoredFile(int id) : base(id) { }

        /// <inheritdoc/>
        public IgnoredFile(int id, string? path, int mainDirId) : base(id, path, mainDirId) { }
    }
}