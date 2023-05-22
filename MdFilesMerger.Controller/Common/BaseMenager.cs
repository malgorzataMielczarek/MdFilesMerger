using MdFilesMerger.App.Concrete;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.Controller.Common
{
    public abstract class BaseMenager<T> : IMenager<T> where T : BaseItem
    {
        protected MenuActionService _actionService;
        protected MainDirectory? _mainDir;
        protected string? _title;

        public BaseMenager(MenuActionService actionService)
        {
            _actionService = actionService;
        }

        public abstract T? GetChangedValue();

        public MenuAction PerformAction()
        {
            MenuAction? action;
            bool isReselect = false;

            do
            {
                DisplayView(isReselect);
                action = SelectOption();
                isReselect = true;
            }
            while (action == null);

            return action;
        }

        protected abstract void DisplayContent();

        protected abstract void DisplayErrorMsg();

        protected void DisplayMainDirPath()
        {
            if (_mainDir is not null)
            {
                Console.WriteLine($"Katalog główny: {_mainDir.GetFullPath()}");
            }
        }

        protected void DisplayMenu(IReadOnlyList<MenuAction> menuActions)
        {
            foreach (var action in menuActions)
            {
                if(action.Id > 0)
                {
                    Console.WriteLine($"{action.Id}. {action.Name}");
                }
            }
        }

        protected void DisplayTitle()
        {
            if (string.IsNullOrWhiteSpace(_title))
            {
                return;
            }

            Console.WriteLine();

            int windowWidth = Console.WindowWidth;
            int numberOfHyphens = (windowWidth - _title.Length) / 2;
            string hyphens = new string('-', numberOfHyphens);

            Console.Write(hyphens + _title.ToUpper() + hyphens);

            if (windowWidth > numberOfHyphens * 2 + _title.Length)
            {
                Console.Write("-");
            }

            Console.WriteLine("\n");
        }

        protected void DisplayView(bool displayError)
        {
            Console.Clear();
            DisplayMainDirPath();
            DisplayTitle();
            DisplayContent();
            if (displayError)
            {
                DisplayErrorMsg();
            }
        }

        protected abstract MenuAction? SelectOption();
    }
}
