namespace MdFilesMerger.Domain.Abstract
{
    /// <summary>
    /// User interface
    /// </summary>
    public interface IUser : IItem
    {
        /// <value>Encrypted password used to authorize user</value>
        public string? Password { get; set; }

        ///<value>Value used for password encryptation/decryptation</value>
        public string Salt { get; }
    }
}
