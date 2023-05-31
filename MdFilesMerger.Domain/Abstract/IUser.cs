namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    ///     Interface of user model.
    /// </summary>
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