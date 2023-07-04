using FluentAssertions;
using MdFilesMerger.Domain.Common;
using Xunit;

namespace MdFilesMerger.Tests.Domain.Common
{
    public class HyperlinksTests
    {
        [Theory]
        [InlineData("<a href='link'>text</a>")]
        [InlineData("pretext <a href=\"link\">text</a>")]
        [InlineData("pretext<a href='link'>text</a>post-text")]
        [InlineData("<a href='link' target=\"_blank\">text</a> post-text")]
        [InlineData("<a href='link'>text</a>between<a href='link'>text</a>")]
        public void ContainsHyperlink_StringWithHtmlHyperlink_ReturnsTrue(string text)
        {
            // Arrange Act
            var result = Hyperlinks.ContainsHyperlink(text);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("<img alt=\"text\" src=\"link\" />")]
        [InlineData("pretext <img src=\"link\" alt=\"text\" />")]
        [InlineData("pretext <img alt=\"text\" height=\"100\" src=\"link\"/> post-text")]
        [InlineData("<img alt='text' src='link' />post-text")]
        [InlineData("<img alt=\"text\" src=\"link\" width=\"150\"/>between<img alt=\"\" src=\"link\" />")]
        public void ContainsHyperlink_StringWithHtmlImg_ReturnsTrue(string text)
        {
            // Arrange Act
            var result = Hyperlinks.ContainsHyperlink(text);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("[text](link)")]
        [InlineData("pretext [text](link)")]
        [InlineData("pretext [text](link) post-text")]
        [InlineData("[text](link) post-text")]
        [InlineData("[text](link)between[text](link)")]
        public void ContainsHyperlink_StringWithMarkdownHyperlink_ReturnsTrue(string text)
        {
            // Arrange Act
            var result = Hyperlinks.ContainsHyperlink(text);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("![text](link)")]
        [InlineData("pretext ![text](link)")]
        [InlineData("pretext ![text](link) post-text")]
        [InlineData("![text](link) post-text")]
        [InlineData("![text](link)between![text](link)")]
        public void ContainsHyperlink_StringWithMarkdownImg_ReturnsTrue(string text)
        {
            // Arrange Act
            var result = Hyperlinks.ContainsHyperlink(text);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("b<a")]
        [InlineData("When I write [text] I really mean dupa")]
        [InlineData("(link) is not a link")]
        [InlineData("Not writing tests is src of many problems.")]
        [InlineData("[text]i(link)")]
        [InlineData("<a href\"almost\">link<a>)")]
        [InlineData(null)]
        public void ContainsHyperlink_StringWithoutHyperlinks_ReturnsFalse(string? text)
        {
            // Arrange Act
            var result = Hyperlinks.ContainsHyperlink(text);

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("<a href='link'>text</a>")]
        [InlineData("pretext <a href=\"link\">text</a>")]
        [InlineData("pretext<a href='link'>text</a>post-text")]
        [InlineData("<a href='link' target=\"_blank\">text</a> post-text")]
        [InlineData("<a href='link'>text</a>between<a href='link2'>text</a>")]
        public void GetLink_StringWithHtmlHyperlink_ReturnsHref(string text)
        {
            // Arrange Act
            var result = Hyperlinks.GetLink(text);

            // Assert
            result.Should().Be("link");
        }

        [Theory]
        [InlineData("<img alt=\"text\" src=\"link\" />")]
        [InlineData("pretext <img src=\"link\" alt=\"text\" />")]
        [InlineData("pretext <img alt=\"text\" height=\"100\" src=\"link\"/> post-text")]
        [InlineData("<img alt='text' src='link' />post-text")]
        [InlineData("<img alt=\"text\" src=\"link\" width=\"150\"/>between<img alt=\"\" src=\"link2\" />")]
        public void GetLink_StringWithHtmlImg_ReturnsSrc(string text)
        {
            // Arrange Act
            var result = Hyperlinks.GetLink(text);

            // Assert
            result.Should().Be("link");
        }

        [Theory]
        [InlineData("[text](link)")]
        [InlineData("pretext [text](link)")]
        [InlineData("pretext [text](link) post-text")]
        [InlineData("[text](link) post-text")]
        [InlineData("[text](link)between[text](link2)")]
        public void GetLink_StringWithMarkdownHyperlink_ReturnsLink(string text)
        {
            // Arrange Act
            var result = Hyperlinks.GetLink(text);

            // Assert
            result.Should().Be("link");
        }

