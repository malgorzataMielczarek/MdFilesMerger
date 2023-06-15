using MdFilesMerger.App.Concrete;
using MdFilesMerger.Controller.Abstract;
using MdFilesMerger.Controller.Common;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.Controller.Concrete
{
    /// <summary>
    ///     Manager for selected file model.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseManager{T, TService}"/> -&gt; <see
    ///         cref="RelativeFileManager{T, TService}"/> -&gt; SelectedFileManager <br/><b>
    ///         Implements: </b><see cref="ICRUDManager{T, TService}"/>, <see cref="IManager{T,
    ///         TService}"/>, <see cref="IRelativeFileManager{T, TService}"/>, <see cref="ISelectedFileManager"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="SelectedFileService"> MdFilesMerger.App.Concrete.SelectedFileService </seealso>
    /// <seealso cref="ICRUDManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.ICRUDManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IRelativeFileManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IReleativeFileManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="ISelectedFileManager"> MdFilesMerger.Controller.Abstract.ISelectedFileManager </seealso>
    /// <seealso cref="BaseManager{T, TService}">
    ///     MdFilesMerger.Controller.Common.BaseManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="RelativeFileManager{T, TService}">
    ///     MdFilesMerger.Controller.Common.RelativeFileManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="SelectedFile"> MdFilesMerger.Domain.Entity.SelectedFile </seealso>
    public class SelectedFileManager : RelativeFileManager<SelectedFile, SelectedFileService>, ISelectedFileManager
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SelectedFileManager"/> class and all
        ///     it's properties.
        /// </summary>
        /// <remarks>
        ///     Sets <see cref="BaseManager{T, TService}.SelectedItem"/> to <see langword="-1"/> and
        ///     <see cref="BaseManager{T, TService}.Service"/> to newly created <see
        ///     cref="SelectedFileService"/> object connected with specified <see
        ///     cref="MainDirectoryService"/> instance.
        /// </remarks>
        /// <param name="mainDirectoryService">
        ///     The main directory service that the newly created <see cref="SelectedFileService"/>
        ///     object will by connected with.
        /// </param>
        public SelectedFileManager(MainDirectoryService mainDirectoryService) : base(new SelectedFileService(mainDirectoryService))
        {
        }

        /// <inheritdoc/>
        public override void DisplayTitle()
        {
            DisplayTitle("Lista plików do scalenia");
        }

        /// <inheritdoc/>
        public bool UpdateTitle()
        {
            if (SelectedItem != -1)
            {
                Console.Write("Podaj tytuł pliku, który chcesz użyć jako odnośnik do niego w spisie treści: ");
                return Service.UpdateTitle(SelectedItem, Console.ReadLine()) == SelectedItem;
            }

            return false;
        }
    }
}