﻿using MdFilesMerger.App.Abstract;
using MdFilesMerger.App.Common;
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
    /// <seealso cref="User"> MdFilesMerger.Domain.Entity.User </seealso>
    public class UserService : BaseService<User>, IUserService
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
        public User? ReadByName(string name)
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

        private void Initialize()
        {
            Create(new User(1, "user", "password"));
        }
    }
}