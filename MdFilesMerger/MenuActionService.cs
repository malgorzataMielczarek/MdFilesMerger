namespace MdFilesMerger
{
    internal class MenuActionService
    {
        public int Id => menuAction.Id;
        public MenuActionService(int id, string description)
        {
            menuAction = new MenuAction(id, description);
        }

        public void Display()
        {
            Console.WriteLine($"{menuAction.Id}. {menuAction.Description}");
        }

        public bool Equals(int id)
        {
            return menuAction.Id == id;
        }

        public bool Equals(string description)
        {
            return description == menuAction.Description;
        }

        private readonly MenuAction menuAction;
    }
}
