namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    /// User interface
    /// </summary>
    public interface IUser : IItem
    {
        /// <value>Encrypted password used to authorize user</value>
        public byte[]? Password { get; set; }

        ///<value>Value used for password encryption/decryption</value>
        public byte[] Salt { get; set; }
    }
}
