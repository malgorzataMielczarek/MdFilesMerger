using MdFilesMerger.App.Abstract;
using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.App.Common
{
    /// <summary>
    ///     Base CRUD service implementation.
    ///     <para>
    ///         <b> Inheritance: </b> BaseService&lt;T&gt; <br/><b>
    ///         Implements: <see cref="ICRUDService{T}"/>, </b><see cref="IService{T}"/>
    ///     </para>
    /// </summary>
    /// <typeparam name="T"> Type of handled entity. </typeparam>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    public class BaseService<T> : ICRUDService<T> where T : class, IItem
    {
        /// <summary>
        ///     The stored collection of <typeparamref name="T"/> objects, that will be managed by
        ///     this class.
        /// </summary>
        protected List<T> _items;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseService{T}"/> class and it's <see
        ///     cref="_items"/> field.
        /// </summary>
        public BaseService()
        {
            _items = new List<T>();
        }

        /// <inheritdoc/>
        public virtual int Create(T item)
        {
            if (_items.Contains(item))
            {
                return -1;
            }

            if (item.Id == 0 || ReadById(item.Id) != null)
            {
                item.Id = GetNewId();
            }

            _items.Add(item);

            return item.Id;
        }

        /// <inheritdoc/>
        public int CreateRange(List<T> items)
        {
            int result = -1;

            foreach (var item in items)
            {
                if (result == -1)
                {
                    result = Create(item);
                }
                else
                {
                    Create(item);
                }
            }

            return result;
        }

        /// <inheritdoc/>
        public int Delete(T? item)
        {
            // find id item has on the list
            int id = GetEqual(item)?.Id ?? -1;

            if (id != -1 && _items.Remove(item!))
            {
                // patch up id "holes" after item removal
                DecreseGreaterIds(id);

                return id;
            }

            return -1;
        }

        /// <inheritdoc/>
        public int Delete(int id)
        {
            T? item = ReadById(id);

            if (_items.Remove(item))
            {
                // patch up id "holes" after item removal
                DecreseGreaterIds(id);

                return id;
            }

            return -1;
        }

        /// <inheritdoc/>
        public T? GetEqual(T? item)
        {
            if (item != null)
            {
                foreach (var item2 in _items)
                {
                    if (item2.Equals(item))
                    {
                        return item2;
                    }
                }
            }

            return null;
        }

        /// <inheritdoc/>
        public int GetNewId()
        {
            int newId = 0;

            foreach (var item in _items)
            {
                if (item.Id > newId)
                {
                    newId = item.Id;
                }
            }

            newId++;

            return newId;
        }

        /// <inheritdoc/>
        public bool IsEmpty()
        {
            return _items.Count == 0;
        }

        /// <inheritdoc/>
        public List<T> ReadAll()
        {
            return _items;
        }

        /// <summary>
        ///     Gets stored <typeparamref name="T"/> object by it's identification number.
        /// </summary>
        /// <param name="id"> Identification number of searched object. </param>
        /// <returns>
        ///     Found <typeparamref name="T"/> object, or <see langword="null"/>, if no object was found.
        /// </returns>
        public T? ReadById(int id)
        {
            foreach (var item in _items)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }

        /// <inheritdoc/>
        public int Update(T item)
        {
            if (item == null)
            {
                return -1;
            }

            var oldItem = ReadById(item.Id);
            if (oldItem != null && _items.Remove(oldItem))
            {
                int newId = Create(item);

                // if item wasn't added, add old item
                if (newId == -1)
                {
                    Create(oldItem);
                }

                return newId;
            }

            return -1;
        }

        private void DecreseGreaterIds(int id)
        {
            foreach (var item in _items)
            {
                if (item.Id > id)
                {
                    item.Id--;
                }
            }
        }
    }
}