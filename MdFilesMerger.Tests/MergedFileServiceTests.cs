using FluentAssertions;
using MdFilesMerger.App.Concrete;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Entity;
using Moq;
using Xunit;

namespace MdFilesMerger.Tests
{
    public class MergedFileServiceTests
    {
        [Fact]
        public void IsReturningItemsOnlyWithGivenUserId()
        {
            // Arrange
            MergedFileService mergedFileService = new MergedFileService();
            MergedFile[] files = new MergedFile[10];
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = new MergedFile() { Name = i.ToString() + ".md", UserId = 0 };
            }
            files[3].UserId = 15;
            files[7].UserId = 10;
            files[9].UserId = 111;
            List<IMergedFile> toSelect = new List<IMergedFile>(files);
            mergedFileService.CreateRange(toSelect);
            List<IMergedFile> toNotSelect = new List<IMergedFile>();
            toNotSelect.Add(files[3]);
            toNotSelect.Add(files[7]);
            toNotSelect.Add(files[9]);
            toSelect.Remove(files[3]);
            toSelect.Remove(files[7]);
            toSelect.Remove(files[9]);

            // Act
            var result = mergedFileService.ReadByUserId(0);

            // Assert
            result.Should().BeOfType<List<IMergedFile>>();
            result.Should().AllBeOfType<MergedFile>();
            result.Should().NotBeNullOrEmpty();
            result.Should().NotContainNulls();
            result.Should().HaveCount(7);
            result.Should().Contain(toSelect);
            result.Should().NotContain(toNotSelect);
        }

        [Fact]
        public void IsReturningSorted()
        {
            // Arrange
            MergedFileService mergedFileService = new MergedFileService();
            MergedFile[] files = new MergedFile[10];
            files[0] = new MergedFile() { Name = "Action.md", UserId = 0 };
            files[1] = new MergedFile() { Name = "Ex.md", UserId = 0 };
            files[2] = new MergedFile() { Name = "Ex0.md", UserId = 0 };
            files[3] = new MergedFile() { Name = "Ex3.md", UserId = 0 };
            files[4] = new MergedFile() { Name = "Ex11.md", UserId = 0 };
            files[5] = new MergedFile() { Name = "Example.md", UserId = 0 };
            files[6] = new MergedFile() { Name = "Example1.md", UserId = 0 };
            files[7] = new MergedFile() { Name = "Example2.md", UserId = 0 };
            files[8] = new MergedFile() { Name = "Example11.md", UserId = 0 };
            files[9] = new MergedFile() { Name = "Exemple.md", UserId = 0 };

            int[] indexes = new int[10] { 4, 0, 8, 1, 3, 5, 9, 7, 2, 6 };
            foreach (int i in indexes)
            {
                mergedFileService.Create(files[i]);
            }

            // Act
            var result = mergedFileService.ReadByUserId(0);

            // Assert
            result.Should().ContainInOrder(files);
            result.Should().BeInAscendingOrder();
        }

        [Fact]
        public void IsReturningEmptyForInvalidUserId()
        {
            // Arrange
            var mergedFileService = new MergedFileService();
            for (int i = 1; i < 10; i++)
            {
                mergedFileService.Create(new MergedFile() { Name = i.ToString(), UserId = i / 2 });
            }

            // Act
            var result = mergedFileService.ReadByUserId(-6);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}