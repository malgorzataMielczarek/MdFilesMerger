using System.Text;
using System.Text.RegularExpressions;

namespace MdFilesMerger.Domain.Common
{
    /// <summary>
    ///     Static class containing methods for hyperlinks management.
    /// </summary>
    public static class Hyperlinks
    {
        // TODO: Fix hyperlinks regex to work correctly when there are few hyperlinks in the string
        private static readonly Regex _htmlHyperlinkRegex = new Regex(@"<a\b(\s*[a-zA-Z]+\s*=\s*(("".*"")|('.*'))\s*)*\s*\bhref\b\s*=\s*(("".*"")|('.*'))\s*(\s*[a-zA-Z]+\s*=\s*(("".*"")|('.*'))\s*)*>.*</\s*\ba>", RegexOptions.Compiled);   // <a href="link">text</a>

        private static readonly Regex _htmlImgRegex = new Regex(@"<img\b(?=(\s*[a-zA-Z]+\s*=\s*(("".*"")|('.*'))\s*)*\s*\bsrc\b\s*=\s*(("".*"")|('.*')))(?=(\s*[a-zA-Z]+\s*=\s*(("".*"")|('.*'))\s*)*\s*\balt\b\s*=\s*(("".*"")|('.*')))(\s*[a-zA-Z]+\s*=\s*(("".*"")|('.*'))\s*)*\s*((/\s*>)|(><\s*/\s*\bimg>))", RegexOptions.Compiled); // <img src="link" alt="text"/>

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

            return ExtractMarkdownHyperlink(text) != string.Empty || ExtractMarkdownImg(text) != string.Empty || ContainsHtmlHyperlink(text) || ContainsHtmlImg(text);
        }

        /// <summary>
        ///     Gets the link part of the first hyperlink (underlying destination, where link points
        ///     and directs to after clicking).
        /// </summary>
        /// <param name="text"> The text containing hyperlink. </param>
        /// <returns>
        ///     String with the link, if passed value contained hyperlink; otherwise <see cref="string.Empty"/>.
        /// </returns>
        public static string GetLink(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            Match match;
            string hyperlink = ExtractMarkdownHyperlink(text);
            if (hyperlink != string.Empty || (hyperlink = ExtractMarkdownImg(text)) != string.Empty)
            {
                int linkStart = hyperlink.IndexOf("](", StringComparison.Ordinal);
                int linkEnd = hyperlink.LastIndexOf(')');

                if (linkStart != -1 && linkEnd != -1)
                {
                    return hyperlink[(linkStart + 2)..linkEnd];
                }
            }
            else if ((match = _htmlHyperlinkRegex.Match(text)).Success)
            {
                int hrefIndex = match.Value.IndexOf("href", StringComparison.Ordinal);

                return GetQuoteText(match.Value, hrefIndex + 5);
            }
            else if ((match = _htmlImgRegex.Match(text)).Success)
            {
                int srcIndex = match.Value.IndexOf("src", StringComparison.Ordinal);

                return GetQuoteText(match.Value, srcIndex + 4);
            }

            return string.Empty;
        }

        /// <summary>
        ///     Gets the text part of the first hyperlink (text that will be displayed as link).
        /// </summary>
        /// <param name="text"> The text containing hyperlink. </param>
        /// <returns>
        ///     String with the link description (visible text), if passed value contained
        ///     hyperlink; otherwise <see cref="string.Empty"/>.
        /// </returns>
        public static string GetText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            string textPart;
            int hyperlinkTextSectionStart = -1, hyperlinkTextSectionEnd = -1;

            Match match;
            string hyperlink = ExtractMarkdownHyperlink(text);
            if (hyperlink != string.Empty)
            {
                hyperlinkTextSectionStart = 0;
                hyperlinkTextSectionEnd = hyperlink.IndexOf("](", StringComparison.Ordinal);
            }
            else if ((hyperlink = ExtractMarkdownImg(text)) != string.Empty)
            {
                hyperlinkTextSectionStart = 1;
                hyperlinkTextSectionEnd = hyperlink.IndexOf("](", StringComparison.Ordinal);
            }
            else if ((match = _htmlHyperlinkRegex.Match(text)).Success)
            {
                hyperlink = match.Value;
                hyperlinkTextSectionEnd = hyperlink.IndexOf("</a>", StringComparison.Ordinal);
                hyperlinkTextSectionStart = hyperlink.LastIndexOf('>', hyperlinkTextSectionEnd);
            }
            else if ((match = _htmlImgRegex.Match(text)).Success)
            {
                int altStart = match.Value.IndexOf("alt", StringComparison.Ordinal);

                return GetQuoteText(match.Value, altStart + 4);
            }

