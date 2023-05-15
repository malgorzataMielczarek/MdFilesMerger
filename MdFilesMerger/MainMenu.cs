namespace MdFilesMerger
{
    internal class MainMenu
    {
        public MenuActionService[] Actions { get; }

        public MainMenu()
        {
            Actions = new MenuActionService[4];

            string menu = this.GetType().Name;
            Actions[0] = new MenuActionService(1, "Zmień katalog główny", menu);
            Actions[1] = new MenuActionService(2, "Wyświetl listę plików do scalenia", menu);
            Actions[2] = new MenuActionService(3, "Utwórz spis treści", menu);
            Actions[3] = new MenuActionService(4, "Scal pliki", menu);
        }

        public MenuActionService? SelectActionById(int id)
        {
            foreach (var action in Actions)
            {
                if (action.MenuAction.Id == id)
                {
                    return action;
                }
            }

            return null;
        }
    }
}
