using MdFilesMerger.App.Abstract;
using MdFilesMerger.Controller.Abstract;
using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.Controller.Common
{
    /// <summary>
    ///     Class with base implementation of ICRUDManager interface for relative file models
    ///     (selected and ignored file models).
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseManager{T, TService}"/> -&gt;
    ///         RelativeFileManager&lt;T, TService&gt; <br/><b> Implements: </b><see
    ///         cref="ICRUDManager{T, TService}"/>, <see cref="IManager{T, TService}"/>
    ///     </para>
    /// </summary>
    /// <typeparam name="T"> Type of relative file model. </typeparam>
    /// <typeparam name="TService">
    ///     Service handling collection of object of specified model.
    /// </typeparam>
    /// <seealso cref="IRelativeFileService{T}"> MdFilesMerger.App.Abstract.IRelativeFileService&lt;T&gt; </seealso>
    /// <seealso cref="ICRUDManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.ICRUDManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="BaseManager{T, TService}">
    ///     MdFilesMerger.Controller.Common.BaseManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IRelativeFile"> MdFilesMerger.Domain.Abstract.IRelativeFile </seealso>
    public abstract class RelativeFileManager<T, TService> : BaseManager<T, TService>, IRelativeFileManager<T, TService> where T : IRelativeFile where TService : IRelativeFileService<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RelativeFileManager{T, TService}"/>
        ///     class and all it's properties.
        /// </summary>
        /// <param name="service"> The instance of service associated with this class. </param>
        /// <remarks>
        ///     <see cref="BaseManager{T, TService}.SelectedItem"/> is set to <see langword="-1"/>
        ///     and <see cref="BaseManager{T, TService}.Service"/> to specified value.
        /// </remarks>
        public RelativeFileManager(TService service) : base(service) { }

        /// <inheritdoc/>
        /// <param name="mainDirectoryId">
        ///     The identifier of main directory object, that created object will by connected to.
        /// </param>
        public bool Create(int mainDirectoryId)
        {
            if (mainDirectoryId == -1)
            {
                return false;
            }

            if (Activator.CreateInstance(typeof(T), 0, mainDirectoryId) is not T file)
            {
                return false;
            }

            int id = Service.Create(file);
            if (id == -1)
            {
                return false;
            }

            var cursor = Console.GetCursorPosition();

            while (true)
            {
                Console.WriteLine("Podaj ścieżkę do pliku (absolutną lub względną do katalogu głównego): ");
                string? path = Console.ReadLine();

                string errorMsg;

                if (!string.IsNullOrWhiteSpace(path))
                {
                    if (Service.UpdatePath(id, path) == id)
                    {
                        SelectedItem = id;
                        return true;
                    }
                    else
                    {
                        errorMsg = "Plik o podanej ścieżce nie istnieje.";
                    }
                }
                else
                {
                    errorMsg = "Ścieżka do pliku nie może być pusta.";
                }

                Console.SetCursorPosition(cursor.Left, cursor.Top);
                Console.WriteLine(errorMsg);
            }
        }

        /// <inheritdoc/>
        public bool Delete()
        {
            if (SelectedItem != -1 && Service.Delete(SelectedItem) == SelectedItem)
            {
                SelectedItem = -1;
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        /// <param name="mainDirectoryId">
        ///     The identifier of main directory object, that object to delete are connected to.
        /// </param>
        public bool Delete(int mainDirectoryId)
        {
            foreach (var file in GetFilteredList(mainDirectoryId))
            {
                int id = file.Id;
                if (Service.Delete(file) != id)
                {
                    return false;
                }
            }

            SelectedItem = -1;
            return true;
        }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <remarks>
        ///     <inheritdoc/> Selection is possible only between elements of the collection that are
        ///     connected with the specified main directory.
        /// </remarks>
        /// <param name="mainDirectoryId">
        ///     The identifier of the main directory object, that elements to select are connected with.
        /// </param>
        public override void Select(int mainDirectoryId)
        {
            DisplayItems(GetFilteredList(mainDirectoryId));
        }

        /// <inheritdoc/>
        public void SelectItem(int id)
        {
            SelectedItem = id;
        }

        /// <inheritdoc/>
        protected override void DisplayItem(T? item)
        {
            if (item != null)
            {
                Console.WriteLine(item.GetPath());
            }
        }

        /// <inheritdoc/>
        /// <param name="mainDirectoryId">
        ///     The identifier of the main directory object, that elements are filtered by.
        /// </param>
        protected override List<T> GetFilteredList(int mainDirectoryId) => Service.ReadByMainDirId(mainDirectoryId);
    }
}