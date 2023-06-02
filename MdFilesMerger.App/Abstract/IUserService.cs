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
    }
}