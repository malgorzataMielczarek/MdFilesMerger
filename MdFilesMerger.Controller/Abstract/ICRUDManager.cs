using MdFilesMerger.App.Abstract;
using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.Controller.Abstract
{
    /// <summary>
    ///     Interface with base CRUD methods for manager.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IManager{T, TService}"/> -&gt; ICRUDManager&lt;T,
    ///         TService&gt; <br/><b> Implementations: </b><see cref="Common.RelativeFileManager{T,
    ///         TService}"/>, <see cref="Concrete.IgnoredFileManager"/>, <see
    ///         cref="Concrete.MainDirectoryManager"/>, <see cref="Concrete.MergedFileManager"/>,
    ///         <see cref="Concrete.SelectedFileManager"/>, <see cref="Concrete.UserManager"/>
    ///     </para>
    /// </summary>
    /// <typeparam name="T"> Type of model. </typeparam>
    /// <typeparam name="TService">
    ///     Type of CRUD service handling collection of specified models.
    /// </typeparam>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="IManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="Common.RelativeFileManager{T, TService}">
    ///     MdFilesMerger.Controller.Common.RelativeFileManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="Concrete.IgnoredFileManager"> MdFilesMerger.Controller.Concrete.IgnoredFileManager </seealso>
    /// <seealso cref="Concrete.MainDirectoryManager"> MdFilesMerger.Controller.Concrete.MainDirectoryManager </seealso>
    /// <seealso cref="Concrete.MergedFileManager"> MdFilesMerger.Controller.Concrete.MergedFileManager </seealso>
    /// <seealso cref="Concrete.SelectedFileManager"> MdFilesMerger.Controller.Concrete.SelectedFileManager </seealso>
    /// <seealso cref="Concrete.UserManager"> MdFilesMerger.Controller.Concrete.UserManager </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    public interface ICRUDManager<T, TService> : IManager<T, TService> where T : IItem where TService : ICRUDService<T>
    {
        /// <summary>
        ///     Creates new entity of the appropriate model, adds it to service collection and sets
        ///     as selected item.
        /// </summary>
        /// <param name="connectedItemId">
        ///     The identifier of the connected item. If models of this type aren't connected with
        ///     any other model, value of this parameter doesn't matter.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if new entity was successfully created; otherwise <see langword="false"/>.
        /// </returns>
        public bool Create(int connectedItemId);

        /// <summary>
        ///     Deletes from the service's collection selected item and all elements of other
        ///     models, that are connected with it.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> if selected item was successfully deleted; otherwise <see langword="false"/>.
        /// </returns>
        public bool Delete();

        /// <summary>
        ///     Deletes from the service's collection all elements connected with the item with
        ///     specified identifier. Removing every element includes also removing all elements of
        ///     other models that are connected with it.
        /// </summary>
        /// <param name="connectedItemId">
        ///     The identifier of the connected item. If models of this type aren't connected with
        ///     any other model, value of this parameter doesn't matter.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if all elements was successfully deleted; otherwise <see langword="false"/>.
        /// </returns>
        public bool Delete(int connectedItemId);
    }
}