using MdFilesMerger.App.Abstract;
using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.Controller.Abstract
{
    /// <summary>
    ///     Interface with base CRUD methods for manager.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IManager{T, U}"/> -&gt; ICRUDManager&lt;T, U&gt;
    ///         <br/><b> Implementations: </b>
    ///     </para>
    /// </summary>
    /// <typeparam name="T"> Type of model. </typeparam>
    /// <typeparam name="U"> Type of CRUD service handling collection of specified models. </typeparam>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt;} </seealso>
    /// <seealso cref="IManager{T, U}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, U&gt;
    /// </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    public interface ICRUDManager<T, U> : IManager<T, U> where T : IItem where U : ICRUDService<T>
    {
        /// <summary>
        ///     Creates new entity of the appropriate model, adds it to service collection and sets
        ///     as selected item.
        /// </summary>
        /// <returns> The identification number of created entity. </returns>
        public bool Create(int connectedItemId);

        /// <summary>
        ///     Deletes selected item from the collection.
        /// </summary>
        /// <returns> The identification number of deleted item. </returns>
        public bool Delete();

        public bool Delete(int connectedItemId);
    }
}