using System.Text;

namespace MdFilesMerger
{
    internal static class Helpers
    {
        public static string ConvertTextToHyperlink(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            //link: [text](link)

            //prepare text part
            //create StringBuilder for hyperlink
            StringBuilder hyperlink = new StringBuilder("[");
            //right trim text and add to hyperlink
            hyperlink.Append(text.TrimEnd());
            //remove leading '#' if text is a header and left trim text part (hyperlink[0] is '[')
            while (char.IsWhiteSpace(hyperlink[1]) || hyperlink[1] == '#')
            {
                hyperlink.Remove(1, 1);
            }
            hyperlink.Append(']');

            //prepare link part: append letters (converted to lower), numbers and spaces replace with '-'
            int textLength = hyperlink.Length - 2;  //minus '[' and ']'
            hyperlink.Append("(#");
            for (int i = 1; i <= textLength; i++)
            {
                char c = hyperlink[i];
                if (char.IsLetter(c))
                {
                    hyperlink.Append(char.ToLower(c));
                }
                else if(char.IsNumber(c))
                {
                    hyperlink.Append(c);
                }
                else if(c == ' ')
                {
                    hyperlink.Append('-');
                }
            }
            hyperlink.Append(')');

            return hyperlink.ToString();
        }
        public static string ConvertHyperlinkHeaderToTextHeader(string hyperlinkHeader)
        {
            if(!ContainsLinkBlock(hyperlinkHeader)) return hyperlinkHeader;

            //add leading characters of hyperlinkHeader (characters before hyperlink, for example header specifiers)
            int hyperlinkStart = hyperlinkHeader.IndexOf('[');
            StringBuilder textHeader = new StringBuilder(hyperlinkHeader, 0, hyperlinkStart, hyperlinkHeader.Length);

            //add text part of hyperlink
            textHeader.Append(GetTextPartFromLinkBlock(hyperlinkHeader));

            return textHeader.ToString().Trim();
        }
        public static string GetLinkPartFromLinkBlock(string linkBlock)
        {
            if (!ContainsLinkBlock(linkBlock)) return string.Empty;

            int linkStart = linkBlock.IndexOf("](");
            int linkEnd = linkBlock.IndexOf(')', linkStart);

            string link = linkBlock.Substring(linkStart + 2, linkEnd - linkStart - 2);

            return link.Trim();
        }
        public static string GetTextPartFromLinkBlock(string linkBlock)
        {
            if (!ContainsLinkBlock(linkBlock)) return string.Empty;

            int hyperlinkStart = linkBlock.IndexOf('[');
            int hyperlinkTextSectionEnd = linkBlock.IndexOf("](");

            string text = linkBlock.Substring(hyperlinkStart + 1, hyperlinkTextSectionEnd - hyperlinkStart - 1);

            return text.Trim();
        }
        public static bool ContainsLinkBlock(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            
            return text.Contains('[') && text.Contains("](") && text.Contains(')');
        }
    }
}
