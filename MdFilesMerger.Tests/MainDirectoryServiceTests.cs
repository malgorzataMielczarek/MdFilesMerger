using FluentAssertions;
using MdFilesMerger.App.Concrete;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Entity;
using Xunit;

namespace MdFilesMerger.Tests
{
    public class MainDirectoryServiceTests
    {
        [Fact]
        public void IsReturningItemsOnlyWithGivenMergedFileId()
        {
            // Arrange
            List<IMainDirectory> mainDirectories = new List<IMainDirectory>();
            var dir1 = new MainDirectory(1, "Path1", 1, DateTime.Now);
            var dir2 = new MainDirectory(2, "Path2", 2, DateTime.Now);
            var dir3 = new MainDirectory(3, "Path3", 1, DateTime.Now);
            var dir4 = new MainDirectory(4, "Path4", 1, DateTime.Now);
            var dir5 = new MainDirectory(5, "Path5", 5, DateTime.Now);
            var dir6 = new MainDirectory(6, "Path6", 1, DateTime.Now);
            mainDirectories.Add(dir1);
            mainDirectories.Add(dir2);
            mainDirectories.Add(dir3);
            mainDirectories.Add(dir4);
            mainDirectories.Add(dir5);
            mainDirectories.Add(dir6);

            MainDirectoryService mainDirectoryService = new MainDirectoryService();
            mainDirectoryService.CreateRange(mainDirectories);

            // Act
            var items = mainDirectoryService.ReadByMergedFileId(1);

            // Assert
            items.Should().BeOfType<List<IMainDirectory>>();
            items.Should().AllBeOfType<MainDirectory>();
            items.Should().NotBeNullOrEmpty();
            items.Should().HaveCountGreaterThanOrEqualTo(4);
            items.Should().Contain(dir1);
            items.Should().NotContain(dir2);
            items.Should().Contain(dir3);
            items.Should().Contain(dir4);
            items.Should().NotContain(dir5);
            items.Should().Contain(dir6);
        }

        [Fact]
        public void IsReturningSorted()
        {
            // Arrange
            List<IMainDirectory> mainDirectories = new List<IMainDirectory>();
            var dir1 = new MainDirectory(1, "Path1", 0, DateTime.Now);
            var dir2 = new MainDirectory(2, "Path2", 0, DateTime.Now);
            var dir3 = new MainDirectory(3, "Path3", 0, DateTime.Now);
            var dir4 = new MainDirectory(4, "Path4", 0, DateTime.Now);
            var dir5 = new MainDirectory(5, "Path5", 0, DateTime.Now);
            var dir6 = new MainDirectory(6, "Path6", 0, DateTime.Now);
            mainDirectories.Add(dir6);
            mainDirectories.Add(dir3);
            mainDirectories.Add(dir2);
            mainDirectories.Add(dir1);
            mainDirectories.Add(dir5);
            mainDirectories.Add(dir4);

            MainDirectoryService mainDirectoryService = new MainDirectoryService();
            mainDirectoryService.CreateRange(mainDirectories);

            // Act
            var items = mainDirectoryService.ReadByMergedFileId(0);

            // Assert
            items.Should().StartWith(dir1).And.EndWith(dir6);
            items[0].Should().Be(dir1);
            items[1].Should().Be(dir2);
            items[2].Should().Be(dir3);
            items[3].Should().Be(dir4);
            items[4].Should().Be(dir5);
            items[5].Should().Be(dir6);
            items.Should().BeInAscendingOrder();
        }

        [Fact]
        public void ReturningEmptyListForInvalidId()
        {
            // Arrange
            MainDirectoryService mainDirectoryService = new MainDirectoryService();

            // Act
            var result = mainDirectoryService.FindAllFiles(-1);

            // Assert
            result.Should().BeAssignableTo<IEnumerable<FileInfo>>();
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public void ReturningEmptyListForNull()
        {
            // Arrange
            MainDirectoryService mainDirectoryService = new MainDirectoryService();

            // Act
            var result = mainDirectoryService.FindAllFiles(null);

            // Assert
            result.Should().BeAssignableTo<IEnumerable<FileInfo>>();
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}