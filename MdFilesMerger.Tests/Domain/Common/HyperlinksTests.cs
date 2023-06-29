using FluentAssertions;
using MdFilesMerger.Domain.Common;
using Xunit;

namespace MdFilesMerger.Tests.Domain.Common
{
    public class HyperlinksTests
    {
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
    }
}