        [Theory]
        [InlineData("![text](link)")]
        [InlineData("pretext ![text](link)")]
        [InlineData("pretext ![text](link) post-text")]
        [InlineData("![text](link) post-text")]
        [InlineData("![text](link)between![text](link2)")]
        public void GetLink_StringWithMarkdownImg_ReturnsSrc(string text)
        {
            // Arrange Act
            var result = Hyperlinks.GetLink(text);

            // Assert
            result.Should().Be("link");
        }

        [Theory]
        [InlineData("b<a")]
        [InlineData("When I write [text] I really mean dupa")]
        [InlineData("(link) is not a link")]
        [InlineData("Not writing tests is src of many problems.")]
        [InlineData("[text]i(link)")]
        [InlineData("<a href\"almost\">link<a>)")]
        [InlineData(null)]
        public void GetLink_StringWithoutHyperlinks_ReturnsEmptyString(string? text)
        {
            // Arrange Act
            var result = Hyperlinks.GetLink(text);

            // Assert
            result.Should().Be(string.Empty);
        }

        [Theory]
        [InlineData("<a href='link'>text</a>")]
        [InlineData("pretext <a href=\"link\">text</a>")]
        [InlineData("pretext<a href='link'>text</a>post-text")]
        [InlineData("<a href='link' target=\"_blank\">text</a> post-text")]
        [InlineData("<a href='link'>text</a>between<a href='link2'>text2</a>")]
        public void GetText_StringWithHtmlHyperlink_ReturnsVisibleText(string text)
        {
            // Arrange Act
            var result = Hyperlinks.GetText(text);

            // Assert
            result.Should().Be("text");
        }

        [Theory]
        [InlineData("<img alt=\"text\" src=\"link\" />")]
        [InlineData("pretext <img src=\"link\" alt=\"text\" />")]
        [InlineData("pretext <img alt=\"text\" height=\"100\" src=\"link\"/> post-text")]
        [InlineData("<img alt='text' src='link' />post-text")]
        [InlineData("<img alt=\"text\" src=\"link\" width=\"150\"/>between<img alt=\"\" src=\"link2\" />")]
        public void GetText_StringWithHtmlImg_ReturnsAlt(string text)
        {
            // Arrange Act
            var result = Hyperlinks.GetText(text);

            // Assert
            result.Should().Be("text");
        }

        [Theory]
        [InlineData("[text](link)")]
        [InlineData("pretext [text](link)")]
        [InlineData("pretext [text](link) post-text")]
        [InlineData("[text](link) post-text")]
        [InlineData("[text](link)between[text2](link2)")]
        public void GetText_StringWithMarkdownHyperlink_ReturnsVisibleText(string text)
        {
            // Arrange Act
            var result = Hyperlinks.GetText(text);

            // Assert
            result.Should().Be("text");
        }

        [Theory]
        [InlineData("![text](link)")]
        [InlineData("pretext ![text](link)")]
        [InlineData("pretext ![text](link) post-text")]
        [InlineData("![text](link) post-text")]
        [InlineData("![text](link)between![text2](link2)")]
        public void GetText_StringWithMarkdownImg_ReturnsAlt(string text)
        {
            // Arrange Act
            var result = Hyperlinks.GetText(text);

            // Assert
            result.Should().Be("text");
        }

        [Theory]
        [InlineData("b<a")]
        [InlineData("When I write [text] I really mean dupa")]
        [InlineData("(link) is not a link")]
        [InlineData("Not writing tests is src of many problems.")]
        [InlineData("[text]i(link)")]
        [InlineData("<a href\"almost\">link<a>)")]
        [InlineData(null)]
        public void GetText_StringWithoutHyperlinks_ReturnsEmptyString(string? text)
        {
            // Arrange Act
            var result = Hyperlinks.GetText(text);

            // Assert
            result.Should().Be(string.Empty);
        }

