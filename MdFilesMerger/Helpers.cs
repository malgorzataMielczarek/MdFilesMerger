﻿using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace MdFilesMerger
{
    internal static class Helpers
    {
        public static bool ContainsHyperlink(string? text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            return ContainsMarkdownHyperlink(text) || ContainsHtmlHyperlink(text) || ContainsHtmlImg(text);
        }

        public static string GetLink(string hyperlink)
        {
            if (ContainsMarkdownHyperlink(hyperlink))
            {
                int linkStart = hyperlink.IndexOf("](", StringComparison.Ordinal);
                int linkEnd = hyperlink.IndexOf(')', linkStart + 2);

                if (linkStart != -1 && linkEnd != -1)
                {
                    return hyperlink.Substring(linkStart + 2, linkEnd - linkStart - 2).Trim();
                }
            }

            else if (ContainsHtmlHyperlink(hyperlink))
            {
                int startTagIndax = hyperlink.IndexOf("<a ", StringComparison.Ordinal);
                int hrefIndex = hyperlink.IndexOf("href", startTagIndax + 3, StringComparison.Ordinal);

                return GetQuoteText(hyperlink, hrefIndex + 5);
            }

            else if (ContainsHtmlImg(hyperlink))
            {
                int startTagIndex = hyperlink.IndexOf("<img ", StringComparison.Ordinal);
                int srcIndex = hyperlink.IndexOf("src", startTagIndex + 5, StringComparison.Ordinal);

                return GetQuoteText(hyperlink, srcIndex + 4);
            }

            return String.Empty;
        }

        public static string GetText(string hyperlink)
        {
            string text;
            int hyperlinkTextSectionStart = -1, hyperlinkTextSectionEnd = -1;

            if (ContainsMarkdownHyperlink(hyperlink))
            {
                hyperlinkTextSectionStart = hyperlink.IndexOf('[');
                hyperlinkTextSectionEnd = hyperlink.IndexOf("](", StringComparison.Ordinal);
            }

            else if (ContainsHtmlHyperlink(hyperlink))
            {
                hyperlinkTextSectionEnd = hyperlink.IndexOf("</a>", StringComparison.Ordinal);
                hyperlinkTextSectionStart = hyperlink.LastIndexOf('>', hyperlinkTextSectionEnd);
            }

            else if (ContainsHtmlImg(hyperlink))
            {
                int hyperlinkStart = hyperlink.IndexOf("<img ", StringComparison.Ordinal);
                int altStart = hyperlink.IndexOf("alt", hyperlinkStart + 5, StringComparison.Ordinal);

                return GetQuoteText(hyperlink, altStart + 4);
            }

            if (hyperlinkTextSectionStart == -1 || hyperlinkTextSectionEnd == -1)
            {
                return String.Empty;
            }

            text = hyperlink.Substring(hyperlinkTextSectionStart + 1, hyperlinkTextSectionEnd - hyperlinkTextSectionStart - 1);

            return text.Trim();
        }

        public static string HyperlinkToText(string hyperlinkHeader)
        {
            if (!ContainsMarkdownHyperlink(hyperlinkHeader))
            {
                return hyperlinkHeader;
            }

            // add leading characters of hyperlinkHeader (characters before hyperlink, for example header specifiers)
            int hyperlinkStart = hyperlinkHeader.IndexOf('[');
            StringBuilder textHeader = new StringBuilder(hyperlinkHeader, 0, hyperlinkStart, hyperlinkHeader.Length);

            // add text part of hyperlink
            textHeader.Append(GetText(hyperlinkHeader));

            return textHeader.ToString().Trim();
        }

        public static string TextToHyperlink(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            // link: [text](link)

            // prepare text part

            // create StringBuilder for hyperlink
            StringBuilder hyperlink = new StringBuilder("[");

            // right trim text and add to hyperlink
            hyperlink.Append(text.TrimEnd());

            // remove leading '#' if text is a header and left trim text part (hyperlink[0] is '[')
            while (char.IsWhiteSpace(hyperlink[1]) || hyperlink[1] == '#')
            {
                hyperlink.Remove(1, 1);
            }

            hyperlink.Append(']');

            // prepare link: append letters (converted to lower), numbers and spaces replace with '-'
            int textLength = hyperlink.Length - 2;  // minus '[' and ']'
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

        private static bool ContainsHtmlHyperlink(string text)
        {
            return text.Contains("<a") && text.Contains("href") && text.Contains("</a>");
        }

        private static bool ContainsHtmlImg(string text)
        {
            return text.Contains("<img") && text.Contains("src") && text.Contains("alt") && text.Contains('>');
        }

        private static bool ContainsMarkdownHyperlink(string text)
        {
            return text.Contains('[') && text.Contains("](") && text.Contains(')');
        }

        private static string GetQuoteText(string text, int startIndex)
        {
            int textStart = -1, textEnd = -1;

            int firstQuotationMark = text.IndexOf('\"', startIndex);
            int firstApostrophe = text.IndexOf('\'', startIndex);

            if (firstQuotationMark == -1)
            {
                textStart = firstApostrophe;
                textEnd = text.IndexOf('\'', firstApostrophe + 1);
            }

            else if (firstApostrophe == -1)
            {
                textStart = firstQuotationMark;
                textEnd = text.IndexOf('\"', firstQuotationMark + 1);
            }

            else if (firstApostrophe < firstQuotationMark)
            {
                textStart = firstApostrophe;
                textEnd = text.IndexOf('\'', firstApostrophe + 1);
            }

            else
            {
                textStart = firstQuotationMark;
                textEnd = text.IndexOf('\"', firstQuotationMark + 1);
            }

            if (textStart == -1 || textEnd == -1)
            {
                return String.Empty;
            }

            return text.Substring(textStart + 1, textEnd - textStart - 1).Trim();
        }
    }
}
