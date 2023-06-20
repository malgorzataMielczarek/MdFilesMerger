using System.Diagnostics.CodeAnalysis;

namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    ///     Base interface for all directory or file related models.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IItem"/> -&gt; IDirectory <br/><see
    ///         cref="IComparable{T}"/> -&gt; IDirectory <br/><b> Implementations: </b><see
    ///         cref="Common.BaseDirectory"/>, <see cref="Common.MdFile"/>, <see
    ///         cref="Common.RelativeFile"/>, <see cref="Entity.IgnoredFile"/>, <see
    ///         cref="Entity.MainDirectory"/>, <see cref="Entity.MergedFile"/>, <see cref="Entity.SelectedFile"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    /// <seealso cref="Common.BaseDirectory"> MdFilesMerger.Domain.Common.BaseDictionary </seealso>
    /// <seealso cref="Common.MdFile"> MdFilesMerger.Domain.Common.MdFile </seealso>
    /// <seealso cref="Common.RelativeFile"> MdFilesMerger.Domain.Common.RelativeFile </seealso>
    /// <seealso cref="Entity.IgnoredFile"> MdFilesMerger.Domain.Entity.IgnoredFile </seealso>
    /// <seealso cref="Entity.MainDirectory"> MdFilesMerger.Domain.Entity.MainDirectory </seealso>
    /// <seealso cref="Entity.MergedFile"> MdFilesMerger.Domain.Entity.MergedFile </seealso>
    /// <seealso cref="Entity.SelectedFile"> MdFilesMerger.Domain.Entity.SelectedFile </seealso>
    public interface IDirectory : IItem, IComparable<IDirectory>
    {
        /// <summary>
        ///     Date and time of last update or creation if entry wasn't modified.
        /// </summary>
        DateTime ModifiedDate { get; set; }

        /// <summary>
        ///     Gets directory/file path held in <see cref="IItem.Name"/> property.
        /// </summary>
        [return: NotNullIfNotNull(nameof(Name))]
        string? GetPath();

        /// <summary>
        ///     Sets <see cref="IItem.Name"/> as <paramref name="path"/>.
        /// </summary>
        bool SetPath([NotNullWhen(true)] string? path);
    }
}