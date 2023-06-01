using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.App.Abstract
{
    /// <summary>
    ///     Base interface for all services in <see cref="App"/> project.
    ///     <para>
    ///         <b> Inheritance: </b> IService&lt;T&gt; <br/><b> Implementations: </b><see cref="Concrete.MenuActionService"/>
    ///     </para>
    /// </summary>
    /// <typeparam name="T"> Type of stored and serviced objects. </typeparam>
    /// <seealso cref="Concrete.MenuActionService"> MdFilesMerger.App.Concrete.MenuActionService </seealso>
    public interface IService<T> where T : IItem
    {
        /// <summary>
        ///     Gets stored <see cref="IItem"/> object by it's identification number.
        /// </summary>
        /// <param name="id"> Value of <see cref="IItem.Id"/> of searched object. </param>
        /// <returns>
        ///     Found <see cref="IItem"/> object, or <see langword="null"/>, if no object was found.
        /// </returns>
        public T? GetById(int id);
    }
}