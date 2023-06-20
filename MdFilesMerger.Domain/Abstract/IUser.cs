using System.Diagnostics.CodeAnalysis;

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
        byte[]? Password { get; set; }

        /// <summary>
        ///     Value used for password encryption/decryption.
        /// </summary>
        byte[] Salt { get; set; }

        /// <summary>
        ///     Checks if decrypted <see cref="Password"/> is equal to <paramref name="password"/>
        /// </summary>
        /// <param name="password">
        ///     Value compared with decrypted <see cref="Password"/> value.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if <paramref name="password"/> and decrypted <see
        ///     cref="Password"/> have the same value; otherwise, <see langword="false"/>
        /// </returns>
        bool PasswordEquals(string? password);

        /// <summary>
        ///     Sets <see cref="Password"/> to encrypted <paramref name="password"/> value or <see
        ///     langword="null"/> value.
        /// </summary>
        /// <remarks>
        ///     <see cref="Password"/> is set to <see langword="null"/> if <paramref
        ///     name="password"/> is <see langword="null"/>, <see cref="string.Empty"/> or consists
        ///     exclusively of white-space characters. Otherwise <paramref name="password"/> is
        ///     encrypted with th use of <see cref="Salt"/> value. <see cref="Password"/> is set to
        ///     returned value.
        /// </remarks>
        /// <param name="password"> User's decrypted password </param>
        /// <returns>
        ///     <see langword="false"/> if <see cref="Password"/> set to <see langword="null"/>;
        ///     otherwise <see langword="true"/>
        /// </returns>
        [MemberNotNullWhen(true, nameof(Password))]
        bool SetPassword(string? password);
    }
}