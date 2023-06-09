﻿using FluentAssertions;
using MdFilesMerger.App.Common;
using MdFilesMerger.Domain.Abstract;
using Moq;
using Xunit;

namespace MdFilesMerger.Tests.App.Common
{
    public class BaseDirectoryServiceTests
    {
        [Fact]
        public void UpdatePath_InvalidId_DoesNotTryToSetPath()
        {
            // Arrange
            string oldPath = "OldPath", validPath = "ValidPath";
            var dir = new Mock<IDirectory>();
            dir.SetupProperty(f => f.Id);
            dir.SetupGet(f => f.Name).Returns(oldPath);
            dir.SetupSet(f => f.Name = It.IsAny<string>());
            dir.Setup(f => f.SetPath(validPath)).Returns(true);

            var baseDirectoryService = new BaseDirectoryService<IDirectory>();
            int id = baseDirectoryService.Create(dir.Object);

            // Act
            var result = baseDirectoryService.UpdatePath(id + 1, validPath);

            // Assert
            result.Should().Be(-1);
            dir.Verify(f => f.SetPath(validPath), Times.Never());
            dir.VerifySet(f => f.Name = It.IsAny<string>(), Times.Never());
        }

        [Fact]
        public void UpdatePath_SetInvalidPath_RestoresName()
        {
            // Arrange
            string oldPath = "OldPath", invalidPath = "InvalidPath";
            var dir = new Mock<IDirectory>();
            dir.SetupProperty(f => f.Id);
            dir.SetupGet(f => f.Name).Returns(oldPath);
            dir.SetupSet(f => f.Name = It.IsAny<string>());
            dir.Setup(f => f.SetPath(invalidPath)).Returns(false);

            var baseDirectoryService = new BaseDirectoryService<IDirectory>();
            int id = baseDirectoryService.Create(dir.Object);

            // Act
            var result = baseDirectoryService.UpdatePath(id, invalidPath);

            // Assert
            result.Should().Be(-1);
            dir.Verify(f => f.SetPath(invalidPath), Times.Once());
            dir.VerifySet(f => f.Name = It.IsAny<string>(), Times.Once());
        }

        [Fact]
        public void UpdatePath_ValidPath_DoesNotRestoreName()
        {
            // Arrange
            string oldPath = "OldPath", validPath = "ValidPath";
            var dir = new Mock<IDirectory>();
            dir.SetupProperty(f => f.Id);
            dir.SetupGet(f => f.Name).Returns(oldPath);
            dir.SetupSet(f => f.Name = It.IsAny<string>());
            dir.Setup(f => f.SetPath(validPath)).Returns(true);

            var baseDirectoryService = new BaseDirectoryService<IDirectory>();
            int id = baseDirectoryService.Create(dir.Object);

            // Act
            var result = baseDirectoryService.UpdatePath(id, validPath);

            // Assert
            result.Should().Be(id);
            dir.Verify(f => f.SetPath(validPath), Times.Once());
            dir.VerifySet(f => f.Name = It.IsAny<string>(), Times.Never());
        }
    }
}