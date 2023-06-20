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
        public void IgnoredToSelectedFileConversion()
        {
            // Arrange
            DateTime now = DateTime.Now;
            IIgnoredFile file = new IgnoredFile(1, "README.md", 1, now);
            List<IIgnoredFile> files = new List<IIgnoredFile>();
            files.Add(file);
            MainDirectory mainDirectory = new MainDirectory(1, "C:/Users/mielczarek/source/repos/KursZostanProgramistaASPdotNET", 1);
            var mock = new Mock<IMainDirectoryService>();
            mock.Setup(s => s.ReadById(1)).Returns(mainDirectory);

            // Act
            var result = IgnoredFileService.ToSelectedFile(files, mock.Object);

            // Assert
            result.Should().BeOfType<List<SelectedFile>>(); // Assert.IsType<List<SelectedFile>>(result);
            result.Should().AllBeOfType<SelectedFile>();
            result.Should().NotBeNullOrEmpty(); // Assert.NotNull(result); Assert.NotEmpty(result);
            result.Should().NotContainNulls();
            result.Should().HaveCount(1);   // Assert.Single(result);
            result[0].Name.Should().BeEquivalentTo("README.md");    // Assert.Equal("README.md", result[0].Name);
            result[0].MainDirId.Should().Be(1); // Assert.Equal(1, result[0].MainDirId);
            result[0].Title.Should().BeEquivalentTo("Kurs \"Zostañ programist¹ ASP.NET\" - notatki");   // Assert.Equal("Kurs \"Zostañ programist¹ ASP.NET\" - notatki", result[0].Title);
            result[0].ModifiedDate.Should().BeAfter(now).And.BeBefore(DateTime.Now);    // Assert.True(result[0].ModifiedDate > now && result[0].ModifiedDate < DateTime.Now);
        }
    }
}