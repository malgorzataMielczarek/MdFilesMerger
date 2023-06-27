using FluentAssertions;
using MdFilesMerger.App.Concrete;
using MdFilesMerger.Domain.Entity;
using Xunit;

namespace MdFilesMerger.Tests.App.Concrete
{
    public class SelectedFileServiceTests
    {
        [Fact]
        public void UpdateTitle_InvalidId_ReturnsMinusOne()
        {
            // Arrange
            var date = DateTime.Now;
            string oldTitle = "Old title", newTitle = "New title";
            SelectedFile selectedFile = new SelectedFile() { ModifiedDate = date, Title = oldTitle };

            SelectedFileService selectedFileService = new SelectedFileService(new MainDirectoryService());
            selectedFileService.Create(selectedFile);

            // Act
            var result = selectedFileService.UpdateTitle(selectedFile.Id + 1, newTitle);

            // Assert
            result.Should().Be(-1);
            selectedFile.ModifiedDate.Should().Be(date);
            selectedFile.Title.Should().Be(oldTitle);
        }

        [Fact]
        public void UpdateTitle_ValidId_ChangesTitleToGivenModifiedDateToNowAndReturnsId()
        {
            // Arrange
            var date = DateTime.Now;
            string oldTitle = "Old title", newTitle = "New title";
            SelectedFile selectedFile = new SelectedFile() { ModifiedDate = date, Title = oldTitle };

            SelectedFileService selectedFileService = new SelectedFileService(new MainDirectoryService());
            selectedFileService.Create(selectedFile);

            // Act
            var result = selectedFileService.UpdateTitle(selectedFile.Id, newTitle);

            // Assert
            result.Should().Be(selectedFile.Id);
            selectedFile.ModifiedDate.Should().BeAfter(date).And.BeBefore(DateTime.Now);
            selectedFile.Title.Should().Be(newTitle);
        }
    }
}