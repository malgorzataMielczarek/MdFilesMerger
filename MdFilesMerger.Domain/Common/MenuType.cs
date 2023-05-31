namespace MdFilesMerger.Domain.Common
{
    /// <summary>
    ///     Enum with existing menus.
    /// </summary>
    public enum MenuType
    {
        /// <summary>
        ///     Menu with basic actions of the program.
        /// </summary>
        Main = 1,

        /// <summary>
        ///     Menu to choose type of table of contents.
        /// </summary>
        TableOfContents = 2,

        /// <summary>
        ///     Menu to choose merged file settings to change.
        /// </summary>
        MergedFile = 3
    }
}