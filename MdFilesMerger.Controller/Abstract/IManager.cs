using MdFilesMerger.App.Abstract;
using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.Controller.Abstract
{
    /// <summary>
    ///     Base manager interface for all models.
    ///     <para> <b> Inheritance: </b> IManager&lt;T, U&gt; <br/><b> Implementations: </b> </para>
    /// </summary>
    /// <typeparam name="T"> Type of model. </typeparam>
    /// <typeparam name="U"> Type of service handling collection of specified models. </typeparam>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt;} </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    public interface IManager<T, U> where T : IItem where U : IService<T>
    {
        /// <summary>
        ///     The element of the service's collection that was last selected by one of the
        ///     manager's methods.
        /// </summary>
        /// <value> By default it is set to <see langword="null"/>, before any item is selected. </value>
        public int SelectedItem { get; }

        /// <summary>
        ///     Service handling collection of objects implementing model associated with this manager.
        /// </summary>
        /// <value> The appropriate service. Service object is created in constructor. </value>
        public U Service { get; }

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
        /// <returns> Returns the identification number of the selected item. </returns>
        public void Select(int connectedItemId);
    }
}