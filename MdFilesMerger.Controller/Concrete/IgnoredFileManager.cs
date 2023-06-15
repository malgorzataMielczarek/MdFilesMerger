﻿using MdFilesMerger.App.Concrete;
using MdFilesMerger.Controller.Common;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.Controller.Concrete
{
    /// <summary>
    ///     Manager for ignored file model.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseManager{T, TService}"/> -&gt; <see
    ///         cref="RelativeFileManager{T, TService}"/> -&gt; IgnoredFileManager <br/><b>
    ///         Implements: </b><see cref="Abstract.ICRUDManager{T, TService}"/>, <see
    ///                     cref="Abstract.IManager{T, TService}"/>, <see
    ///                     cref="Abstract.IRelativeFileManager{T, TService}"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="IgnoredFileService"> MdFilesMerger.App.Concrete.IgnoredFileService </seealso>
    /// <seealso cref="Abstract.ICRUDManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.ICRUDManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="Abstract.IManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="Abstract.IRelativeFileManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IRelativeFileManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="BaseManager{T, TService}">
    ///     MdFilesMerger.Controller.Common.BaseManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="RelativeFileManager{T, TService}">
    ///     MdFilesMerger.Controller.Common.RelativeFileManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IgnoredFile"> MdFilesMerger.Domain.Entity.IgnoredFile </seealso>
    public sealed class IgnoredFileManager : RelativeFileManager<IgnoredFile, IgnoredFileService>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="IgnoredFileManager"/> class and all
        ///     it's properties.
        /// </summary>
        /// <remarks>
        ///     Sets <see cref="BaseManager{T, TService}.SelectedItem"/> to <see langword="-1"/> and
        ///     <see cref="BaseManager{T, TService}.Service"/> to newly created <see
        ///     cref="IgnoredFileService"/> object connected with specified <see
        ///     cref="MainDirectoryService"/> instance.
        /// </remarks>
        /// <param name="mainDirectoryService">
        ///     The main directory service that newly created <see cref="IgnoredFileService"/>
        ///     object will by connected with.
        /// </param>
        public IgnoredFileManager(MainDirectoryService mainDirectoryService) : base(new IgnoredFileService(mainDirectoryService)) { }

        /// <inheritdoc/>
        public override void DisplayTitle()
        {
            DisplayTitle("Lista ignorowanych plików");
        }
    }
}