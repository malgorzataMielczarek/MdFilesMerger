namespace MdFilesMerger.Domain.Common
{
    /// <summary>
    ///     Types of table of contents that can be placed in merged file.
    /// </summary>
    public enum TableOfContents
    {
        /// <summary>
        ///     Table of contents and it's header won't be included.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Table of contents that is a plain text, or more specifically a set of appropriate
        ///     level headers.
        /// </summary>
        Text = 1,

        /// <summary>
        ///     Table of contents that contains appropriate level headers with hyperlinks to
        ///     connected paragraphs.
        /// </summary>
        Hyperlink = 2
    }
}