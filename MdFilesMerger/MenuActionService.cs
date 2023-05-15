namespace MdFilesMerger
{
    internal class MenuActionService
    {
        public MenuAction MenuAction { get; }

        public MenuActionService(int id, string description, string menu)
        {
            MenuAction = new MenuAction(id, description, menu);
        }

        public void Display()
        {
            Console.WriteLine($"{MenuAction.Id}. {MenuAction.Description}");
        }
    }
}
