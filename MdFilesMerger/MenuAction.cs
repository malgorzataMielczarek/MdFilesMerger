namespace MdFilesMerger
{
    internal class MenuAction
    {
        public int Id { get; }
        public string Description { get; }

        public MenuAction(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
