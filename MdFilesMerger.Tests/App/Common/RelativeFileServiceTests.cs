using FluentAssertions;
using MdFilesMerger.App.Abstract;
using MdFilesMerger.App.Common;
using MdFilesMerger.App.Concrete;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;
using Moq;
using Xunit;

namespace MdFilesMerger.Tests.App.Common
{
    public class RelativeFileServiceTests
    {
        [Fact]
        public void FindNewFiles_AllFilesAssociatedWithCollectiond_ReturnsEmpty()
        {
            // Arrange
            var service = new RelativeFileService<RelativeFileMock>(new MainDirectoryService());
            IMainDirectory mainDirectory = new MainDirectory(1, Environment.CurrentDirectory, 1);
            var files = new FileInfo[1];
            FileInfo file = new FileInfo("file_1.md");
            service.Create(new RelativeFileMock(file, mainDirectory));
            files[0] = file;

            // Act
            var result = service.FindNewFiles(files, mainDirectory);

            //Assert
            result.Should().BeOfType<List<FileInfo>>();
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public void FindNewFiles_AnyNull_ReturnsEmpty()
        {
            // Arrange
            var service = new RelativeFileService<RelativeFileMock>(new MainDirectoryService());
            IMainDirectory mainDirectory = new MainDirectory();

            // Act
            var result1 = service.FindNewFiles(null, mainDirectory);
            var result2 = service.FindNewFiles(new List<FileInfo>(), null);
            var result3 = service.FindNewFiles(null, null);

            //Assert
            result1.Should().BeOfType<List<FileInfo>>();
            result1.Should().NotBeNull();
            result1.Should().BeEmpty();
            result2.Should().BeOfType<List<FileInfo>>();
            result2.Should().NotBeNull();
            result2.Should().BeEmpty();
            result3.Should().BeOfType<List<FileInfo>>();
            result3.Should().NotBeNull();
            result3.Should().BeEmpty();
        }

        [Fact]
        public void FindNewFiles_EmptyList_ReturnsEmpty()
        {
            // Arrange
            var service = new RelativeFileService<RelativeFileMock>(new MainDirectoryService());
            IMainDirectory mainDirectory = new MainDirectory(1, Environment.CurrentDirectory, 1);

            // Act
            var result = service.FindNewFiles(new List<FileInfo>(), mainDirectory);

            //Assert
            result.Should().BeOfType<List<FileInfo>>();
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public void FindNewFiles_ReturnsFilesNotAssociatedWithCollection()
        {
            // Arrange
            var service = new RelativeFileService<RelativeFileMock>(new MainDirectoryService());
            IMainDirectory mainDirectory = new MainDirectory(1, Environment.CurrentDirectory, 1);
            var files = new FileInfo[2];

            FileInfo file = new FileInfo("file.md");
            service.Create(new RelativeFileMock(file, mainDirectory));
            files[0] = file;

            FileInfo newFile = new FileInfo("newFile.md");
            files[1] = newFile;

            // Act
            var result = service.FindNewFiles(files, mainDirectory);

            //Assert
            result.Should().BeOfType<List<FileInfo>>();
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(1);
            result.Should().Contain(newFile);
        }

        [Fact]
        public void ReadByMainDirId_InvalidMainDirId_ReturnsEmpty()
        {
            // Arrange
            var service = new RelativeFileService<RelativeFileMock>(new MainDirectoryService());
            service.Create(new RelativeFileMock() { Name = "example.md", MainDirId = 1 });

            // Act
            var result = service.ReadByMainDirId(2);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public void ReadByMainDirId_UnsortedCollection_ReturnsSorted()
        {
            // Arrange
            var service = new RelativeFileService<RelativeFileMock>(new MainDirectoryService());
            RelativeFileMock[] sortedCollection = new RelativeFileMock[10];
            sortedCollection[0] = new RelativeFileMock() { Name = "Action.md", MainDirId = 0 };
            sortedCollection[1] = new RelativeFileMock() { Name = "Ex.md", MainDirId = 0 };
            sortedCollection[2] = new RelativeFileMock() { Name = "Ex0.md", MainDirId = 0 };
            sortedCollection[3] = new RelativeFileMock() { Name = "Ex3.md", MainDirId = 0 };
            sortedCollection[4] = new RelativeFileMock() { Name = "Ex11.md", MainDirId = 0 };
            sortedCollection[5] = new RelativeFileMock() { Name = "Example.md", MainDirId = 0 };
            sortedCollection[6] = new RelativeFileMock() { Name = "Example1.md", MainDirId = 0 };
            sortedCollection[7] = new RelativeFileMock() { Name = "Example2.md", MainDirId = 0 };
            sortedCollection[8] = new RelativeFileMock() { Name = "Example11.md", MainDirId = 0 };
            sortedCollection[9] = new RelativeFileMock() { Name = "Exemple.md", MainDirId = 0 };

            // Mix up the collection
            int[] indexes = new int[10] { 4, 0, 8, 1, 3, 5, 9, 7, 2, 6 };
            foreach (int i in indexes)
            {
                service.Create(sortedCollection[i]);
            }

            // Act
            var result = service.ReadByMainDirId(0);

            // Assert
            result.Should().ContainInOrder(sortedCollection);
            result.Should().BeInAscendingOrder();
        }

        [Fact]
        public void ReadByMainDirId_ValidMainDirId_ReturnsOnlyItemsWithGivenMainDirId()
        {
            // Arrange
            var service = new RelativeFileService<RelativeFileMock>(new MainDirectoryService());
            RelativeFileMock[] files = new RelativeFileMock[10];
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = new RelativeFileMock() { Name = i.ToString() + ".md", MainDirId = 0 };
            }
            files[3].MainDirId = 15;
            files[7].MainDirId = 10;
            files[9].MainDirId = 111;
            List<RelativeFileMock> toSelect = new List<RelativeFileMock>(files);
            service.CreateRange(toSelect);
            List<RelativeFileMock> toNotSelect = new List<RelativeFileMock>();
            toNotSelect.Add(files[3]);
            toNotSelect.Add(files[7]);
            toNotSelect.Add(files[9]);
            toSelect.Remove(files[3]);
            toSelect.Remove(files[7]);
            toSelect.Remove(files[9]);

            // Act
            var result = service.ReadByMainDirId(0);

            // Assert
            result.Should().BeOfType<List<RelativeFileMock>>();
            result.Should().AllBeOfType<RelativeFileMock>();
            result.Should().NotBeNullOrEmpty();
            result.Should().NotContainNulls();
            result.Should().HaveCount(7);
            result.Should().Contain(toSelect);
            result.Should().NotContain(toNotSelect);
        }

        [Fact]
        public void UpdatePath_InvalidId_RenturnsMinusOne()
        {
            // Arrange
            IMainDirectory mainDirectory = new MainDirectory(1, Environment.CurrentDirectory, 1);
            var mock = new Mock<IMainDirectoryService>();
            mock.Setup(s => s.ReadById(It.IsAny<int>()));

            var service = new RelativeFileService<RelativeFileMock>(mock.Object);
            var file = new RelativeFileMock();
            service.Create(file);

            // Act
            var result = service.UpdatePath(file.Id + 1, "newPath.md");

            // Assert
            result.Should().Be(-1);
            mock.Verify(s => s.ReadById(It.IsAny<int>()), Times.Never());
        }

        [Fact]
        public void UpdatePath_ValidIdAndNull_KeepsOldPathAndReturnsMinusOne()
        {
            // Arrange
            IMainDirectory mainDirectory = new MainDirectory(1, Environment.CurrentDirectory, 1);
            var mock = new Mock<IMainDirectoryService>();
            mock.Setup(s => s.ReadById(1)).Returns(mainDirectory);

            var service = new RelativeFileService<RelativeFileMock>(mock.Object);
            string oldPath = "oldPath.md";
            var file = new RelativeFileMock(new FileInfo(oldPath), mainDirectory);
            service.Create(file);

            // Act
            var result = service.UpdatePath(file.Id, null);

            // Assert
            result.Should().Be(-1);
            file.Name.Should().Be(oldPath);
        }

        [Fact]
        public void UpdatePath_ValidIdAndPath_SetsNameToNewPathAndReturnsId()
        {
            // Arrange
            IMainDirectory mainDirectory = new MainDirectory(1, Environment.CurrentDirectory, 1);
            var mock = new Mock<IMainDirectoryService>();
            mock.Setup(s => s.ReadById(1)).Returns(mainDirectory);

            var service = new RelativeFileService<RelativeFileMock>(mock.Object);
            string oldPath = "oldPath.md", newPath = Path.Combine("path", "newPath.md");
            var file = new RelativeFileMock(new FileInfo(oldPath), mainDirectory);
            service.Create(file);

            // Act
            var result = service.UpdatePath(file.Id, Path.GetFullPath(newPath));

            // Assert
            result.Should().Be(file.Id);
            file.Name.Should().NotBe(oldPath).And.Be(newPath);
        }

        private class RelativeFileMock : RelativeFile
        {
            public RelativeFileMock() : base()
            {
            }

            public RelativeFileMock(FileInfo file, IMainDirectory mainDirectory) : base(file, mainDirectory)
            {
            }

            public override bool SetPath(string? path, string? mainDirPath)
            {
                if (path != null && mainDirPath != null)
                {
                    Name = Path.GetRelativePath(mainDirPath, path);
                    return true;
                }
                else
                {
                    Name = path;
                    return false;
                }
            }
        }
    }
}