using FluentAssertions;
using MdFilesMerger.App.Concrete;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Entity;
using Moq;
using Xunit;

namespace MdFilesMerger.Tests.App.Concrete
{
    public class UserServiceTests
    {
        [Fact]
        public void UpdateName_InvalidId_ReturnsMinusOne()
        {
            // Arrange
            string oldName = "Old name", newName = "New name";
            User user = new User() { Name = oldName };

            UserService userService = new UserService();
            userService.Create(user);

            // Act
            var result = userService.UpdateName(user.Id + 1, newName);

            // Assert
            result.Should().Be(-1);
            user.Name.Should().Be(oldName);
        }

        [Fact]
        public void UpdateName_ValidIdAndContainedName_KeepsNameAndReturnsMinusOne()
        {
            // Arrange
            string oldName = "Old name", newName = string.Empty;
            User user = new User() { Name = oldName };

            UserService userService = new UserService();
            userService.Create(user);

            // Act
            var result = userService.UpdateName(user.Id, newName);

            // Assert
            result.Should().Be(-1);
            user.Name.Should().Be(oldName);
        }

        [Fact]
        public void UpdateName_ValidIdAndName_ChangeNameToGivenAndReturnId()
        {
            // Arrange
            string oldName = "Old name", newName = "New name";
            User user = new User() { Name = oldName };

            UserService userService = new UserService();
            userService.Create(user);

            // Act
            var result = userService.UpdateName(user.Id, newName);

            // Assert
            result.Should().Be(user.Id);
            user.Name.Should().Be(newName);
        }

        [Fact]
        public void UpdatePassword_InvalidId_ReturnsMinusOne()
        {
            // Arrange
            byte[] oldPassword = { 1, 2, 3 };
            string newPassword = "ValidPassword";

            var user = new Mock<IUser>();
            user.SetupProperty(user => user.Id);
            user.SetupGet(user => user.Password).Returns(oldPassword);
            user.SetupSet(user => user.Password = It.IsAny<byte[]>());
            user.Setup(user => user.SetPassword(newPassword)).Returns(true);

            UserService userService = new UserService();
            int id = userService.Create(user.Object);

            // Act
            var result = userService.UpdatePassword(id + 1, newPassword);

            // Assert
            result.Should().Be(-1);
            user.Verify(user => user.SetPassword(newPassword), Times.Never());
        }

        [Fact]
        public void UpdatePassword_ValidIdAndInvalidPassword_TriesToSetPasswordAndReturnsMinusOne()
        {
            // Arrange
            byte[] oldPassword = { 1, 2, 3 };
            string newPassword = "InvalidPassword";

            var user = new Mock<IUser>();
            user.SetupProperty(user => user.Id);
            user.SetupGet(user => user.Password).Returns(oldPassword);
            user.SetupSet(user => user.Password = It.IsAny<byte[]>());
            user.Setup(user => user.SetPassword(newPassword)).Returns(false);

            UserService userService = new UserService();
            int id = userService.Create(user.Object);

            // Act
            var result = userService.UpdatePassword(id, newPassword);

            // Assert
            result.Should().Be(-1);
            user.Verify(user => user.SetPassword(newPassword), Times.Once());
        }

        [Fact]
        public void UpdatePassword_ValidIdAndPassword_SetsPasswordAndReturnsId()
        {
            // Arrange
            byte[] oldPassword = { 1, 2, 3 };
            string newPassword = "ValidPassword";

            var user = new Mock<IUser>();
            user.SetupProperty(user => user.Id);
            user.SetupGet(user => user.Password).Returns(oldPassword);
            user.SetupSet(user => user.Password = It.IsAny<byte[]>());
            user.Setup(user => user.SetPassword(newPassword)).Returns(true);

            UserService userService = new UserService();
            int id = userService.Create(user.Object);

            // Act
            var result = userService.UpdatePassword(id, newPassword);

            // Assert
            result.Should().Be(id);
            user.Verify(user => user.SetPassword(newPassword), Times.Once());
        }
    }
}