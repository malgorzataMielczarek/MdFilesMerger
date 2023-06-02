using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.App.Abstract
{
    /// <summary>
    ///     Interface with base CRUD functionalities to serve stored private collection of <see
    ///     cref="IItem"/> objects.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IService{T}"/> -&gt; ICRUDService&lt;T&gt; <br/><b>
    ///         Implementations: </b><see cref="Common.BaseService{T}"/>, <see cref="Concrete.UserService"/>
    ///     </para>
    /// </summary>
    /// <typeparam name="T"> Type of handled entity. </typeparam>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="Common.BaseService{T}"> MdFilesMerger.App.Common.BaseService&lt;T&gt; </seealso>
    /// <seealso cref="Concrete.UserService"> MdFilesMerger.App.Concrete.UserService </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    public interface ICRUDService<T> : IService<T> where T : IItem
    {
        /// <summary>
        ///     Adds the item to the stored collection.
        /// </summary>
        /// <param name="item"> The item to add. </param>
        /// <returns>
        ///     Identification number of added item. It can be:
        ///     <list type="bullet">
        ///         <item>
        ///             <paramref name="item"/>.Id, if it is greater than <see langword="0"/> and
        ///             there was no equal element or element with the same id in the collection,
        ///         </item>
        ///         <item>
        ///             new identification number (one grater than max id value in the collection),
        ///             if there is no equal element in the collection, but there is already element
        ///             with this id or value of <paramref name="item"/>.Id is less equal <see langword="0"/>,
        ///         </item>
        ///         <item>
        ///             <see langword="-1"/>, if <paramref name="item"/> wasn't added to the
        ///             collection (for example <paramref name="item"/> was <see langword="null"/>
        ///             or there already is equal item in the collection).
        ///         </item>
        ///     </list>
        /// </returns>
        public int Create(T item);

        /// <summary>
        ///     Adds the range of items to the stored collection.
        /// </summary>
        /// <param name="items"> The list of items to add. </param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             Identification number of first item added to the stored collection.
        ///         </listheader>
        ///         <item>
        ///             Usually, it will be id of the first element of <paramref name="items"/>
        ///             after adding it to the stored collection.
        ///         </item>
        ///         <item>
        ///             If first element wasn't successfully added (for example it was <see
        ///             langword="null"/> or the collection already contains equal object), it will
        ///             be id of the first successfully added element.
        ///         </item>
        ///         <item>
        ///             Id value can change if element's original id is less equal <see
        ///             langword="0"/> or the stored collection already contains element with this
        ///             id value. In that case, new id value will be one grater than max id value in
        ///             the stored collection.
        ///         </item>
        ///         <item>
        ///             If no element was successfully added to the stored collection (for example
        ///             <paramref name="items"/> was empty or contained only <see langword="null"/>
        ///             values) method returns <see langword="-1"/>.
        ///         </item>
        ///     </list>
        /// </returns>
        public int CreateRange(List<T> items);

        /// <summary>
        ///     Deletes the specified item.
        /// </summary>
        /// <param name="item"> The item to delete. </param>
        /// <returns>
        ///     Identification number of removed item or <see langword="-1"/>, if item wasn't
        ///     removed, also if there is no element with id <paramref name="item"/>.Id in the
        ///     stored collection.
        /// </returns>
        public int Delete(T? item);

        /// <summary>
        ///     Deletes the item with the specified identification number.
        /// </summary>
        /// <param name="id"> The identification number of the item to removed. </param>
        /// <returns>
        ///     Identification number of removed item or <see langword="-1"/>, if item wasn't
        ///     removed, also if there is no element with identification number <paramref
        ///     name="id"/> in the stored collection.
        /// </returns>
        public int Delete(int id);

        /// <summary>
        ///     Gets element from collection that is equal to specified, according to appropriate
        ///     <see cref="Object.Equals(object?)"/> override.
        /// </summary>
        /// <param name="item"> The item, whose counterpart is searched for in the collection. </param>
        /// <returns>
        ///     <paramref name="item"/> counterpart from the collection, if found; otherwise <see langword="null"/>.
        /// </returns>
        public T? GetEqual(T? item);

        /// <summary>
        ///     Gets the identification number for new element.
        /// </summary>
        /// <returns>
        ///     Returns number one greater then max Id in the stored collection, or <see
        ///     langword="1"/>, if list is empty.
        /// </returns>
        public int GetNewId();

        /// <summary>
        ///     Determines whether the stored collection is empty.
        /// </summary>
        /// <returns> <see langword="true"/> if the list is empty; otherwise, <see langword="false"/>. </returns>
        public bool IsEmpty();

        /// <summary>
        ///     Gets the whole stored collection.
        /// </summary>
        /// <returns> The stored collection as <see cref="List{T}"/>. </returns>
        public List<T> ReadAll();

        /// <summary>
        ///     Updates the specified item.
        /// </summary>
        /// <remarks>
        ///     First method removes from the collection element with the same identification
        ///     number. If removal succeeded, calls <see cref="Create(T)"> Create( <paramref
        ///     name="item"/>) </see> method. If <paramref name="item"/> wasn't successfully added
        ///     to the collection, previous object is restored.
        /// </remarks>
        /// <param name="item"> The updated item. </param>
        /// <returns>
        ///     Identification number of updated item or <see langword="-1"/>, if there is no
        ///     element with id <paramref name="item"/>.Id in the stored collection, old value
        ///     wasn't successfully removed or there is already equal object in the collection.
        /// </returns>
        public int Update(T item);
    }
}