        [Theory]
        [InlineData("<a href='link'>text</a>", "text")]
        [InlineData("pretext <a href=\"link\">text</a>", "pretext text")]
        [InlineData("href='pretext' <a href=\"link\">text</a>", "href='pretext' text")]
        [InlineData("pretext<a href='link'>text</a>post-text", "pretexttextpost-text")]
        [InlineData("<a href='link' target=\"_blank\">text</a> post-text", "text post-text")]
        [InlineData("<a href='link'>te</axt</a>between<a href='link2'>text2</a>", "te</axtbetween<a href='link2'>text2</a>")]
        public void HyperlinkToText_StringWithHtmlHyperlink_ReturnsTextWithFirstHyperlinkChangedToPlainText(string text, string expectedResult)
        {
            // Arrange Act
            var result = Hyperlinks.HyperlinkToText(text);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("<img alt=\"text\" src=\"link\" />")]
        [InlineData("pretext <img src=\"link\" alt=\"text\" />")]
        [InlineData("pretext <img alt=\"text\" height=\"100\" src=\"link\"/> post-text")]
        [InlineData("<img alt='text' src='link' />post-text")]
        [InlineData("<img alt=\"text\" src=\"link\" width=\"150\"/>between<img alt=\"\" src=\"link2\" />")]
        public void HyperlinkToText_StringWithHtmlImg_ReturnsUnchangedString(string text)
        {
            // Arrange Act
            var result = Hyperlinks.HyperlinkToText(text);

            // Assert
            result.Should().Be(text);
        }

        [Theory]
        [InlineData("[text](link)", "text")]
        [InlineData("pretext [text](link)", "pretext text")]
        [InlineData("pretext [text](link) post-text", "pretext text post-text")]
        [InlineData("[text](link) post-text", "text post-text")]
        [InlineData("[text](link)between[text2](link2)", "textbetween[text2](link2)")]
        [InlineData("[pretext] [text](link)between[text2](link2)", "[pretext] textbetween[text2](link2)")]
        public void HyperlinkToText_StringWithMarkdownHyperlink_ReturnsTextWithFirstHyperlinkChangedToPlainText(string text, string expectedResult)
        {
            // Arrange Act
            var result = Hyperlinks.HyperlinkToText(text);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("![text](link)")]
        [InlineData("pretext ![text](link)")]
        [InlineData("pretext ![text](link) post-text")]
        [InlineData("![text](link) post-text")]
        [InlineData("![text](link)between![text2](link2)")]
        public void HyperlinkToText_StringWithMarkdownImg_ReturnsUnchangedString(string text)
        {
            // Arrange Act
            var result = Hyperlinks.HyperlinkToText(text);

            // Assert
            result.Should().Be(text);
        }

        [Theory]
        [InlineData("b<a")]
        [InlineData("When I write [text] I really mean dupa")]
        [InlineData("(link) is not a link")]
        [InlineData("Not writing tests is src of many problems.")]
        [InlineData("[text]i(link)")]
        [InlineData("<a href\"almost\">link<a>)")]
        [InlineData(null)]
        public void HyperlinkToText_StringWithoutHyperlinks_ReturnsUnchangedString(string? text)
        {
            // Arrange Act
            var result = Hyperlinks.HyperlinkToText(text);

            // Assert
            result.Should().Be(text);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void TextToHyperlink_NullOrWhiteSpace_ReturnsStringEmpty(string text)
        {
            // Arrange Act
            var result = Hyperlinks.TextToHyperlink(text);

            // Assert
            result.Should().Be(string.Empty);
        }

        [Theory]
        [InlineData("What is IT?", "[What is IT?](#what-is-it)")]
        [InlineData("I need $", "[I need $](#i-need-)")]
        [InlineData("## Header2 ", "[Header2](#header2)")]
        [InlineData("---", "[---](#---)")]
        public void TextToHyperlink_String_ReturnsHyperlinkLeadingToHeaderWithGivenText(string text, string expectedHyperlink)
        {
            // Arrange Act
            var result = Hyperlinks.TextToHyperlink(text);

            // Assert
            result.Should().Be(expectedHyperlink);
        }

        [Theory]
        [InlineData("–––")]
        [InlineData("???")]
        [InlineData("###")]
        public void TextToHyperlink_UnlinkableString_ReturnsGivenString(string text)
        {
            // Arrange Act
            var result = Hyperlinks.TextToHyperlink(text);

            // Assert
            result.Should().Be(text);
        }
    }
}