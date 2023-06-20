namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    ///     Interface for ignored file model.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IItem"/> -&gt; <see cref="IDirectory"/> -&gt; <see
    ///         cref="IRelativeFile"/> -&gt; IIgnoredFile <br/><b> Implementations: </b><see cref="Entity.IgnoredFile"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="IDirectory"> MdFilesMerger.Domain.Abstract.IDirectory </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    /// <seealso cref="IRelativeFile"> MdFilesMerger.Domain.Abstract.IRelativeFile </seealso>
    /// <seealso cref="Entity.IgnoredFile"> MdFilesMerger.Domain.Entity.IgnoredFile </seealso>
    public interface IIgnoredFile : IRelativeFile
    {
    }
}