using MdFilesMerger.App.Abstract;
using MdFilesMerger.App.Common;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.App.Concrete
{
    /// <summary>
    ///     A class that handles a list of User objects.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseService{T}"/> -&gt; UserService <br/><b>
    ///         Implements: </b><see cref="ICRUDService{T}"/>, <see cref="IService{T}"/>, <see cref="IUserService"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="IUserService"> MdFilesMerger.App.Abstract.IUserService </seealso>
    /// <seealso cref="BaseService{T}"> MdFilesMerger.App.Common.BaseService&lt;T&gt; </seealso>
    /// <seealso cref="IUser"> MdFilesMerger.Domain.Abstract.IUser </seealso>
    /// <seealso cref="User"> MdFilesMerger.Domain.Entity.User </seealso>
    public sealed class UserService : BaseService<IUser>, IUserService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        public UserService() : base()
        {
            Initialize();
        }

        /// <inheritdoc/>
        public int CheckCredentials(string userName, string password)
        {
            var user = ReadByName(userName);

            if (user != null && user.PasswordEquals(password))
            {
                return user.Id;
            }

            return -1;
        }

        /// <inheritdoc/>
        public int UpdateName(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return -1;
            }

            IUser? oldUser = ReadById(id);

            if (oldUser == null)
            {
                return -1;
            }
            else
            {
                if (oldUser.Name == name)
                {
                    return oldUser.Id;
                }

                if (ReadByName(name) == null)
                {
                    oldUser.Name = name;
                    return oldUser.Id;
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <inheritdoc/>
        public int UpdatePassword(int id, string password)
        {
            IUser? user = ReadById(id);

            if (user != null)
            {
                var oldPwd = user.Password;
                if (user.SetPassword(password))
                {
                    return user.Id;
                }
                else
                {
                    user.Password = oldPwd;
                }
            }

            return -1;
        }

        //TODO: Set default user password after implementing encrypting/decrypting methods of User class
        private void Initialize()
        {
            Create(new User(1, "user", null));
        }

        private IUser? ReadByName(string name)
        {
            foreach (var item in _items)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }

            return null;
        }
    }
}