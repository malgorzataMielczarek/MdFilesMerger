namespace MdFilesMerger
{
    internal class MainMenuService
    {
        public string Title { get; }

        public MainMenu MainMenu { get; }

        public MainMenuService()
        {
            Title = "Menu główne";
            MainMenu = new MainMenu();
        }

        public void DisplayErrorMessage(bool isError)
        {
            if (isError)
            {
                Console.WriteLine("Wybrana czynność nie istnieje.");
            }
        }

        public void DisplayMenu()
        {
            foreach (var action in MainMenu.Actions)
            {
                action.Display();
            }

            Console.WriteLine();
        }

        public MenuActionService? SelectAction()
        {
            Console.Write("Podaj numer czynności z powyższego menu, którą chcesz wykonać lub wciśnij Esc, aby zakończyć działanie programu: ");

            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
            {
                return new MenuActionService(0, "Exit", nameof(MdFilesMerger.MainMenu));
            }

            else
            {
                string input = key.KeyChar.ToString() + Console.ReadLine();

                if (int.TryParse(input, out int result))
                {
                    return MainMenu.SelectActionById(result);
                }

                else
                {
                    return null;
                }
            }
        }
    }
}
