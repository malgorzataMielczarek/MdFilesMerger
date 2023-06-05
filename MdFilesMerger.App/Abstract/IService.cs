using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.App.Abstract
{
    /// <summary>
    ///     Base interface for all services in <see cref="App"/> project.
    ///     <para>
    ///         <b> Inheritance: </b> IService&lt;T&gt; <br/><b> Implementations: </b><see
    ///         cref="Common.BaseDirectoryService{T}"/>, <see cref="Common.BaseService{T}"/>, <see
    ///         cref="Concrete.MainDirectoryService"/>, <see cref="Concrete.MenuActionService"/>,
    ///         <see cref="Concrete.UserService"/>
    ///     </para>
    /// </summary>
    /// <typeparam name="T"> Type of stored and serviced objects. </typeparam>
    /// <seealso cref="Common.BaseDirectoryService{T}"> MdFilesMerger.App.Common.BaseDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="Common.BaseService{T}"> MdFilesMerger.App.Common.BaseService&lt;T&gt; </seealso>
    /// <seealso cref="Concrete.MenuActionService"> MdFilesMerger.App.Concrete.MenuActionService </seealso>
    /// <seealso cref="Concrete.MainDirectoryService"> MdFilesMerger.App.Concrete.MainDirectoryService </seealso>
    /// <seealso cref="Concrete.UserService"> MdFilesMerger.App.Concrete.UserService </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    public interface IService<T> where T : IItem
    {
        /// <summary>
        ///     Gets stored <typeparamref name="T"/> object by it's identification number.
        /// </summary>
        /// <param name="id"> Identification number of searched object. </param>
        /// <returns>
        ///     Found <typeparamref name="T"/> object, or <see langword="null"/>, if no object was found.
        /// </returns>
        public T? ReadById(int id);
    }
}