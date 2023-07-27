using MdFilesMerger.App.Abstract;
using MdFilesMerger.Controller.Abstract;
using MdFilesMerger.Controller.Common;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Abstract;
using System.Text;
using MdFilesMerger.App.Concrete;

namespace MdFilesMerger.Controller.Concrete
{
    /// <summary>
    ///     Manager for merged file model.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseManager{T, TService}"/> -&gt; MergedFileManager
    ///         <br/><b> Implements: </b><see cref="ICRUDManager{T, TService}"/>, <see
    ///         cref="IManager{T, TService}"/>, <see cref="IMergedFileManager"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="IMergedFileService"> MdFilesMerger.App.Abstract.IMergedFileService </seealso>
    /// <seealso cref="ICRUDManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.ICRUDManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IMergedFileManager"> MdFilesMerger.Controller.Abstract.IMergedFileManager </seealso>
    /// <seealso cref="BaseManager{T, TService}">
    ///     MdFilesMerger.Controller.Common.BaseManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IMergedFile"> MdFilesMerger.Domain.Abstract.IMergedFile </seealso>
    public sealed class MergedFileManager : BaseManager<IMergedFile, IMergedFileService>, IMergedFileManager
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MergedFileManager"/> class and all it's properties.
        /// </summary>
        /// <remarks>
        ///     Sets <see cref="BaseManager{T, TService}.Service"/> to the newly created instance of
        ///     <see cref="MergedFileService"/> class, <see cref="BaseManager{T,
        ///     TService}.SelectedItem"/> to the first object of service's collection and <see
        ///     cref="MainDirectoryManager"/> to newly created <see
        ///     cref="Concrete.MainDirectoryManager"/> object. Next sets parent directory of
        ///     selected item to path of currently selected main directory.
        /// </remarks>
        public MergedFileManager() : base(new MergedFileService())
        {
            SelectedItem = 1;
            MainDirectoryManager = new MainDirectoryManager();

            // set merged file parent directory to main directory
            string? path = MainDirectoryManager.Service.ReadById(MainDirectoryManager.SelectedItem)?.GetPath();
            if (!string.IsNullOrWhiteSpace(path))
            {
                Service.ReadById(SelectedItem)?.SetParentDirectory(path);
            }
        }

        /// <inheritdoc/>
        public IMainDirectoryManager MainDirectoryManager { get; }

