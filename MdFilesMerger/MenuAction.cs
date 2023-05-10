namespace MdFilesMerger
{
    internal class MenuAction
    {
        public int Id { get; }
        public string Description { get; }
        public string Menu { get; }

        public MenuAction(int id, string description, string menu)
        {
            Id = id;
            Description = description;
            Menu = menu;
        }
    }
}