            if (hyperlinkTextSectionStart == -1 || hyperlinkTextSectionEnd == -1)
            {
                return string.Empty;
            }

            textPart = hyperlink.Substring(hyperlinkTextSectionStart + 1, hyperlinkTextSectionEnd - hyperlinkTextSectionStart - 1);

            return textPart.Trim();
        }

        /// <summary>
        ///     Converts the text containing hyperlink to the plain text.
        /// </summary>
        /// <remarks>
        ///     The first hyperlink in the given text is replaced by its text part. The rest of the
        ///     passed text stays the same.
        /// </remarks>
        /// <param name="text"> The text containing hyperlink. For example header. </param>
        /// <returns> The passed text with it's first hyperlink converted. </returns>
        public static string HyperlinkToText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            string hyperlink = ExtractMarkdownHyperlink(text);
            if (hyperlink == string.Empty)
            {
                Match match = _htmlHyperlinkRegex.Match(text);

                if (match.Success)
                {
                    hyperlink = match.Value;
                }
            }

            if (hyperlink != string.Empty)
            {
                return text.Replace(hyperlink, GetText(hyperlink));
            }
            else
            {
                return text;
            }
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
            return _htmlHyperlinkRegex.Match(text).Success;
            //return text.Contains("<a") && text.Contains("href") && text.Contains("</a>");
        }

        private static bool ContainsHtmlImg(string text)
        {
            return _htmlImgRegex.Match(text).Success;
            //return text.Contains("<img") && text.Contains("src") && text.Contains("alt") && text.Contains('>');
        }

        private static string ExtractMarkdownHyperlink(string text)
        {
            int middle = 0;
            while (middle < text.Length && (middle = text.IndexOf("](", middle, StringComparison.Ordinal)) != -1)
            {
                int begin = -1;
                int bracketsToFind = 1;

                // find beginning
                for (int i = middle - 1; i >= 0; i--)
                {
                    if (text[i] == ']')
                    {
                        bracketsToFind++;
                    }
                    else if (text[i] == '[')
                    {
                        bracketsToFind--;
                        if (bracketsToFind == 0)
                        {
                            // make sure it's not an image
                            if (i == 0 || text[i - 1] != '!')
                            {
                                begin = i;
                            }

                            break;
                        }
                    }
                }

                // if beginning found, find end
                if (begin != -1)
                {
                    bracketsToFind = 1;
                    for (int i = middle + 2; i < text.Length; i++)
                    {
                        if (text[i] == '(')
                        {
                            bracketsToFind++;
                        }
                        else if (text[i] == ')')
                        {
                            bracketsToFind--;

                            // if end found, return hyperlink
                            if (bracketsToFind == 0)
                            {
                                return text[begin..(i + 1)];
                            }
                        }
                    }
                }

                middle += 2;
            }

            return string.Empty;
        }

        private static string ExtractMarkdownImg(string text)
        {
            int begin = 0;

            while (begin < text.Length && (begin = text.IndexOf("![", begin, StringComparison.Ordinal)) != -1)
            {
                // find middle - "]("
                int middle = -1;
                int bracketsToFind = 1;
                for (int i = begin + 2; i < text.Length; i++)
                {
                    if (text[i] == '[')
                    {
                        bracketsToFind++;
                    }
                    else if (text[i] == ']')
                    {
                        bracketsToFind--;
                        if (bracketsToFind == 0)
                        {
                            if (i < text.Length - 2 && text[i + 1] == '(')
                            {
                                middle = i;
                            }

                            break;
                        }
                    }
                }

                // if middle found, find end
                if (middle != -1)
                {
                    bracketsToFind = 1;
                    for (int i = middle + 2; i < text.Length; i++)
                    {
                        if (text[i] == '(')
                        {
                            bracketsToFind++;
                        }
                        else if (text[i] == ')')
                        {
                            bracketsToFind--;
                            // if end found, return img
                            if (bracketsToFind == 0)
                            {
                                return text[begin..(i + 1)];
                            }
                        }
                    }
                }

                begin += 2;
            }

            return string.Empty;
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