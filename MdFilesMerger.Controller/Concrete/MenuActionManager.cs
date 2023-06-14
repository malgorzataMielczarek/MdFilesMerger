using MdFilesMerger.App.Concrete;
using MdFilesMerger.Controller.Common;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.Controller.Concrete
{
    public class MenuActionManager : BaseManager<MenuAction, MenuActionService>
    {
        public MenuActionManager() : base(new MenuActionService())
        {
            SelectedItem = 2;
        }

        public override void DisplayTitle()
        {
            MenuType menuType = Service.ReadById(SelectedItem)?.NextMenu ?? MenuType.Main;
            string title = menuType switch
            {
                MenuType.Main => "Menu główne",
                MenuType.TableOfContents => "Utwórz spis treści dla tworzonego pliku .md",
                MenuType.MergedFile => "Połącz wybrane pliki",
                _ => string.Empty,
            };

            DisplayTitle(title);
        }

        public void EnterOrEsc()
        {
            Console.Write("Wciśnij Enter aby wrócić do menu głównego lub Esc by zakończyć program.");

            var key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
            {
                SelectedItem = 1;
            }
            else
            {
                SelectedItem = 2;
            }
        }

        public void GoToMainMenu()
        {
            SelectedItem = 2;
        }

        public MenuAction? Select()
        {
            Select(0);

            return Service.ReadById(SelectedItem);
        }

        /// <inheritdoc/>

        protected override List<MenuAction> GetFilteredList(int connectedItemId = 0)
        {
            MenuType menuType = Service.ReadById(SelectedItem)?.NextMenu ?? MenuType.Main;

            return Service.ReadByMenuType(menuType);
        }

        protected override void SelectItem(IReadOnlyList<MenuAction> list)
        {
            if (list.Count > 0)
            {
                var cursor = Console.GetCursorPosition();
                while (true)
                {
                    Console.Write("Podaj numer czynności, którą chcesz wykonać, wciśnij Esc aby zakończyć działanie programu");

                    if (list[0].Menu != MenuType.Main)
                    {
                        Console.Write(" lub Enter, aby wrócić do menu głównego");
                    }

                    Console.Write(": ");

                    var key = Console.ReadKey();

                    SelectedItem = key.Key switch
                    {
                        ConsoleKey.Escape => 1,
                        ConsoleKey.Enter => 2,
                        >= ConsoleKey.D1 and <= ConsoleKey.D9 => GetIdByIndex(key.Key - ConsoleKey.D1, list),
                        _ => -1
                    };

                    if (SelectedItem == -1)
                    {
                        Console.SetCursorPosition(cursor.Left, cursor.Top);
                        Console.WriteLine("Nie rozpoznano wybranej czynności. Wybierz ponownie co chcesz zrobić.");
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}