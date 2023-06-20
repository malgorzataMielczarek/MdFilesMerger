using MdFilesMerger.App.Abstract;
using MdFilesMerger.Controller.Concrete;
using MdFilesMerger.Domain.Abstract;

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
    /// <seealso cref="IUserService"> MdFilesMerger.App.Abstract.IUserService </seealso>
    /// <seealso cref="ICRUDManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.ICRUDManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="UserManager"> MdFilesMerger.Controller.Concrete.UserManager </seealso>
    /// <seealso cref="IUser"> MdFilesMerger.Domain.Abstract.IUser </seealso>
    public interface IUserManager : ICRUDManager<IUser, IUserService>
    {
        /// <summary>
        ///     The merged file manager.
        /// </summary>
        IMergedFileManager MergedFileManager { get; }

        /// <summary>
        ///     Updates the password of selected item.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> if password updated successfully; otherwise <see langword="false"/>.
        /// </returns>
        bool UpdatePassword();
    }
}