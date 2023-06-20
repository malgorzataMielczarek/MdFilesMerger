using MdFilesMerger.App.Abstract;
using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.Controller.Abstract
{
    /// <summary>
    ///     Interface with additional methods of manager for menu action model.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="IManager{T, TService}"/> -&gt; IMenuActionManager <br/><b>
    ///         Implementations: </b><see cref="Concrete.MenuActionManager"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="IMenuActionService"> MdFilesMerger.App.Abstract.IMenuActionService </seealso>
    /// <seealso cref="IManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="Concrete.MenuActionManager"> MdFilesMerger.Controller.Concrete.MenuActionManager </seealso>
    /// <seealso cref="IMenuAction"> MdFilesMerger.Domain.Abstract.IMenuAction </seealso>
    public interface IMenuActionManager : IManager<IMenuAction, IMenuActionService>
    {
        /// <summary>
        ///     Asks user whether he/she wants to go back to main menu or exit the program
        ///     completely. If user presses <c> Esc </c> "Exit" menu action is selected. If user
        ///     presses <c> Enter </c> (or anything else, other that <c> Esc </c>),
        ///     "DisplayMainMenu" menu action is selected.
        /// </summary>
        void EnterOrEsc();

        /// <summary>
        ///     Selects "DisplayMainMenu" menu action.
        /// </summary>
        void GoToMainMenu();

        /// <summary>
        ///     Selects menu action.
        /// </summary>
        /// <remarks>
        ///     Displays appropriate menu (elements of collection with <see cref="MenuAction.Menu"/>
        ///     value equal selected item's <see cref="MenuAction.NextMenu"/> value. Asks user to
        ///     select one element and sets it as selected item.
        /// </remarks>
        /// <returns>
        ///     Selected menu action or <see langword="null"/> if user failed to select existing action.
        /// </returns>
        IMenuAction? Select();
    }
}