using MdFilesMerger.App.Concrete;
using MdFilesMerger.Domain.Entity;
using FluentAssertions;
using Xunit;
using Moq;
using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.Tests.App.Concrete
{
    public class UserServiceTests
    {
        [Fact]
        public void CanUpdateName()
        {
            // Arrange
            int id = 100000;
            string oldName = "Old name", newName = "New name";
            User user = new User() { Id = id, Name = oldName };

            UserService userService = new UserService();
            userService.Create(user);

            // Act
            var result = userService.UpdateName(user.Id, newName);

            // Assert
            result.Should().Be(user.Id);
            user.Name.Should().Be(newName);
        }

        [Fact]
        public void IsNameNotUpdatedForInvalidNewName()
        {
            // Arrange
            int id = 100000;
            string oldName = "Old name", newName = string.Empty;
            User user = new User() { Id = id, Name = oldName };

            UserService userService = new UserService();
            userService.Create(user);

            // Act
            var result = userService.UpdateName(user.Id, newName);

            // Assert
            result.Should().Be(-1);
            user.Name.Should().Be(oldName);
        }

        [Fact]
        public void IsNameNotUpdatedForInvalidId()
        {
            // Arrange
            int id = 100000;
            string oldName = "Old name", newName = "New name";
            User user = new User() { Id = id, Name = oldName };

            UserService userService = new UserService();
            userService.Create(user);

            // Act
            var result = userService.UpdateName(user.Id + 1, newName);

            // Assert
            result.Should().Be(-1);
            user.Name.Should().Be(oldName);
        }

        [Fact]
        public void CanUpdatePassword()
        {
            // Arrange
            int id = 100000;
            byte[] oldPassword = { 1, 2, 3 };
            string newPassword = "ValidPassword";

            var user = new Mock<IUser>();
            user.Setup(user => user.Id).Returns(id);
            user.SetupGet(user => user.Password).Returns(oldPassword);
            user.SetupSet(user => user.Password = It.IsAny<byte[]>());
            user.Setup(user => user.SetPassword(newPassword)).Returns(true);

            UserService userService = new UserService();
            userService.Create(user.Object);

            // Act
            var result = userService.UpdatePassword(id, newPassword);

            // Assert
            result.Should().Be(id);
            user.Verify(user => user.SetPassword(newPassword), Times.Once());
        }

        [Fact]
        public void IsPasswordNotUpdatedForInvalidNewPassword()
        {
            // Arrange
            int id = 100000;
            byte[] oldPassword = { 1, 2, 3 };
            string newPassword = "InvalidPassword";

            var user = new Mock<IUser>();
            user.Setup(user => user.Id).Returns(id);
            user.SetupGet(user => user.Password).Returns(oldPassword);
            user.SetupSet(user => user.Password = It.IsAny<byte[]>());
            user.Setup(user => user.SetPassword(newPassword)).Returns(false);

            UserService userService = new UserService();
            userService.Create(user.Object);

            // Act
            var result = userService.UpdatePassword(id, newPassword);

            // Assert
            result.Should().Be(-1);
            user.Verify(user => user.SetPassword(newPassword), Times.Once());
        }

        [Fact]
        public void IsPasswordNotUpdatedForInvalidId()
        {
            // Arrange
            int id = 100000;
            byte[] oldPassword = { 1, 2, 3 };
            string newPassword = "ValidPassword";

            var user = new Mock<IUser>();
            user.Setup(user => user.Id).Returns(id);
            user.SetupGet(user => user.Password).Returns(oldPassword);
            user.SetupSet(user => user.Password = It.IsAny<byte[]>());
            user.Setup(user => user.SetPassword(newPassword)).Returns(true);

            UserService userService = new UserService();
            userService.Create(user.Object);

            // Act
            var result = userService.UpdatePassword(-1000, newPassword);

            // Assert
            result.Should().Be(-1);
            user.Verify(user => user.SetPassword(newPassword), Times.Never());
        }
    }
}