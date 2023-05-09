namespace MdFilesMerger
{
    internal class MainMenuService
    {
        public string Title { get; }

        public MainMenuService()
        {
            Title = "Menu główne";

            mainMenu = new MainMenu();
        }

        public MenuActionService? SelectAction()
        {
            Console.Write("Podaj numer czynności z powyższego menu, którą chcesz wykonać lub wciśnij Esc, aby zakończyć działanie programu: ");

            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
            {
                return new MenuActionService(0, "Zamknij program");
            }
            else
            {
                string input = key.KeyChar.ToString() + Console.ReadLine();

                if (int.TryParse(input, out int result))
                {
                    return mainMenu.SelectActionById(result);
                }
                else
                {
                    return null;
                }
            }
        }
        public void DisplayMenu()
        {
            foreach (var action in mainMenu.Actions)
            {
                action.Display();
            }
            Console.WriteLine();
        }
        public void DisplayErrorMessage(bool isError)
        {
            if(isError)
            {
                Console.WriteLine("Wybrana czynność nie istnieje.");
            }
        }

        private readonly MainMenu mainMenu;

    }
}
