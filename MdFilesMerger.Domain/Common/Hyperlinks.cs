﻿using System.Text;

namespace MdFilesMerger.Domain.Common
{
    /// <summary>
    ///     Static class containing methods for hyperlinks management.
    /// </summary>
    public static class Hyperlinks
    {
        /// <summary>
        ///     Determines whether the specified text contains hyperlink.
        /// </summary>
        /// <param name="text"> The text. </param>
        /// <returns>
        ///     <see langword="true"/> if the specified text contains hyperlink; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool ContainsHyperlink(string? text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            return ContainsMarkdownHyperlink(text) || ContainsHtmlHyperlink(text) || ContainsHtmlImg(text);
        }

        /// <summary>
        ///     Gets the link part of the first hyperlink (underlying destination, where link points
        ///     and directs to after clicking).
        /// </summary>
        /// <param name="hyperlink"> The text containing hyperlink. </param>
        /// <returns>
        ///     String with the link, if passed value contained hyperlink; otherwise <see cref="string.Empty"/>.
        /// </returns>
        public static string GetLink(string hyperlink)
        {
            if (string.IsNullOrWhiteSpace(hyperlink))
            {
                return string.Empty;
            }

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

            return string.Empty;
        }

        /// <summary>
        ///     Gets the text part of the first hyperlink (text that will be displayed as link).
        /// </summary>
        /// <param name="hyperlink"> The text containing hyperlink. </param>
        /// <returns>
        ///     String with the link description (visible text), if passed value contained
        ///     hyperlink; otherwise <see cref="string.Empty"/>.
        /// </returns>
        public static string GetText(string hyperlink)
        {
            if (string.IsNullOrWhiteSpace(hyperlink))
            {
                return string.Empty;
            }

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
                return string.Empty;
            }

            text = hyperlink.Substring(hyperlinkTextSectionStart + 1, hyperlinkTextSectionEnd - hyperlinkTextSectionStart - 1);

            return text.Trim();
        }

        /// <summary>
        ///     Converts the text containing hyperlink to the plain text.
        /// </summary>
        /// <remarks>
        ///     The first hyperlink in the given text is replaced by its text part. The rest of the
        ///     passed text stays the same.
        /// </remarks>
        /// <param name="hyperlinkHeader"> The text containing hyperlink. For example header. </param>
        /// <returns> The passed text with it's first hyperlink converted. </returns>
        // TODO: Implement searching for whole hyperlink pattern and getting text part from it
        // TODO: Check if it is not markdown image
        // TODO: Implement version for html hyperlinks
        // TODO: Add text before AND AFTER hyperlink
        public static string HyperlinkToText(string hyperlinkHeader)
        {
            if (string.IsNullOrWhiteSpace(hyperlinkHeader) || !ContainsMarkdownHyperlink(hyperlinkHeader))
            {
                return hyperlinkHeader;
            }

            // add leading characters of hyperlinkHeader (characters before hyperlink, for example
            // header specifiers)
            int hyperlinkStart = hyperlinkHeader.IndexOf('[');
            StringBuilder textHeader = new StringBuilder(hyperlinkHeader, 0, hyperlinkStart, hyperlinkHeader.Length);

            // add text part of hyperlink
            textHeader.Append(GetText(hyperlinkHeader));

            return textHeader.ToString().Trim();
        }

        /// <summary>
        ///     Converts text to Markdown hyperlink leading to appropriate section of the file that
        ///     it will be placed in.
        /// </summary>
        /// <remarks>
        ///     The specified text is treated as text part of Markdown hyperlink. If text is a
        ///     Markdown style header the leading '#' characters are previously removed. The text is
        ///     also trimmed. Link leads to appropriate section of the file (that it will be place
        ///     in). Name of the section is the same as the text.
        /// </remarks>
        /// <param name="text"> The text to convert. </param>
        /// <returns>
        ///     Prepared hyperlink. If given text is unlinkable, given text (without any changes) is returned
        /// </returns>
        public static string TextToHyperlink(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            // link: [text](link)

            // prepare text part
            StringBuilder textPart = new StringBuilder(text.Trim());
            // remove leading '#' if text is a header and left trim text part
            while (textPart.Length > 0 && (char.IsWhiteSpace(textPart[0]) || textPart[0] == '#'))
            {
                textPart.Remove(0, 1);
            }

            // prepare link: append letters (converted to lower), numbers, dashes ('-') and spaces
            // replace with dashes
            StringBuilder linkPart = new StringBuilder();
            int textLength = textPart.Length;
            foreach (var c in textPart.ToString())
            {
                if (char.IsLetter(c))
                {
                    linkPart.Append(char.ToLower(c));
                }
                else if (char.IsNumber(c) || c == '-')
                {
                    linkPart.Append(c);
                }
                else if (c == ' ')
                {
                    linkPart.Append('-');
                }
            }

            if (linkPart.Length > 0)
            {
                return "[" + textPart.ToString() + "](#" + linkPart + ")";
            }
            else
            {
                return text;
            }
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
                return string.Empty;
            }

            return text.Substring(textStart + 1, textEnd - textStart - 1).Trim();
        }
    }
}