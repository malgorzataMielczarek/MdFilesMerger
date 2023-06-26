using FluentAssertions;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;
using Xunit;

namespace MdFilesMerger.Tests.Domain.Common
{
    public class BaseDirectoryTests
    {
        [Fact]
        public void IsComparingCorrectly()
        {
            // Arrange
            BaseDirectory dir10a = new BaseDirectory() { Name = "dir10/dir10a" };
            BaseDirectory dir10 = new BaseDirectory() { Name = "dir10" };
            BaseDirectory dir = new BaseDirectory() { Name = "dir" };
            BaseDirectory dir2 = new BaseDirectory() { Name = "dir2" };
            BaseDirectory dirBis = new BaseDirectory() { Name = "dir" };
            BaseDirectory file10 = new BaseDirectory() { Name = "dir10/file10.md" };
            BaseDirectory file10a = new BaseDirectory() { Name = "dir10/dir10a/file10.md" };

            // Act
            var equal = new int[] { dir.CompareTo(dirBis), dirBis.CompareTo(dir) };
            var first = new int[] { dir.CompareTo(dir10), dir.CompareTo(dir2), dir10.CompareTo(dir10a), dir10.CompareTo(file10), dir10a.CompareTo(file10), file10.CompareTo(file10a), dir2.CompareTo(dir10) };
            var second = new int[] { dir10.CompareTo(dir), dir2.CompareTo(dir), dir10a.CompareTo(dir10), file10.CompareTo(dir10), file10.CompareTo(dir10a), file10a.CompareTo(file10), dir10.CompareTo(dir2) };

            // Assert
            equal.Should().AllBeEquivalentTo(0);
            first.Should().AllSatisfy(x => x.Should().BeLessThan(0));
            second.Should().AllSatisfy(x => x.Should().BeGreaterThan(0));
        }

        [Fact]
        public void CanSetRootedPath()
        {
            // Arrange
            string oldPath = "this/is/old/path", newPath = "C:/this/is/new/path";
            var newPathsFormats = new string[] { newPath, "C:\\this\\is\\new\\path" };
            BaseDirectory baseDirectory = new BaseDirectory(1, oldPath, DateTime.Now.AddDays(-5));

            foreach (var format in newPathsFormats)
            {
                DateTime oldModDat = baseDirectory.ModifiedDate;

                // Act
                var result = baseDirectory.SetPath(format);

                // Assert
                result.Should().BeTrue();
                baseDirectory.Name.Should().Be(newPath);
                baseDirectory.ModifiedDate.Should().BeAfter(oldModDat).And.BeBefore(DateTime.Now);
            }
        }

        [Fact]
        public void CanSetNotRootedPath()
        {
            // Arrange
            string oldPath = "this/is/old/path", newPath = "this/is/new/path";
            var newPathsFormats = new string[] { newPath, "this\\is\\new\\path" };
            BaseDirectory baseDirectory = new BaseDirectory(1, oldPath, DateTime.Now.AddDays(-5));

            foreach (var format in newPathsFormats)
            {
                DateTime oldModDat = baseDirectory.ModifiedDate;

                // Act
                var result = baseDirectory.SetPath(format);

                // Assert
                result.Should().BeTrue();
                baseDirectory.Name.Should().Be(newPath);
                baseDirectory.ModifiedDate.Should().BeAfter(oldModDat).And.BeBefore(DateTime.Now);
            }
        }

        [Fact]
        public void IsPathNullIfInvalid()
        {
            // Arrange
            string oldPath = "this/is/old/path";
            BaseDirectory baseDirectory = new BaseDirectory(1, oldPath, DateTime.Now.AddDays(-5));

            foreach (var newPath in new string[] { "        ", "th*s/*s/inval*d/path", "another>path", "file?", "other|file", "one\tmore\\path", "not\\in:\\root", null })
            {
                DateTime oldModDat = baseDirectory.ModifiedDate;

                // Act
                var result = baseDirectory.SetPath(newPath);

                // Assert
                result.Should().BeFalse();
                baseDirectory.Name.Should().BeNull();
                baseDirectory.ModifiedDate.Should().Be(oldModDat);
            }
        }
    }
}