namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    ///     Interface of user model.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IItem"/> -&gt; IUser <br/><b> Implementations:
    ///         </b><see cref="Entity.User"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    /// <seealso cref="Entity.User"> MdFilesMerger.Domain.Entity.User </seealso>
    public interface IUser : IItem
    {
        /// <summary>
        ///     Encrypted password used to authorize user.
        /// </summary>
        public byte[]? Password { get; set; }

        /// <summary>
        ///     Value used for password encryption/decryption.
        /// </summary>
        public byte[] Salt { get; set; }
    }
}