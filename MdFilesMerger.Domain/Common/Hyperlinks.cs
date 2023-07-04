using System.Text;
using System.Text.RegularExpressions;

namespace MdFilesMerger.Domain.Common
{
    /// <summary>
    ///     Static class containing methods for hyperlinks management.
    /// </summary>
    public static class Hyperlinks
    {
        private const string PROPERTY_PATERN = @"(\s*\b[a-zA-Z]+\b" + PROPERTY_VALUE_PATERN + ")";
        private const string PROPERTY_VALUE_PATERN = @"\s*=\s*((""[\w\s]*"")|('[\w\s]*'))\s*";
        private static readonly Regex _htmlHyperlinkRegex = new Regex(@"<a\b" + PROPERTY_PATERN + @"*\s*\bhref\b" + PROPERTY_VALUE_PATERN + PROPERTY_PATERN + @"*>.*?<\s*/\s*\ba>", RegexOptions.Compiled);   // <a href="link">text</a>
        private static readonly Regex _htmlImgRegex = new Regex(@"<img\b(?=" + PROPERTY_PATERN + @"*\s*\bsrc\b" + PROPERTY_VALUE_PATERN + @")(?=" + PROPERTY_PATERN + @"*\s*\balt\b" + PROPERTY_VALUE_PATERN + ")" + PROPERTY_PATERN + @"*\s*((/\s*>)|(><\s*/\s*\bimg>))", RegexOptions.Compiled); // <img src="link" alt="text"/>

        /// <summary>
        ///     Determines whether the specified text contains hyperlink or image.
        /// </summary>
        /// <param name="text"> The text. </param>
        /// <returns>
        ///     <see langword="true"/> if the specified text contains hyperlink or image; otherwise,
        ///     <see langword="false"/>.
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
        ///     Gets the link part of the first found hyperlink/image (underlying destination, where
        ///     link points and directs to after clicking or from which image is taken).
        /// </summary>
        /// <remarks>
        ///     Method looks for the markdown hiperlinks in the first place. If non was found it
        ///     searches in sequance for markdown image, html hyperlink and html image. Returned
        ///     link belongs to the first found hyperlink/image.
        /// </remarks>
        /// <param name="text"> The text containing hyperlink/image. </param>
        /// <returns>
        ///     String with the link, if passed value contained hyperlink or image; otherwise <see cref="string.Empty"/>.
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
        ///     Gets the text part of the first found hyperlink/image (text that will be displayed
        ///     as link or is image's alternative text).
        /// </summary>
        /// <remarks>
        ///     Method looks for the markdown hiperlinks in the first place. If none was found it
        ///     searches in sequance for markdown image, html hyperlink and html image. Returned
        ///     text belongs to the first found hyperlink/image.
        /// </remarks>
        /// <param name="text"> The text containing hyperlink/image. </param>
        /// <returns>
        ///     If hyperlink was found, returns its visible text; if image was found, returns its
        ///     alternative text; otherwise returns <see cref="string.Empty"/>.
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
        ///     The first found hyperlink in the given text is replaced by its text part. The rest
        ///     of the passed text stays the same.
        ///     <para>
        ///         Method searches for markdown hyperlink in the first place. If none was found it
        ///         looks for html hyperlink.
        ///     </para>
        /// </remarks>
        /// <param name="text"> The text containing hyperlink. For example header. </param>
        /// <returns>
        ///     The passed text with the first found hyperlink converted. If given text doesn't
        ///     contain any hyperlinks, it is returned in unchanged form.
        /// </returns>
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