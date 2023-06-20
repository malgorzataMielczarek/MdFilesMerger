using MdFilesMerger.App.Abstract;
using MdFilesMerger.App.Concrete;
using MdFilesMerger.Controller.Abstract;
using MdFilesMerger.Controller.Common;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.Controller.Concrete
{
    /// <summary>
    ///     Manager for main directory model.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseManager{T, TService}"/> -&gt;
    ///         MainDirectoryManager <br/><b> Implements: </b><see cref="ICRUDManager{T,
    ///         TService}"/>, <see cref="IMainDirectoryManager"/>, <see cref="IManager{T, TService}"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="IMainDirectoryService"> MdFilesMerger.App.Abstract.IMainDirectoryService </seealso>
    /// <seealso cref="ICRUDManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.ICRUDManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IMainDirectoryManager"> MdFilesMerger.Controller.Abstract.IMainDirectoryManager </seealso>
    /// <seealso cref="IManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="BaseManager{T, TService}">
    ///     MdFilesMerger.Controller.Common.BaseManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IMainDirectory"> MdFilesMerger.Domain.Abstract.IMainDirectory </seealso>
    public sealed class MainDirectoryManager : BaseManager<IMainDirectory, IMainDirectoryService>, IMainDirectoryManager
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MainDirectoryManager"/> class and all
        ///     it's properties.
        /// </summary>
        /// <remarks>
        ///     Sets <see cref="BaseManager{T, TService}.Service"/> to newly created <see
        ///     cref="MainDirectoryService"/> object. If services collection is empty, it asks user
        ///     for an input to create <see cref="MainDirectory"/> object. Next it sets <see
        ///     cref="BaseManager{T, TService}.SelectedItem"/> to the identifier of the first object
        ///     of services collection. At last it sets <see cref="SelectedFileManager"/> and <see
        ///     cref="IgnoredFileManager"/> properties by creating appropriate new objects connected
        ///     with this managers service.
        /// </remarks>
        public MainDirectoryManager() : base(new MainDirectoryService())
        {
            Initialize();
            SelectedFileManager = new SelectedFileManager(Service);
            IgnoredFileManager = new IgnoredFileManager(Service);
        }

        /// <inheritdoc/>
        public IIgnoredFileManager IgnoredFileManager { get; }

        /// <inheritdoc/>
        public ISelectedFileManager SelectedFileManager { get; }

        /// <inheritdoc/>
        /// <param name="mergedFileId">
        ///     The identifier of merged file that created object will be connected with.
        /// </param>
        public bool Create(int mergedFileId)
        {
            var cursor = Console.GetCursorPosition();
            SelectedItem = -1;
            MainDirectory dir = new MainDirectory() { MergedFileId = mergedFileId };

            while (true)
            {
                Console.Write("Wprowadź ścieżkę dostępu do katalogu: ");
                string? path = Console.ReadLine();

                if (dir.SetPath(path))
                {
                    SelectedItem = Service.Create(dir);
                }
                else
                {
                    SelectedItem = -1;
                }

                if (SelectedItem == -1)
                {
                    Console.SetCursorPosition(cursor.Left, cursor.Top);
                    Console.WriteLine("Nie można znaleźć katalogu o podanej ścieżce. Upewnij się, że istnieje i ponownie wprowadź ścieżkę.");
                }
                else
                {
                    SelectedFileManager.Service.CreateRange(Service.FindAllFiles(SelectedItem), Service.ReadById(SelectedItem)!);
                    break;
                }
            }

            return SelectedItem != -1;
        }

        /// <inheritdoc/>
        public bool Delete()
        {
            if (SelectedItem != -1 && Delete(Service.ReadById(SelectedItem)))
            {
                SelectedItem = -1;
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        /// <param name="mergedFileId">
        ///     The identifier of merged file that elements to delete are connected with.
        /// </param>
        public bool Delete(int mergedFileId)
        {
            foreach (var item in GetFilteredList(mergedFileId))
            {
                if (!Delete(item))
                {
                    return false;
                }
            }

            SelectedItem = -1;
            return true;
        }

        /// <inheritdoc/>
        public override void DisplayTitle()
        {
            DisplayTitle("Ustaw katalog, w którym chcesz wyszukiwać pliki .md");
        }

        /// <inheritdoc/>
        public void IgnoreFile(FileInfo file, IMainDirectory? mainDirectory)
        {
            if (mainDirectory == null || string.IsNullOrWhiteSpace(file?.FullName))
            {
                return;
            }

            ISelectedFile? selectedFile = new SelectedFile(file, mainDirectory);
            if ((selectedFile = SelectedFileManager.Service.GetEqual(selectedFile)) != null)
            {
                SelectedFileManager.SelectItem(selectedFile.Id);
                if (SelectedFileManager.Delete())

                {
                    int id = IgnoredFileManager.Service.Create(selectedFile.ToIgnoredFile());
                    IgnoredFileManager.SelectItem(id);
                }
            }
        }

        /// <inheritdoc/>
        public void IgnoreFile(FileInfo file)
        {
            IMainDirectory? mainDirectory = Service.ReadById(SelectedItem);
            IgnoreFile(file, mainDirectory);
        }

        /// <inheritdoc/>
        public bool UpdatePath()
        {
            if (SelectedItem == -1)
            {
                return false;
            }

            var cursor = Console.GetCursorPosition();
            while (true)
            {
                Console.Write("Nowa ścieżka dostępu do katalogu: ");
                string? path = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(path) && Service.UpdatePath(SelectedItem, path) == SelectedItem)
                {
                    return true;
                }
                else
                {
                    Console.SetCursorPosition(cursor.Left, cursor.Top);
                    Console.WriteLine("Nie można znaleźć katalogu o podanej ścieżce. Upewnij się, że istnieje i ponownie wprowadź ścieżkę.");
                }
            }
        }

        /// <inheritdoc/>
        protected override void DisplayItem(IMainDirectory? item)
        {
            if (item != null)
            {
                Console.WriteLine(item.GetPath());
            }
        }

        /// <inheritdoc/>
        /// <param name="mergedFileId">
        ///     The identifier of merged file object, that elements are filtered by.
        /// </param>
        protected override List<IMainDirectory> GetFilteredList(int mergedFileId) => Service.ReadByMergedFileId(mergedFileId);

        private bool Delete(IMainDirectory? mainDirectory)
        {
            if (mainDirectory == null)
            {
                return false;
            }

            int mainId = mainDirectory.Id;

            return SelectedFileManager.Delete(mainId) && IgnoredFileManager.Delete(mainId) && Service.Delete(mainDirectory) == mainId;
        }

        private void Initialize()
        {
            if (Service.IsEmpty())
            {
                DisplayTitle();
                Create(1);
                Console.Clear();
            }
            else
            {
                SelectedItem = 1;
            }
        }
    }
}