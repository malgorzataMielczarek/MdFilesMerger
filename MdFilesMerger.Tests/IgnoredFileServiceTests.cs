using FluentAssertions;
using MdFilesMerger.App.Abstract;
using MdFilesMerger.App.Concrete;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Entity;
using Moq;
using Xunit;

namespace MdFilesMerger.Tests
{
    public class IgnoredFileServiceTests
    {
        [Fact]
        public void CanConverseWholeList()
        {
            // Arrange
            string mainDirPath = "ExampleDir";
            MainDirectory mainDirectory = new MainDirectory(1, mainDirPath, 1, DateTime.Now);
            SelectedFile selectedFile = new SelectedFile(1, "Example.md", 1, DateTime.Now, "Title");

            var mock = new Mock<IMainDirectoryService>();
            mock.Setup(s => s.ReadById(1)).Returns(mainDirectory);

            var ignoredMock = new Mock<IIgnoredFile>();
            ignoredMock.Setup(i => i.ToSelectedFile(mainDirPath)).Returns(selectedFile);
            ignoredMock.Setup(i => i.MainDirId).Returns(mainDirectory.Id);
            var file1 = ignoredMock.Object;
            List<IIgnoredFile> files = new List<IIgnoredFile>();
            files.Add(ignoredMock.Object);
            files.Add(ignoredMock.Object);
            files.Add(ignoredMock.Object);

            // Act
            var result = IgnoredFileService.ToSelectedFile(files, mock.Object);

            // Assert
            result.Should().BeOfType<List<SelectedFile>>(); // Assert.IsType<List<SelectedFile>>(result);
            result.Should().AllBeOfType<SelectedFile>();
            result.Should().NotBeNullOrEmpty(); // Assert.NotNull(result); Assert.NotEmpty(result);
            result.Should().NotContainNulls();
            result.Should().HaveSameCount(files);
            result.Should().OnlyContain(item => item == selectedFile);
        }
    }
}