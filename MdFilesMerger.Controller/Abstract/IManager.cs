using MdFilesMerger.App.Abstract;
using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.Controller.Abstract
{
    /// <summary>
    ///     Base manager interface for all models.
    ///     <para>
    ///         <b> Inheritance: </b> IManager&lt;T, TService&gt; <br/><b> Implementations: </b><see
    ///         cref="Common.BaseManager{T, TService}"/>, <see cref="Common.RelativeFileManager{T,
    ///         TService}"/>, <see cref="Concrete.IgnoredFileManager"/>, <see
    ///         cref="Concrete.MainDirectoryManager"/>, <see cref="Concrete.MenuActionManager"/>,
    ///         <seealso cref="Concrete.MergedFileManager"/>, <see
    ///         cref="Concrete.SelectedFileManager"/>, <see cref="Concrete.UserManager"/>
    ///     </para>
    /// </summary>
    /// <typeparam name="T"> Type of model. </typeparam>
    /// <typeparam name="TService"> Type of service handling collection of specified models. </typeparam>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt;} </seealso>
    /// <seealso cref="Common.BaseManager{T, TService}">
    ///     MdFilesMerger.Controller.Common.BaseManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="Common.RelativeFileManager{T, TService}">
    ///     MdFilesMerger.Controller.Common.RelativeFileManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="Concrete.IgnoredFileManager"> MdFilesMerger.Controller.Concrete.IgnoredFileManager </seealso>
    /// <seealso cref="Concrete.MainDirectoryManager"> MdFilesMerger.Controller.Concrete.MainDirectoryManager </seealso>
    /// <seealso cref="Concrete.MenuActionManager"> MdFilesMerger.Controller.Concrete.MenuActionManager </seealso>
    /// <seealso cref="Concrete.MergedFileManager"> MdFilesMerger.Controller.Concrete.MergedFileManager </seealso>
    /// <seealso cref="Concrete.SelectedFileManager"> MdFilesMerger.Controller.Concrete.SelectedFileManager </seealso>
    /// <seealso cref="Concrete.UserManager"> MdFilesMerger.Controller.Concrete.UserManager </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    public interface IManager<T, TService> where T : IItem where TService : IService<T>
    {
        /// <summary>
        ///     The identifier of the element of the service's collection that was last selected by
        ///     one of the manager's methods.
        /// </summary>
        /// <value> By default it is set to <see langword="-1"/>, before any item is selected. </value>
        public int SelectedItem { get; }

        /// <summary>
        ///     Service handling collection of objects implementing model associated with this manager.
        /// </summary>
        /// <value> The appropriate service. Service object is created in constructor. </value>
        public TService Service { get; }

        /// <summary>
        ///     Displays information about the currently selected item.
        /// </summary>
        public void Display();

        /// <summary>
        ///     Displays the title of the manager.
        /// </summary>
        public void DisplayTitle();

        /// <summary>
        ///     Selects the item from service's collection.
        /// </summary>
        /// <remarks>
        ///     Asks user for appropriate data and selects the item (sets <see cref="SelectedItem"/>
        ///     value) based on the input.
        /// </remarks>
        /// <param name="connectedItemId">
        ///     If model supported by this manager is connected with another model, it is the
        ///     identifier of appropriate connected object. Otherwise parameter's value doesn't matter.
        /// </param>
        public void Select(int connectedItemId);
    }
}