        /// <inheritdoc/>
        /// <param name="userId">
        ///     The identifier of user that created item will be connected with.
        /// </param>
        // TODO: Implement creating new merged file object by getting appropriate data from user.
        public bool Create(int userId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool Delete()
        {
            return Delete(Service.ReadById(SelectedItem));
        }

        /// <inheritdoc/>
        /// <param name="userId">
        ///     The identifier of the user, that elements to delete are connected with.
        /// </param>
        public bool Delete(int userId)
        {
            foreach (var file in GetFilteredList(userId))
            {
                if (!Delete(file))
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc/>
        public override void DisplayTitle()
        {
            DisplayTitle("Połącz wybrane pliki");
        }

        /// <inheritdoc/>
        public void Merge()
        {
            FileInfo? file = Service.CreateFile(SelectedItem);

            if (file != null)
            {
                string mergingMsg = "Scalanie plików";
                Console.WriteLine(mergingMsg);

                List<ISelectedFile> selectedFiles = new List<ISelectedFile>();

                foreach (var dir in MainDirectoryManager.Service.ReadByMergedFileId(SelectedItem))
                {
                    MainDirectoryManager.IgnoreFile(file, dir);
                    selectedFiles.AddRange(MainDirectoryManager.SelectedFileManager.Service.ReadByMainDirId(dir.Id));
                }

                Service.AppendTableOfContents(file, SelectedItem, selectedFiles);

                string newLine = Service.ReadById(SelectedItem)!.NewLineStyle;
                for (int i = 0; i < selectedFiles.Count; i++)
                {
                    MainDirectoryManager.SelectedFileManager.Service.AppendFile(selectedFiles[i], file, newLine);

                    // show progress
                    Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                    int procent = (i + 1) * 100 / selectedFiles.Count;
                    StringBuilder progress = new StringBuilder(mergingMsg);
                    progress.Append(' ', (Console.WindowWidth - mergingMsg.Length - procent.ToString().Length - 1) / 2);
                    progress.Append(procent);
                    progress.Append('%');
                    Console.WriteLine(progress.ToString());
                    Thread.Sleep(1);
                }
            }
        }

        /// <inheritdoc/>
        public void UpdateFileName()
        {
            if (SelectedItem != -1)
            {
                var cursor = Console.GetCursorPosition();
                Console.Write("Podaj nazwę tworzonego pliku: ");
                while (true)
                {
                    string? name;
                    while (string.IsNullOrEmpty(name = Console.ReadLine()))
                    {
                        Console.SetCursorPosition(cursor.Left, cursor.Top);
                        Console.WriteLine("Nazwa pliku nie może być pusta.");
                        Console.Write("Podaj ponownie nazwę tworzonego pliku: ");
                    }

                    if (Service.UpdateFileName(SelectedItem, name) == SelectedItem)
                    {
                        break;
                    }
                    else
                    {
                        Console.SetCursorPosition(cursor.Left, cursor.Top);
                        Console.WriteLine("Podana nazwa zawiera niedozwolone znaki.");
                        Console.Write("Podaj ponownie nazwę tworzonego pliku: ");
                    }
                }
            }
        }

        /// <inheritdoc/>
        public void UpdateParentDirectory()
        {
            if (SelectedItem != -1)
            {
                var cursor = Console.GetCursorPosition();
                Console.Write("Podaj ścieżkę do katalogu, w którym chcesz zapisać plik: ");
                while (true)
                {
                    string? path;
                    while (string.IsNullOrEmpty(path = Console.ReadLine()))
                    {
                        Console.SetCursorPosition(cursor.Left, cursor.Top);
                        Console.WriteLine("Ścieżka nie może być pusta.");
                        Console.Write("Podaj ponownie ścieżkę do katalogu, w którym chcesz zapisać plik: ");
                    }

                    if (Service.UpdateParentDirectory(SelectedItem, path) == SelectedItem)
                    {
                        break;
                    }
                    else
                    {
                        Console.SetCursorPosition(cursor.Left, cursor.Top);
                        Console.WriteLine("Podana ścieżka jest nieprawidłowa.");
                        Console.Write("Podaj ponownie ścieżkę do katalogu, w którym chcesz zapisać plik: ");
                    }
                }
            }
        }

        /// <inheritdoc/>
        public void UpdateTableOfContents(TableOfContents newTableOfContents)
        {
            if (SelectedItem == -1)
            {
                return;
            }

            Service.UpdateTableOfContents(SelectedItem, newTableOfContents);

            IMergedFile? file = Service.ReadById(SelectedItem);
            if (file != null)
            {
                DisplayTitle("Podgląd wybranego spisu treści");
                if (file.TableOfContents == TableOfContents.None)
                {
                    Console.WriteLine("Brak");
                }
                else
                {
                    List<ISelectedFile> selectedFiles = new List<ISelectedFile>();
                    foreach (var mainDir in MainDirectoryManager.Service.ReadByMergedFileId(file.Id))
                    {
                        selectedFiles.AddRange(MainDirectoryManager.SelectedFileManager.Service.ReadByMainDirId(mainDir.Id));
                    }

                    Console.WriteLine(TableOfContentsService.CreateTOC(file, selectedFiles));
                }
                Console.WriteLine();
            }
        }

        /// <inheritdoc/>
        public void UpdateTitle()
        {
            if (SelectedItem != -1)
            {
                Console.Write("Podaj nagłówek (tytuł) tworzonego pliku: ");
                Service.UpdateTitle(SelectedItem, Console.ReadLine());
            }
        }

        /// <inheritdoc/>
        protected override void DisplayItem(IMergedFile? item)
        {
            if (item != null)
            {
                DisplaySetting("Nazwa tworzonego pliku", item.GetFileName());
                DisplaySetting("Położenie pliku", item.GetParentDirectory());
                DisplaySetting("Tytuł", item.Title);
            }
        }

        /// <inheritdoc/>
        /// <param name="userId">
        ///     The identifier of user, that retrieved elements are connected with.
        /// </param>
        protected override List<IMergedFile> GetFilteredList(int userId) => Service.ReadByUserId(userId);

        private bool Delete(IMergedFile? file)
        {
            if (file == null)
            {
                return false;
            }

            int id = file.Id;
            return MainDirectoryManager.Delete(id) && Service.Delete(file) == id;
        }

        private void DisplaySetting(string description, string? value)
        {
            Console.WriteLine($"{description}: {value}");
        }
    }
}