using FluentAssertions;
using MdFilesMerger.App.Abstract;
using MdFilesMerger.App.Common;
using MdFilesMerger.App.Concrete;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;
using Moq;
using Xunit;

namespace MdFilesMerger.Tests.App.Concrete
{
    public class RelativeFileServiceTests
    {
        [Fact]
        public void CanFindNewFiles()
        {
            // Arrange
            var service = new RelativeFileService<RelativeFileMock>(new MainDirectoryService());
            IMainDirectory mainDirectory = new MainDirectory(1, Environment.CurrentDirectory, 1);
            var files = new FileInfo[15];
            var newFiles = new FileInfo[5];
            for (int i = 0; i < 10; i++)
            {
                FileInfo file = new FileInfo("file_" + i + ".md");
                service.Create(new RelativeFileMock(file, mainDirectory));
                files[i] = file;
            }

            for (int i = 0; i < 5; i++)
            {
                FileInfo newFile = new FileInfo("newFile_" + i + ".md");
                newFiles[i] = newFile;
                files[i + 10] = newFile;
            }

            // Act
            var result = service.FindNewFiles(files, mainDirectory);

            //Assert
            result.Should().BeOfType<List<FileInfo>>();
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(5);
            result.Should().Contain(newFiles);
        }

        [Fact]
        public void IsEmptyIfNoNewFiles()
        {
            // Arrange
            var service = new RelativeFileService<RelativeFileMock>(new MainDirectoryService());
            IMainDirectory mainDirectory = new MainDirectory(1, Environment.CurrentDirectory, 1);
            var files = new FileInfo[10];
            for (int i = 0; i < 10; i++)
            {
                FileInfo file = new FileInfo("file_" + i + ".md");
                service.Create(new RelativeFileMock(file, mainDirectory));
                files[i] = file;
            }

            // Act
            var result = service.FindNewFiles(files, mainDirectory);

            //Assert
            result.Should().BeOfType<List<FileInfo>>();
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public void IsEmptyIfEmpty()
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
        public void IsEmptyIfNull()
        {
            // Arrange
            var service = new RelativeFileService<RelativeFileMock>(new MainDirectoryService());
            IMainDirectory mainDirectory = new MainDirectory(1, Environment.CurrentDirectory, 1);

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
        public void IsReturningItemsOnlyWithGivenMainDirId()
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
        public void IsReturningSorted()
        {
            // Arrange
            var service = new RelativeFileService<RelativeFileMock>(new MainDirectoryService());
            RelativeFileMock[] files = new RelativeFileMock[10];
            files[0] = new RelativeFileMock() { Name = "Action.md", MainDirId = 0 };
            files[1] = new RelativeFileMock() { Name = "Ex.md", MainDirId = 0 };
            files[2] = new RelativeFileMock() { Name = "Ex0.md", MainDirId = 0 };
            files[3] = new RelativeFileMock() { Name = "Ex3.md", MainDirId = 0 };
            files[4] = new RelativeFileMock() { Name = "Ex11.md", MainDirId = 0 };
            files[5] = new RelativeFileMock() { Name = "Example.md", MainDirId = 0 };
            files[6] = new RelativeFileMock() { Name = "Example1.md", MainDirId = 0 };
            files[7] = new RelativeFileMock() { Name = "Example2.md", MainDirId = 0 };
            files[8] = new RelativeFileMock() { Name = "Example11.md", MainDirId = 0 };
            files[9] = new RelativeFileMock() { Name = "Exemple.md", MainDirId = 0 };

            int[] indexes = new int[10] { 4, 0, 8, 1, 3, 5, 9, 7, 2, 6 };
            foreach (int i in indexes)
            {
                service.Create(files[i]);
            }

            // Act
            var result = service.ReadByMainDirId(0);

            // Assert
            result.Should().ContainInOrder(files);
            result.Should().BeInAscendingOrder();
        }

        [Fact]
        public void IsReturningEmptyForInvalidMainDirId()
        {
            // Arrange
            var service = new RelativeFileService<RelativeFileMock>(new MainDirectoryService());
            for (int i = 1; i < 10; i++)
            {
                service.Create(new RelativeFileMock() { Name = i.ToString(), MainDirId = i / 2 });
            }

            // Act
            var result = service.ReadByMainDirId(-6);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public void CanUpdatePath()
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

        [Fact]
        public void IsNotUpdatingPathIfNull()
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
        public void IsNotUpdatingPathIfInvalidId()
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
            var result = service.UpdatePath(file.Id + 1, Path.GetFullPath(newPath));

            // Assert
            result.Should().Be(-1);
            file.Name.Should().Be(oldPath);
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