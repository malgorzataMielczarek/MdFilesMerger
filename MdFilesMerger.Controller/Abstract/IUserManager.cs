using MdFilesMerger.App.Concrete;
using MdFilesMerger.Controller.Concrete;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.Controller.Abstract
{
    /// <summary>
    ///     Interface with additional methods and properties of manager for user model.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IManager{T, TService}"/> -&gt; <see
    ///         cref="ICRUDManager{T, TService}"/> -&gt; IUserManager <br/><b> Implementations:
    ///         </b><see cref="UserManager"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="UserService"> MdFilesMerger.App.Concrete.UserService </seealso>
    /// <seealso cref="ICRUDManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.ICRUDManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="UserManager"> MdFilesMerger.Controller.Concrete.UserManager </seealso>
    /// <seealso cref="User"> MdFilesMerger.Domain.Entity.User </seealso>
    public interface IUserManager : ICRUDManager<User, UserService>
    {
        /// <summary>
        ///     The merged file manager.
        /// </summary>
        public MergedFileManager MergedFileManager { get; }

        /// <summary>
        ///     Updates the password of selected item.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> if password updated successfully; otherwise <see langword="false"/>.
        /// </returns>
        public bool UpdatePassword();
    }
}