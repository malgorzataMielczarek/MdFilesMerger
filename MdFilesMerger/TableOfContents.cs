using System.Text;

namespace MdFilesMerger
{
    internal class TableOfContents
    {
        public enum Types
        {
            None = 0,
            Text = 1,
            Hyperlink = 2
        }
        public readonly ListOfMdFiles ListOfFiles;

        public string GetText()
        {
            switch (Type)
            {
                case Types.None: return string.Empty;
                case Types.Text: return CreateTextTableOfContents();
                case Types.Hyperlink: return CreateHyperlinksTableOfContents();
                default: return string.Empty;
            }
        }

        public Types Type { get; set; }

        public TableOfContents(ListOfMdFiles list)
        {
            this.ListOfFiles = list;
            Type = Types.None;
        }
        public void DisplayMenu()
        {
            string? mainDirPath = null;
            if (ListOfFiles != null && ListOfFiles.Count > 0)
                mainDirPath = ListOfFiles.First().GetMainDirectoryPath();
            int numberOfTypes = 2;
            bool isFirst = true;
            int type;
            do
            {
                Program.ChangeView(mainDirPath);
                Program.DisplayTitle("Utwórz spis treści dla tworzonego pliku .md");
                Console.WriteLine("Wybierz rodzaj spisu treści jaki chcesz utworzyć");
                Console.WriteLine("1. Spis treści będący zwykłym tekstem");
                Console.WriteLine("2. Spis treści złożony z hiperlinków do odpowiednich paragrafów");
                Console.WriteLine();
                if (isFirst) isFirst = false;
                else Console.Write("Nie rozumiem co chcesz zrobić. ");
                Console.Write("Podaj numer typu wybranego z powyższego menu: ");
                _ = int.TryParse(Console.ReadLine(), out type);
                if (type < 0 || type > numberOfTypes) type = 0;
            }
            while (type == 0);
            switch (type)
            {
                case 1:
                    Type = Types.Text; 
                    break;
                case 2:
                    Type = Types.Hyperlink; 
                    break;
            }
            Program.ChangeView(mainDirPath);
            Console.WriteLine(GetText());
            Console.WriteLine();
        }

        public string CreateHyperlinksTableOfContents()
        {

            List<string> appendedDirectories = new List<string>();
            Dictionary<string, int> links = new Dictionary<string, int>();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("## Spis treści");
            foreach (MdFile file in ListOfFiles)
            {
                string title = GetFileTitle(file);
                int dirNumber = AppendDirectoriesEntries(appendedDirectories, stringBuilder, file);
                stringBuilder.Append(new String('#', dirNumber + 3));
                stringBuilder.Append(' ');
                string hyperlink = Helpers.ConvertTextToHyperlink(title);
                string link = Helpers.GetLinkPartFromLinkBlock(hyperlink);
                int qtt;
                if (links.TryGetValue(link, out qtt))
                {
                    links[link] = ++qtt;
                }
                else
                {
                    qtt = 1;
                    links.Add(link, qtt);
                }
                hyperlink = hyperlink.Insert(hyperlink.Length - 1, "-" + qtt.ToString());
                stringBuilder.AppendLine(hyperlink);
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
                    for (int j = 0; j < i + 3; j++)
                        stringBuilder.Append('#');
                    stringBuilder.AppendLine(" " + dir);
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
                    title = title.Substring(1);
                }
                title = title.Trim();
            }

            return title;
        }

        public string CreateTextTableOfContents()
        {
            List<string> appendedDirectories = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("## Spis treści");
            foreach (MdFile file in ListOfFiles)
            {
                string title = GetFileTitle(file);
                int dirNumber = AppendDirectoriesEntries(appendedDirectories, stringBuilder, file);
                stringBuilder.Append(new String('#', dirNumber + 3));
                stringBuilder.Append(' ');
                stringBuilder.AppendLine(title);
            }
            return stringBuilder.ToString();
        }
    }
}
