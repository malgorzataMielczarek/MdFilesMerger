using System;

namespace MdFilesMerger
{
    internal class MainMenu
    {

        public MenuActionService[] Actions { get; }


        public MainMenu()
        {
            Actions = new MenuActionService[4];

            Actions[0] = new MenuActionService(1, "Zmień katalog główny");
            Actions[1] = new MenuActionService(2, "Wyświetl listę plików do scalenia");
            Actions[2] = new MenuActionService(3, "Utwórz spis treści");
            Actions[3] = new MenuActionService(4, "Scal pliki");
        }

        public MenuActionService? SelectActionById(int id)
        {
            foreach (var action in Actions)
            {
                if (action.Equals(id))
                {
                    return action;
                }
            }

            return null;
        }
    }
}
