using System.Text;

namespace MdFilesMerger
{
    internal class TableOfContents
    {
        private const string TABLE_OF_CONTENTS_HEADER = "## Spis treści" + Program.NEW_LINE;
        public enum Types
        {
            Text = 1,
            Hyperlink = 2,
            None = 3
        }
        public string Title { get; }

        public string? CreateTableOfContents(ListOfMdFiles listOfFiles)
        {
            switch (Type)
            {
                case Types.Text: return CreateTextTableOfContents(listOfFiles);
                case Types.Hyperlink: return CreateHyperlinksTableOfContents(listOfFiles);
                default: return null;
            }
        }

        public Types Type { get; set; }

        public TableOfContents()
        {
            Title = "Utwórz spis treści dla tworzonego pliku .md";
            
            actions = new MenuActionService[3];

            string menu = this.GetType().Name;
            actions[0] = new MenuActionService(1, "Spis treści będący zwykłym tekstem", menu);
            actions[1] = new MenuActionService(2, "Spis treści złożony z hiperlinków do odpowiednich paragrafów", menu);
            actions[2] = new MenuActionService(3, "Bez spisu treści", menu);

            Type = Types.None;
        }
        public void DisplayMenu()
        {
            foreach(var action in actions)
            {
                action.Display();
            }
            Console.WriteLine();
        }
        public void DisplayError(bool isError)
        {
            if(isError)
            {
                Console.WriteLine("Nie rozpoznano wybranego typu.");
            }
        }
        public MenuActionService? SelectAction()
        {
            Console.Write("Podaj numer typu wybranego z powyższego menu: ");

            _ = int.TryParse(Console.ReadLine(), out int type);

            switch(type)
            {
                case 1:
                    Type = Types.Text;
                    break;
                case 2:
                    Type = Types.Hyperlink;
                    break;
                case 3:
                    Type = Types.None;
                    break;
                default:
                    return null;
            }
            return actions[type - 1];
        }

        public string CreateHyperlinksTableOfContents(ListOfMdFiles listOfFiles)
        {
            //Count files with the same title
            List<string> titlesOfFiles = new List<string>();
            Dictionary<string, int> titles = new Dictionary<string, int>();
            foreach(var file in listOfFiles)
            {
                string title = GetFileTitle(file);
                titlesOfFiles.Add(title);
                if(titles.ContainsKey(title))
                {
                    titles[title]++;
                }
                else
                {
                    titles.Add(title, 1);
                }
            }

            List<string> appendedDirectories = new List<string>();

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(TABLE_OF_CONTENTS_HEADER);
            for (int i = 0; i < listOfFiles.Count; i++)
            {
                MdFile file = listOfFiles[i];
                string title = titlesOfFiles[i];

                int dirNumber = AppendDirectoriesEntries(appendedDirectories, stringBuilder, file);

                string hyperlink = Helpers.ConvertTextToSectionHyperlink(title);

                int qtt = titles[title]++;
                hyperlink = hyperlink.Insert(hyperlink.Length - 1, "-" + qtt.ToString());
                stringBuilder.Append('#', dirNumber + 3);
                stringBuilder.Append(" ");
                stringBuilder.Append(hyperlink);
                stringBuilder.Append(Program.NEW_LINE);
            }
            return stringBuilder.ToString();
        }

        private static int AppendDirectoriesEntries(List<string> appendedDirectories, StringBuilder stringBuilder, MdFile file)
        {
            string directories = "";
            int dirNumber = file.SubDirectories.Length;
            //Don't add the name of the most nested directory if it is the only .md file in that directory
            if (dirNumber > 0 && file.FileInfo.Directory != null && file.FileInfo.Directory.GetFiles("*.md").Length == 1)
            {
                dirNumber--;
            }
            for (int i = 0; i < dirNumber; i++)
            {
                string dir = file.SubDirectories[i];
                directories += "\\" + dir;
                if (!appendedDirectories.Contains(directories))
                {
                    appendedDirectories.Add(directories);

                    stringBuilder.Append('#', i + 3);
                    stringBuilder.Append(" ");
                    stringBuilder.Append(dir);
                    stringBuilder.Append(Program.NEW_LINE);
                }
            }

            return dirNumber;
        }

        private static string GetFileTitle(MdFile file)
        {
            string title = file.GetFileHeader();
            if (title != null)
            {
                while (title.StartsWith('#'))
                {
                    title = title[1..];
                }
                title = title.Trim();
            }
            else return string.Empty;

            return title;
        }

        public string CreateTextTableOfContents(ListOfMdFiles listOfFiles)
        {
            List<string> appendedDirectories = new List<string>();

            StringBuilder stringBuilder = new StringBuilder();
            
            stringBuilder.Append(TABLE_OF_CONTENTS_HEADER);

            foreach (MdFile file in listOfFiles)
            {
                string title = GetFileTitle(file);

                int dirNumber = AppendDirectoriesEntries(appendedDirectories, stringBuilder, file);

                stringBuilder.Append('#', dirNumber + 3);
                stringBuilder.Append(" ");
                stringBuilder.Append(title);
                stringBuilder.Append(Program.NEW_LINE);
            }
            return stringBuilder.ToString();
        }

        private MenuActionService[] actions;
    }
}
