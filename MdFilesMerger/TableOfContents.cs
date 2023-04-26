using System.Text;

namespace MdFilesMerger
{
    internal class TableOfContents
    {
        private readonly List<MdFile> list;

        public TableOfContents(List<MdFile> list)
        {
            this.list = list;
        }

        public string CreateHyperlinksTableOfContents()
        {
            List<string> appendedDirectories = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("## Spis treści");
            foreach (MdFile file in list)
            {
                string title = file.GetFileHeader();
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
                        stringBuilder.AppendLine(" " + ConvertTextToHyperlink(dir));
                    }
                }
                stringBuilder.Append(new String('#', dirNumber + 3));
                stringBuilder.Append(' ');
                stringBuilder.AppendLine(ConvertTextToHyperlink(title));
            }
            return stringBuilder.ToString();
        }
        public string CreateTextTableOfContents()
        {
            string hyperlinkTableOfContents = CreateHyperlinksTableOfContents();
            StringBuilder builder = new StringBuilder();
            foreach (string entry in hyperlinkTableOfContents.Split("\n"))
            {
                builder.AppendLine(ConvertHyperlinkHeaderToTextHeader(entry));
            }
            return builder.ToString();
        }

        private string ConvertTextToHyperlink(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            string link = text.ToLower().Replace(" ", "-");
            for (int i = 0; i < link.Length; i++)
            {
                char c = link[i];
                if (!(char.IsLower(c) || char.IsNumber(c) || c == '-'))
                    link = link.Remove(i, 1);
            }
            while (text.StartsWith('#'))
            {
                text = text.Remove(0, 1);
            }
            string hyperlink = "[" + text + "](" + link + ")";

            return hyperlink;
        }
        private string ConvertHyperlinkHeaderToTextHeader(string hyperlinkHeader)
        {
            if (string.IsNullOrEmpty(hyperlinkHeader)
                || !(hyperlinkHeader.Contains('[') && hyperlinkHeader.Contains(']') && hyperlinkHeader.Contains('(') && hyperlinkHeader.Contains(')')))
                return hyperlinkHeader;

            int hyperlinkStart = hyperlinkHeader.IndexOf('[');
            int hyperlinkTextSectionEnd = hyperlinkHeader.LastIndexOf(']');
            string text = hyperlinkHeader[..hyperlinkStart] + hyperlinkHeader.Substring(hyperlinkStart + 1, hyperlinkTextSectionEnd - hyperlinkStart - 1);

            return text;
        }
    }
}
