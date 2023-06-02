using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.App.Abstract
{
    /// <summary>
    ///     Defines methods to service <see cref="User"/> objects collection.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IService{T}"/> -&gt; <see cref="ICRUDService{T}"/>
    ///         -&gt; IUserService <br/><b> Implementations: </b><see cref="Concrete.UserService"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="Concrete.UserService"> MdFilesMerger.App.Concrete.UserService </seealso>
    /// <seealso cref="User"> MdFilesMerger.Domain.Entity.User </seealso>
    public interface IUserService : ICRUDService<User>
    {
        /// <summary>
        ///     Check the credentials.
        /// </summary>
        /// <param name="userName"> Name of the user. </param>
        /// <param name="password"> The password. </param>
        /// <returns>
        ///     Identification number of user with specified name if it is in the collection and
        ///     specified password is correct; otherwise <see langword="-1"/>.
        /// </returns>
        int CheckCredentials(string userName, string password);

        /// <summary>
        ///     From stored collection of <see cref="User"/> objects, gets user with specified name.
        /// </summary>
        /// <param name="name"> The name of user that you are looking for. </param>
        /// <returns>
        ///     <see cref="User"/> object with <see cref="Domain.Common.BaseItem.Name"/> equal
        ///     <paramref name="name"/>. If there is no element with specified name in stored
        ///     collection, method returns <see langword="null"/>.
        /// </returns>
        public User? ReadByName(string name);

        /// <summary>
        ///     Updates the name of user with specified identification number.
        /// </summary>
        /// <param name="id"> The identification number of updated user. </param>
        /// <param name="name"> The new name of the user. </param>
        /// <returns>
        ///     Id of updated user, unless:
        ///     <list type="bullet">
        ///         <item>
        ///             name is <see langword="null"/>, <see cref="string.Empty"/> or consists only
        ///             from white-spaces characters,
        ///         </item>
        ///         <item>
        ///             in the collection, there is no user with specified id, or user with
        ///             specified name already exists,
        ///         </item>
        ///     </list>
        ///     then <see langword="-1"/>.
        /// </returns>
        public int UpdateName(int id, string name);

        /// <summary>
        ///     Updates the password of user with specified identification number.
        /// </summary>
        /// <param name="id"> The identification number of updated user. </param>
        /// <param name="password"> The new password of the user. </param>
        /// <returns>
        ///     Id of updated user, unless, in the collection, there is no user with specified id,
        ///     or specified password is <see langword="null"/>, <see cref="string.Empty"/> or
        ///     consists only from white-spaces characters.
        /// </returns>
        public int UpdatePassword(int id, string password);
    }
}