using FluentAssertions;
using MdFilesMerger.Domain.Common;
using Xunit;

namespace MdFilesMerger.Tests.Domain.Common
{
    public class MdFileTests
    {
        [Theory]
        [InlineData("valid/path/to/file")]
        [InlineData("valid/path/to/file.md")]
        [InlineData("valid\\path\\to\\file")]
        [InlineData("valid\\path\\to\\file.md")]
        public void SetPath_ValidPath_SetsNameToPathEndedWithMdExtensionModifiedDateToNowAndReturnsTrue(string path)
        {
            // Arrange
            DateTime oldDate = DateTime.Now;
            MdFile mdFile = new MdFile() { Name = "old/path.md", ModifiedDate = oldDate };

            // Act
            var result = mdFile.SetPath(path);

            // Assert
            result.Should().BeTrue();
            mdFile.Name.Should().Be("valid/path/to/file.md");
            mdFile.ModifiedDate.Should().BeAfter(oldDate).And.BeBefore(DateTime.Now);
        }
    }
}