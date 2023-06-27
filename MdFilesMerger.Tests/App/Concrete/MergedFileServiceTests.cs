using FluentAssertions;
using MdFilesMerger.App.Concrete;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;
using Moq;
using Xunit;

namespace MdFilesMerger.Tests.App.Concrete
{
    public class MergedFileServiceTests
    {
        [Fact]
        public void ReadByUserId_InvalidUserId_ReturnsEmpty()
        {
            // Arrange
            var mergedFileService = new MergedFileService();

            // Act
            var result = mergedFileService.ReadByUserId(-1);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public void ReadByUserId_UnsortedCollection_ReturnsSorted()
        {
            // Arrange
            MergedFileService mergedFileService = new MergedFileService();
            MergedFile[] sortedFiles = new MergedFile[10];
            sortedFiles[0] = new MergedFile() { Name = "Action.md", UserId = 0 };
            sortedFiles[1] = new MergedFile() { Name = "Ex.md", UserId = 0 };
            sortedFiles[2] = new MergedFile() { Name = "Ex0.md", UserId = 0 };
            sortedFiles[3] = new MergedFile() { Name = "Ex3.md", UserId = 0 };
            sortedFiles[4] = new MergedFile() { Name = "Ex11.md", UserId = 0 };
            sortedFiles[5] = new MergedFile() { Name = "Example.md", UserId = 0 };
            sortedFiles[6] = new MergedFile() { Name = "Example1.md", UserId = 0 };
            sortedFiles[7] = new MergedFile() { Name = "Example2.md", UserId = 0 };
            sortedFiles[8] = new MergedFile() { Name = "Example11.md", UserId = 0 };
            sortedFiles[9] = new MergedFile() { Name = "Exemple.md", UserId = 0 };

            // Create unorganized collection
            int[] indexes = new int[10] { 4, 0, 8, 1, 3, 5, 9, 7, 2, 6 };
            foreach (int i in indexes)
            {
                mergedFileService.Create(sortedFiles[i]);
            }

            // Act
            var result = mergedFileService.ReadByUserId(0);

            // Assert
            result.Should().ContainInOrder(sortedFiles);
            result.Should().BeInAscendingOrder();
        }

        [Fact]
        public void ReadByUserId_ValidUserId_ReturnsOnlyItemsWithGivenUserId()
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
        public void UpdateFileName_InvalidId_ReturnsMinusOne()
        {
            // Arrange
            string oldName = "OldName.md", validName = "ValidName.md";
            var file = new Mock<IMergedFile>();
            file.SetupProperty(f => f.Id);
            file.SetupGet(f => f.Name).Returns(oldName);
            file.Setup(f => f.SetFileName(It.IsAny<string>()));

            var mergedFileService = new MergedFileService();
            int id = mergedFileService.Create(file.Object);

            // Act
            var result = mergedFileService.UpdateFileName(id + 1, validName);

            // Assert
            result.Should().Be(-1);
            file.Verify(f => f.SetFileName(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public void UpdateFileName_ValidIdAndInvalidName_RestoresOldNameAndReturnsMinusOne()
        {
            // Arrange
            string oldName = "OldName.md", invalidName = "InvalidName.md";
            var file = new Mock<IMergedFile>();
            file.SetupProperty(f => f.Id);
            file.SetupGet(f => f.Name).Returns(oldName);
            file.SetupSet(f => f.Name = It.IsAny<string>());
            file.Setup(f => f.SetFileName(invalidName)).Returns(false);

            var mergedFileService = new MergedFileService();
            int id = mergedFileService.Create(file.Object);

            // Act
            var result = mergedFileService.UpdateFileName(id, invalidName);

            // Assert
            result.Should().Be(-1);
            file.Verify(f => f.SetFileName(invalidName), Times.Once());
            file.VerifySet(f => f.Name = oldName);
        }

        [Fact]
        public void UpdateFileName_ValidIdAndName_SetsNameToNewAndReturnsId()
        {
            // Arrange
            string oldName = "OldName.md", validName = "ValidName.md";
            var file = new Mock<IMergedFile>();
            file.SetupProperty(f => f.Id);
            file.SetupGet(f => f.Name).Returns(oldName);
            file.SetupSet(f => f.Name = It.IsAny<string>());
            file.Setup(f => f.SetFileName(validName)).Returns(true);

            var mergedFileService = new MergedFileService();
            int id = mergedFileService.Create(file.Object);

            // Act
            var result = mergedFileService.UpdateFileName(id, validName);

            // Assert
            result.Should().Be(id);
            file.Verify(f => f.SetFileName(validName), Times.Once());
            file.VerifySet(f => f.Name = It.IsAny<string>(), Times.Never());
        }

        [Fact]
        public void UpdateNewLineStyle_InvalidId_ReturnsMinusOne()
        {
            // Arrange
            var date = DateTime.Now;
            string oldNewLineStyle = "\n", newNewLineStyle = "\r\n";
            MergedFile mergedFile = new MergedFile() { ModifiedDate = date, NewLineStyle = oldNewLineStyle };

            MergedFileService mergedFileService = new MergedFileService();
            mergedFileService.Create(mergedFile);

            // Act
            var result = mergedFileService.UpdateNewLineStyle(mergedFile.Id + 1, newNewLineStyle);

            // Assert
            result.Should().Be(-1);
            mergedFile.ModifiedDate.Should().Be(date);
            mergedFile.NewLineStyle.Should().Be(oldNewLineStyle);
        }

        [Fact]
        public void UpdateNewLineStyle_ValidId_ChangesNewLineStyleToGivenModifiedDateToNowAndReturnsId()
        {
            // Arrange
            var date = DateTime.Now;
            string oldNewLineStyle = "\n", newNewLineStyle = "\r\n";
            MergedFile mergedFile = new MergedFile() { ModifiedDate = date, NewLineStyle = oldNewLineStyle };

            MergedFileService mergedFileService = new MergedFileService();
            mergedFileService.Create(mergedFile);

            // Act
            var result = mergedFileService.UpdateNewLineStyle(mergedFile.Id, newNewLineStyle);

            // Assert
            result.Should().Be(mergedFile.Id);
            mergedFile.ModifiedDate.Should().BeAfter(date).And.BeBefore(DateTime.Now);
            mergedFile.NewLineStyle.Should().Be(newNewLineStyle);
        }

        [Fact]
        public void UpdateParentDirectory_InvalidId_ReturnsMinusOne()
        {
            // Arrange
            string validPath = "ValidPath";
            var file = new Mock<IMergedFile>();
            file.SetupProperty(f => f.Id);
            file.SetupGet(f => f.Name);
            file.Setup(f => f.SetParentDirectory(It.IsAny<string>()));

            var mergedFileService = new MergedFileService();
            int id = mergedFileService.Create(file.Object);

            // Act
            var result = mergedFileService.UpdateParentDirectory(id + 1, validPath);

            // Assert
            result.Should().Be(-1);
            file.Verify(f => f.SetParentDirectory(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public void UpdateParentDirectory_ValidIdAndInvalidPath_RestoresOldPathAndReturnsMinusOne()
        {
            // Arrange
            string oldPath = "OldPath", invalidPath = "InvalidPath";
            var file = new Mock<IMergedFile>();
            file.SetupProperty(f => f.Id);
            file.SetupGet(f => f.Name).Returns(oldPath);
            file.SetupSet(f => f.Name = It.IsAny<string>());
            file.Setup(f => f.SetParentDirectory(invalidPath)).Returns(false);

            var mergedFileService = new MergedFileService();
            int id = mergedFileService.Create(file.Object);

            // Act
            var result = mergedFileService.UpdateParentDirectory(id, invalidPath);

            // Assert
            result.Should().Be(-1);
            file.Verify(f => f.SetParentDirectory(invalidPath), Times.Once());
            file.VerifySet(f => f.Name = It.IsAny<string>(), Times.Once());
        }

        [Fact]
        public void UpdateParentDirectory_ValidIdAndPath_SetsParentDirectoryAndReturnsId()
        {
            // Arrange
            string oldPath = "OldPath", validPath = "ValidPath";
            var file = new Mock<IMergedFile>();
            file.SetupProperty(f => f.Id);
            file.SetupGet(f => f.Name).Returns(oldPath);
            file.SetupSet(f => f.Name = It.IsAny<string>());
            file.Setup(f => f.SetParentDirectory(validPath)).Returns(true);

            var mergedFileService = new MergedFileService();
            int id = mergedFileService.Create(file.Object);

            // Act
            var result = mergedFileService.UpdateParentDirectory(id, validPath);

            // Assert
            result.Should().Be(id);
            file.Verify(f => f.SetParentDirectory(validPath), Times.Once());
            file.VerifySet(f => f.Name = It.IsAny<string>(), Times.Never());
        }

        [Fact]
        public void UpdateTableOfContents_InvalidId_ReturnsMinusOne()
        {
            // Arrange
            var date = DateTime.Now;
            TableOfContents oldTOC = TableOfContents.None, newTOC = TableOfContents.Hyperlink;
            MergedFile mergedFile = new MergedFile() { ModifiedDate = date, TableOfContents = oldTOC };

            MergedFileService mergedFileService = new MergedFileService();
            mergedFileService.Create(mergedFile);

            // Act
            var result = mergedFileService.UpdateTableOfContents(mergedFile.Id + 1, newTOC);

            // Assert
            result.Should().Be(-1);
            mergedFile.ModifiedDate.Should().Be(date);
            mergedFile.TableOfContents.Should().Be(oldTOC);
        }

        [Fact]
        public void UpdateTableOfContents_ValidId_ChangesTableOfContentsToGivenModifiedDateToNowAndReturnsId()
        {
            // Arrange
            var date = DateTime.Now;
            TableOfContents oldTOC = TableOfContents.None, newTOC = TableOfContents.Hyperlink;
            MergedFile mergedFile = new MergedFile() { ModifiedDate = date, TableOfContents = oldTOC };

            MergedFileService mergedFileService = new MergedFileService();
            mergedFileService.Create(mergedFile);

            // Act
            var result = mergedFileService.UpdateTableOfContents(mergedFile.Id, newTOC);

            // Assert
            result.Should().Be(mergedFile.Id);
            mergedFile.ModifiedDate.Should().BeAfter(date).And.BeBefore(DateTime.Now);
            mergedFile.TableOfContents.Should().Be(newTOC);
        }

        [Fact]
        public void UpdateTitle_InvalidId_ReturnsMinusOne()
        {
            // Arrange
            var date = DateTime.Now;
            string oldTitle = "Old title", newTitle = "New title";
            MergedFile mergedFile = new MergedFile() { ModifiedDate = date, Title = oldTitle };

            MergedFileService mergedFileService = new MergedFileService();
            mergedFileService.Create(mergedFile);

            // Act
            var result = mergedFileService.UpdateTitle(mergedFile.Id + 1, newTitle);

            // Assert
            result.Should().Be(-1);
            mergedFile.ModifiedDate.Should().Be(date);
            mergedFile.Title.Should().Be(oldTitle);
        }

        [Fact]
        public void UpdateTitle_ValidId_ChangesTitleToGivenModifiedDeteToNowAndReturnsId()
        {
            // Arrange
            var date = DateTime.Now;
            string oldTitle = "Old title", newTitle = "New title";
            MergedFile mergedFile = new MergedFile() { ModifiedDate = date, Title = oldTitle };

            MergedFileService mergedFileService = new MergedFileService();
            mergedFileService.Create(mergedFile);

            // Act
            var result = mergedFileService.UpdateTitle(mergedFile.Id, newTitle);

            // Assert
            result.Should().Be(mergedFile.Id);
            mergedFile.ModifiedDate.Should().BeAfter(date).And.BeBefore(DateTime.Now);
            mergedFile.Title.Should().Be(newTitle);
        }

        [Fact]
        public void UpdateTOCHeader_InvalidId_ReturnsMinusOne()
        {
            // Arrange
            var date = DateTime.Now;
            string oldHeader = "## Old Header";
            string newHeader = "## New Header";
            MergedFile mergedFile = new MergedFile() { ModifiedDate = date, TOCHeader = oldHeader };

            MergedFileService mergedFileService = new MergedFileService();
            mergedFileService.Create(mergedFile);

            // Act
            var result = mergedFileService.UpdateTOCHeader(mergedFile.Id + 1, newHeader);

            // Assert
            result.Should().Be(-1);
            mergedFile.ModifiedDate.Should().Be(date);
            mergedFile.TOCHeader.Should().Be(oldHeader);
        }

        [Theory]
        [InlineData("New Header", "## New Header")]
        [InlineData("# New Header", "# New Header")]
        [InlineData(null, "")]
        [InlineData("", "")]
        public void UpdateTOCHeader_ValidId_ChangesTOCHeaderToNewModifiedDateToNowAndReturnsId(string? givenHeader, string resultHeader)
        {
            // Arrange
            var date = DateTime.Now;
            string oldHeader = "## Old Header";
            MergedFile mergedFile = new MergedFile() { ModifiedDate = date, TOCHeader = oldHeader };

            MergedFileService mergedFileService = new MergedFileService();
            mergedFileService.Create(mergedFile);

            // Act
            var result = mergedFileService.UpdateTOCHeader(mergedFile.Id, givenHeader);

            // Assert
            result.Should().Be(mergedFile.Id);
            mergedFile.ModifiedDate.Should().BeAfter(date).And.BeBefore(DateTime.Now);
            mergedFile.TOCHeader.Should().Be(resultHeader);
        }
    }
}