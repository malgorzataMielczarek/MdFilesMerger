using FluentAssertions;
using MdFilesMerger.Domain.Common;
using Xunit;

namespace MdFilesMerger.Tests.Domain.Common
{
    public class BaseDirectoryTests
    {
        [Fact]
        public void CompareTo_EquivalentIDirectory_ReturnsZero()
        {
            // Arrange
            BaseDirectory thisDir = new BaseDirectory() { Name = "equal" };
            BaseDirectory otherDir = new BaseDirectory() { Name = "equal" };

            // Act
            var result = thisDir.CompareTo(otherDir);

            // Assert
            result.Should().Be(0);
        }

        [Theory]
        [InlineData("dir", "dir2")]
        [InlineData("dir2", "dir10")]
        [InlineData("dir10", "dir10/dir10a")]
        [InlineData("dir10/dir10a", "dir10/file10.md")]
        [InlineData("dir10/file10.md", "dir10/dir10a/file10.md")]
        public void CompareTo_FollowingIDirectory_ReturnsLessThanZero(string thisName, string otherName)
        {
            // Arrange
            BaseDirectory thisDir = new BaseDirectory() { Name = thisName };
            BaseDirectory otherDir = new BaseDirectory() { Name = otherName };

            // Act
            var result = thisDir.CompareTo(otherDir);

            // Assert
            result.Should().BeLessThan(0);
        }

        [Theory]
        [InlineData("dir2", "dir")]
        [InlineData("dir10", "dir2")]
        [InlineData("dir10/dir10a", "dir10")]
        [InlineData("dir10/file10.md", "dir10/dir10a")]
        [InlineData("dir10/dir10a/file10.md", "dir10/file10.md")]
        public void CompareTo_PrecedingIDirectory_ReturnsGreaterThanZero(string thisName, string otherName)
        {
            // Arrange
            BaseDirectory thisDir = new BaseDirectory() { Name = thisName };
            BaseDirectory otherDir = new BaseDirectory() { Name = otherName };

            // Act
            var result = thisDir.CompareTo(otherDir);

            // Assert
            result.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("        ")]
        [InlineData("th*s/*s/inval*d/path")]
        [InlineData("another>path")]
        [InlineData("file?")]
        [InlineData("other|file")]
        [InlineData("one\tmore\\path")]
        [InlineData("not\\in:\\root")]
        [InlineData(null)]
        public void SetPath_InvalidPath_SetsNameToNullAndReturnsFalse(string? inputPath)
        {
            // Arrange
            string oldPath = "this/is/old/path";
            BaseDirectory baseDirectory = new BaseDirectory(1, oldPath, DateTime.Now);
            DateTime oldModDat = baseDirectory.ModifiedDate;

            // Act
            var result = baseDirectory.SetPath(inputPath);

            // Assert
            result.Should().BeFalse();
            baseDirectory.Name.Should().BeNull();
            baseDirectory.ModifiedDate.Should().Be(oldModDat);
        }

        [Theory]
        [InlineData("this/is/new/path", "this/is/new/path")]
        [InlineData("this\\is\\new\\path", "this/is/new/path")]
        public void SetPath_NotRootedPath_ChangesNameToFormatedGivenPathModifiedDateToNowAndReturnsTrue(string inputPath, string formatedPath)
        {
            // Arrange
            string oldPath = "this/is/old/path";
            BaseDirectory baseDirectory = new BaseDirectory(1, oldPath, DateTime.Now);
            DateTime oldModDat = baseDirectory.ModifiedDate;

            // Act
            var result = baseDirectory.SetPath(inputPath);

            // Assert
            result.Should().BeTrue();
            baseDirectory.Name.Should().Be(formatedPath);
            baseDirectory.ModifiedDate.Should().BeAfter(oldModDat).And.BeBefore(DateTime.Now);
        }

        [Theory]
        [InlineData("C:/this/is/new/path", "C:/this/is/new/path")]
        [InlineData("C:\\this\\is\\new\\path", "C:/this/is/new/path")]
        public void SetPath_RootedPath_ChangesNameToFormatedGivenPathModifiedDateToNowAndReturnsTrue(string inputPath, string formatedPath)
        {
            // Arrange
            string oldPath = "this/is/old/path";
            BaseDirectory baseDirectory = new BaseDirectory(1, oldPath, DateTime.Now);
            DateTime oldModDat = baseDirectory.ModifiedDate;

            // Act
            var result = baseDirectory.SetPath(inputPath);

            // Assert
            result.Should().BeTrue();
            baseDirectory.Name.Should().Be(formatedPath);
            baseDirectory.ModifiedDate.Should().BeAfter(oldModDat).And.BeBefore(DateTime.Now);
        }
    }
}