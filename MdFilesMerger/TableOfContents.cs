using System.Text;

namespace MdFilesMerger
{
    internal class TableOfContents
    {
        public readonly List<MdFile> ListOfFiles;
        public string? Text { get; private set; }

        public TableOfContents(List<MdFile> list)
        {
            this.ListOfFiles = list;
        }
        public void DisplayMenu()
        {
            string? mainDirPath = null;
            if (ListOfFiles != null && ListOfFiles.Count > 0)
                mainDirPath = ListOfFiles.First().GetMainDirectoryPath();
            int type = 0;
            int numberOfTypes = 2;
            bool isFirst = true;
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
                case 1: Text = CreateTextTableOfContents(); break;
                case 2: Text = CreateHyperlinksTableOfContents(); break;
            }
            Program.ChangeView(mainDirPath);
            Console.WriteLine(Text);
            Console.WriteLine();
        }

        private string CreateHyperlinksTableOfContents()
        {
            string tableOfContents = CreateTextTableOfContents();
            Dictionary<string, int> links = new Dictionary<string, int>();
            StringBuilder builder = new StringBuilder();
            foreach(string entry in tableOfContents.Split("\n", StringSplitOptions.RemoveEmptyEntries))
            {
                int textStart = entry.IndexOf("# ") + 2;
                string headerSpecifier = entry[..textStart];
                string hyperlink = Helpers.ConvertTextToHyperlink(entry);
                string link = Helpers.GetLinkPartFromLinkBlock(hyperlink);
                if (links.TryGetValue(link, out int qtt))
                {
                    links[link] = qtt + 1;
                    hyperlink = hyperlink.Insert(hyperlink.Length - 1, "-" + qtt.ToString());
                }
                else
                {
                    links.Add(link, 1);
                }
                builder.Append(headerSpecifier);
                builder.AppendLine(hyperlink);
            }
            return builder.ToString();
        }
        private string CreateTextTableOfContents()
        {
            List<string> appendedDirectories = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("## Spis treści");
            foreach (MdFile file in ListOfFiles)
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
                stringBuilder.Append(new String('#', dirNumber + 3));
                stringBuilder.Append(' ');
                stringBuilder.AppendLine(title);
            }
            return stringBuilder.ToString();
        }
    }
}
