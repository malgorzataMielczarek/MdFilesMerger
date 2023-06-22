using MdFilesMerger.App.Concrete;
using MdFilesMerger.Domain.Entity;
using Xunit;
using FluentAssertions;

namespace MdFilesMerger.Tests
{
    public class SelectedFileServiceTests
    {
        [Fact]
        public void CanUpdateTitle()
        {
            // Arrange
            int id = 100000;
            var date = DateTime.Now.AddDays(-7);
            string oldTitle = "Old title", newTitle = "New title";
            SelectedFile selectedFile = new SelectedFile() { Id = id, ModifiedDate = date, Title = oldTitle };

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