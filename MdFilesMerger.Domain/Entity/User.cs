using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;
using System.Diagnostics.CodeAnalysis;

namespace MdFilesMerger.Domain.Entity
{
    /// <summary>
    ///     Implementation of user model.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseItem"/> -&gt; User <br/><b> Implements: </b><see
    ///         cref="IItem"/>, <see cref="IUser"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="BaseItem"> MdFilesMerger.Domain.Entity.BaseItem </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    /// <seealso cref="IUser"> MdFilesMerger.Domain.Abstract.IUser </seealso>
    public sealed class User : BaseItem, IUser
    {
        /// <summary>
        ///     Sets default values for all properties.
        /// </summary>
        /// <remarks>
        ///     Sets <see cref="BaseItem.Id"/> to <see langword="0"/>, <see cref="BaseItem.Name"/>
        ///     to <see langword="null"/>, <see cref="Password"/> to <see langword="null"/> and <see
        ///     cref="Salt"/> to auto generated random value.
        /// </remarks>
        public User() : this(0) { }

        /// <summary>
        ///     Sets <see cref="BaseItem.Id"/> to <paramref name="id"/> and default for other properties.
        /// </summary>
        /// <remarks>
        ///     Sets <see cref="BaseItem.Name"/> to <see langword="null"/>, <see cref="Password"/>
        ///     to <see langword="null"/> and <see cref="Salt"/> to auto generated random value.
        /// </remarks>
        /// <param name="id"> Identification number of this user. </param>
        public User(int id) : this(id, null, null) { }

        /// <summary>
        ///     Sets <see cref="BaseItem.Id"/> to <see langword="0"/>, <see cref="BaseItem.Name"/>
        ///     to <paramref name="userName"/>, <see cref="Password"/> to encrypted <paramref
        ///     name="password"/> value and <see cref="Salt"/> to auto generated random value.
        /// </summary>
        /// <param name="userName"> User's user name saved in <see cref="BaseItem.Name"/>. </param>
        /// <param name="password">
        ///     User's password before encryption. It will be encrypted and saved in <see cref="Password"/>
        /// </param>
        public User(string userName, string password) : this(0, userName, password) { }

        /// <summary>
        ///     Sets <see cref="BaseItem.Id"/> to <paramref name="id"/>, <see cref="BaseItem.Name"/>
        ///     to <paramref name="userName"/>, <see cref="Password"/> to encrypted <paramref
        ///     name="password"/> value and <see cref="Salt"/> to auto generated random value.
        /// </summary>
        /// <param name="id"> Identification number of this user. </param>
        /// <param name="userName"> User's user name saved in <see cref="BaseItem.Name"/>. </param>
        /// <param name="password">
        ///     User's password before encryption. It will be encrypted and saved in <see cref="Password"/>
        /// </param>
        public User(int id, string? userName, string? password) : base(id, userName)
        {
            SetRandomSalt();
            SetPassword(password);
        }

        /// <summary>
        ///     Sets <see cref="BaseItem.Id"> Id </see> to <paramref name="id"/>, <see
        ///     cref="BaseItem.Name"> Name </see> to <paramref name="userName"/>, <see
        ///     cref="Password"> Password </see> to <paramref name="password"/> and <see
        ///     cref="Salt"> Salt </see> to <paramref name="salt"/>.
        /// </summary>
        /// <remarks> Can be used for example when getting data from database. </remarks>
        /// <param name="id"> Identification number of item. </param>
        /// <param name="userName"> User name (login) of this user. </param>
        /// <param name="password"> Encoded user's password. </param>
        /// <param name="salt"> Array of bytes used for encoding/decoding password. </param>
        public User(int id, string userName, byte[] password, byte[] salt) : base(id, userName)
        {
            Password = password;
            Salt = salt;
        }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <value> By default set to <see langword="null"/>. </value>
        public byte[]? Password { get; set; }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <value> Set to auto-generated random value. </value>
        public byte[] Salt { get; set; }

        /// <summary>
        ///     Checks if decrypted <see cref="Password"/> is equal to <paramref name="password"/>
        /// </summary>
        /// <param name="password"> Value compared with decrypted <see cref="Password"/> value. </param>
        /// <returns>
        ///     <see langword="true"/> if <paramref name="password"/> and decrypted <see
        ///     cref="Password"/> have the same value; otherwise, <see langword="false"/>
        /// </returns>
        public bool PasswordEquals(string? password)
        {
            return password?.Equals(DecryptePassword()) ?? Password == null;
        }

        /// <summary>
        ///     Sets <see cref="Password"/> to encrypted <paramref name="password"/> value or <see
        ///     langword="null"/> value.
        /// </summary>
        /// <remarks>
        ///     <see cref="Password"/> is set to <see langword="null"/> if <paramref
        ///     name="password"/> is <see langword="null"/>, <see cref="string.Empty"/> or consists
        ///     exclusively of white-space characters. Otherwise <paramref name="password"/> is
        ///     encrypted using <see cref="EncryptePassword(string)"/> method and <see cref="Salt"/>
        ///     value. <see cref="Password"/> is set to returned value.
        /// </remarks>
        /// <param name="password"> User's decrypted password </param>
        /// <returns>
        ///     <see langword="false"/> if <see cref="Password"/> set to <see langword="null"/>;
        ///     otherwise <see langword="true"/>
        /// </returns>
        [MemberNotNullWhen(true, nameof(Password))]
        public bool SetPassword(string? password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                Password = null;
                return false;
            }
            else
            {
                Password = EncryptePassword(password);
                return true;
            }
        }

        // TODO: Decrypt password method
        private string? DecryptePassword()
        {
            throw new NotImplementedException();
        }

        // TODO: Encrypt password method
        private byte[] EncryptePassword(string password)
        {
            throw new NotImplementedException();
        }

        // TODO: Generating random salt value
        [MemberNotNull(nameof(Salt))]
        private void SetRandomSalt()
        {
            Salt = new byte[16];
            var random = new Random();
            random.NextBytes(Salt);
        }
    }
}