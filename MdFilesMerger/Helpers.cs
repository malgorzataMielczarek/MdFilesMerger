namespace MdFilesMerger
{
    internal static class Helpers
    {
        public static string ConvertTextToHyperlink(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            while (text.StartsWith('#'))
            {
                text = text.Remove(0, 1);
            }
            text = text.Trim();
            string link = text.ToLower().Replace(" ", "-");
            for (int i = 0; i < link.Length; i++)
            {
                char c = link[i];
                if (!(char.IsLower(c) || char.IsNumber(c) || c == '-'))
                    link = link.Remove(i, 1);
            }
            string hyperlink = "[" + text + "](#" + link + ")";

            return hyperlink;
        }
        public static string ConvertHyperlinkHeaderToTextHeader(string hyperlinkHeader)
        {
            if (string.IsNullOrEmpty(hyperlinkHeader)
                || !(hyperlinkHeader.Contains('[') && hyperlinkHeader.Contains("](") && hyperlinkHeader.Contains(')')))
                return hyperlinkHeader;

            int hyperlinkStart = hyperlinkHeader.IndexOf('[');
            int hyperlinkTextSectionEnd = hyperlinkHeader.LastIndexOf(']');
            string text = hyperlinkHeader[..hyperlinkStart] + hyperlinkHeader.Substring(hyperlinkStart + 1, hyperlinkTextSectionEnd - hyperlinkStart - 1);

            return text.Trim();
        }
        public static string GetLinkPartFromLinkBlock(string linkBlock)
        {
            if (string.IsNullOrEmpty(linkBlock)
                || !(linkBlock.Contains('[') && linkBlock.Contains("](") && linkBlock.Contains(')')))
                return string.Empty;

            int linkStart = linkBlock.IndexOf("](");
            int linkEnd = linkBlock.IndexOf(')', linkStart);
            string text = linkBlock.Substring(linkStart + 2, linkEnd - linkStart - 2);

            return text.Trim();
        }
    }
}
