namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    ///     Interface of selected file model.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IItem"/> -&gt; <see cref="IDirectory"/> -&gt; <see
    ///         cref="IEditFile"/> -&gt; ISelectedFile <br/><see cref="IItem"/> -&gt; <see
    ///         cref="IDirectory"/> -&gt; <see cref="IRelativeFile"/> -&gt; ISelectedFile <br/><b>
    ///         Implementations: </b><see cref="Entity.SelectedFile"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="IEditFile"> MdFilesMerger.Domain.Abstract.IEditFile </seealso>
    /// <seealso cref="IRelativeFile"> MdFilesMerger.Domain.Abstract.IRelativeFile </seealso>
    public interface ISelectedFile : IEditFile, IRelativeFile
    {
        /// <summary>
        ///     Opens the file and reads first not empty line of text from it.
        /// </summary>
        /// <param name="mainDirPath"> Absolute path of main directory. </param>
        /// <returns>
        ///     If read line is a header (starts with '#') return it. Else return filename, without
        ///     extension. If file header is a link return only text part.
        /// </returns>
        public string GetFileHeader(string? mainDirPath);

        /// <summary>
        ///     Converts to <see cref="Entity.IgnoredFile"/>.
        /// </summary>
        /// <returns>
        ///     New <see cref="Entity.IgnoredFile"/> object associated with the same file and
        ///     connected with the same <see cref="Entity.MainDirectory"/> object as this instance.
        /// </returns>
        public Entity.IgnoredFile ToIgnoredFile();
    }
}