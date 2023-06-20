namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    ///     Base interface for all models.
    ///     <para>
    ///         <b> Inheritance: </b> IItem <br/><b> Implementations: </b><see
    ///         cref="Common.BaseItem"/>, <see cref="Common.BaseDirectory"/>, <see
    ///         cref="Common.MdFile"/>, <see cref="Common.RelativeFile"/>, <see
    ///         cref="Entity.IgnoredFile"/>, <see cref="Entity.MainDirectory"/>, <see
    ///         cref="Entity.MenuAction"/>, <see cref="Entity.MergedFile"/>, <see
    ///         cref="Entity.SelectedFile"/>, <see cref="Entity.User"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="Common.BaseDirectory"> MdFilesMerger.Domain.Common.BaseDirectory </seealso>
    /// <seealso cref="Common.BaseItem"> MdFilesMerger.Domain.Common.BaseItem </seealso>
    /// <seealso cref="Common.MdFile"> MdFilesMerger.Domain.Common.MdFile </seealso>
    /// <seealso cref="Common.RelativeFile"> MdFilesMerger.Domain.Common.RelativeFile </seealso>
    /// <seealso cref="Entity.IgnoredFile"> MdFilesMerger.Domain.Entity.IgnoredFile </seealso>
    /// <seealso cref="Entity.MainDirectory"> MdFilesMerger.Domain.Entity.MainDirectory </seealso>
    /// <seealso cref="Entity.MenuAction"> MdFilesMerger.Domain.Entity.MenuAction </seealso>
    /// <seealso cref="Entity.MergedFile"> MdFilesMerger.Domain.Entity.MergedFile </seealso>
    /// <seealso cref="Entity.SelectedFile"> MdFilesMerger.Domain.Entity.SelectedFile </seealso>
    /// <seealso cref="Entity.User"> MdFilesMerger.Domain.Entity.User </seealso>
    public interface IItem
    {
        /// <summary>
        ///     Distinct identification number of item.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        ///     Description of item. It will be description of menu element, user name or path of directory/file.
        /// </summary>
        string? Name { get; set; }
    }
}