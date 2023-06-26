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

        [Fact]
        public void NameNotChangedForInvalidValue()
        {
            // Arrange
            int id = 100000;
            string oldName = "OldName.md", invalidName = "InvalidName.md";
            var file = new Mock<IMergedFile>();
            file.SetupGet(f => f.Id).Returns(id);
            file.SetupGet(f => f.Name).Returns(oldName);
            file.SetupSet(f => f.Name = It.IsAny<string>());
            file.SetupSet(f => f.ModifiedDate = It.IsAny<DateTime>());
            file.Setup(f => f.SetFileName(invalidName)).Returns(false);

            var mergedFileService = new MergedFileService();
            mergedFileService.Create(file.Object);

            // Act
            var result = mergedFileService.UpdateFileName(id, invalidName);

            // Assert
            result.Should().Be(-1);
            file.Verify(f => f.SetFileName(invalidName), Times.Once());
            file.VerifySet(f => f.ModifiedDate = It.IsAny<DateTime>(), Times.Never());
            file.VerifySet(f => f.Name = It.IsAny<string>(), Times.Once());
        }

        [Fact]
        public void CanUpdateName()
        {
            // Arrange
            int id = 100000;
            string oldName = "OldName.md", validName = "ValidName.md";
            var file = new Mock<IMergedFile>();
            file.SetupGet(f => f.Id).Returns(id);
            file.SetupGet(f => f.Name).Returns(oldName);
            file.SetupSet(f => f.Name = It.IsAny<string>());
            file.SetupSet(f => f.ModifiedDate = It.IsAny<DateTime>());
            file.Setup(f => f.SetFileName(validName)).Returns(true);

            var mergedFileService = new MergedFileService();
            mergedFileService.Create(file.Object);

            // Act
            var result = mergedFileService.UpdateFileName(id, validName);

            // Assert
            result.Should().Be(id);
            file.Verify(f => f.SetFileName(validName), Times.Once());
            file.VerifySet(f => f.ModifiedDate = It.IsAny<DateTime>(), Times.Once());
            file.VerifySet(f => f.Name = It.IsAny<string>(), Times.Never());
        }

        [Fact]
        public void CanUpdateNewLineStyle()
        {
            // Arrange
            int id = 100000;
            var date = DateTime.Now.AddDays(-7);
            string oldNewLineStyle = "\n", newNewLineStyle = "\r\n";
            MergedFile mergedFile = new MergedFile() { Id = id, ModifiedDate = date, NewLineStyle = oldNewLineStyle };

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
        public void ParentDirectoryNotChangedForInvalidValue()
        {
            // Arrange
            int id = 100000;
            string oldPath = "OldPath", invalidPath = "InvalidPath";
            var file = new Mock<IMergedFile>();
            file.SetupGet(f => f.Id).Returns(id);
            file.SetupGet(f => f.Name).Returns(oldPath);
            file.SetupSet(f => f.Name = It.IsAny<string>());
            file.SetupSet(f => f.ModifiedDate = It.IsAny<DateTime>());
            file.Setup(f => f.SetParentDirectory(invalidPath)).Returns(false);

            var mergedFileService = new MergedFileService();
            mergedFileService.Create(file.Object);

            // Act
            var result = mergedFileService.UpdateParentDirectory(id, invalidPath);

            // Assert
            result.Should().Be(-1);
            file.Verify(f => f.SetParentDirectory(invalidPath), Times.Once());
            file.VerifySet(f => f.ModifiedDate = It.IsAny<DateTime>(), Times.Never());
            file.VerifySet(f => f.Name = It.IsAny<string>(), Times.Once());
        }

        [Fact]
        public void CanUpdateParentDirectory()
        {
            // Arrange
            int id = 100000;
            string oldPath = "OldPath", validPath = "ValidPath";
            var file = new Mock<IMergedFile>();
            file.SetupGet(f => f.Id).Returns(id);
            file.SetupGet(f => f.Name).Returns(oldPath);
            file.SetupSet(f => f.Name = It.IsAny<string>());
            file.SetupSet(f => f.ModifiedDate = It.IsAny<DateTime>());
            file.Setup(f => f.SetParentDirectory(validPath)).Returns(true);

            var mergedFileService = new MergedFileService();
            mergedFileService.Create(file.Object);

            // Act
            var result = mergedFileService.UpdateParentDirectory(id, validPath);

            // Assert
            result.Should().Be(id);
            file.Verify(f => f.SetParentDirectory(validPath), Times.Once());
            file.VerifySet(f => f.ModifiedDate = It.IsAny<DateTime>(), Times.Once());
            file.VerifySet(f => f.Name = It.IsAny<string>(), Times.Never());
        }

        [Fact]
        public void CanUpdateTableOfContents()
        {
            // Arrange
            int id = 100000;
            var date = DateTime.Now.AddDays(-7);
            TableOfContents oldTOC = TableOfContents.None, newTOC = TableOfContents.Hyperlink;
            MergedFile mergedFile = new MergedFile() { Id = id, ModifiedDate = date, TableOfContents = oldTOC };

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
        public void CanUpdateTitle()
        {
            // Arrange
            int id = 100000;
            var date = DateTime.Now.AddDays(-7);
            string oldTitle = "Old title", newTitle = "New title";
            MergedFile mergedFile = new MergedFile() { Id = id, ModifiedDate = date, Title = oldTitle };

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
        public void CanUpdateTOCHeader()
        {
            // Arrange
            int id = 100000;
            var date = DateTime.Now.AddDays(-7);
            string oldHeader = "## Old Header";
            string?[] newHeaders = { "New Header", "# New Header", null, string.Empty };
            string[] results = { "## New Header", "# New Header", string.Empty, string.Empty };
            MergedFile mergedFile = new MergedFile() { Id = id, ModifiedDate = date, TOCHeader = oldHeader };

            MergedFileService mergedFileService = new MergedFileService();
            mergedFileService.Create(mergedFile);

            for (int i = 0; i < newHeaders.Length; i++)
            {
                // Act
                var result = mergedFileService.UpdateTOCHeader(mergedFile.Id, newHeaders[i]);

                // Assert
                result.Should().Be(mergedFile.Id);
                mergedFile.ModifiedDate.Should().BeAfter(date).And.BeBefore(DateTime.Now);
                mergedFile.TOCHeader.Should().Be(results[i]);
            }
        }
    }
}