using FluentAssertions;
using MdFilesMerger.App.Concrete;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;
using Xunit;

namespace MdFilesMerger.Tests.App.Concrete
{
    public class MenuActionServiceTests
    {
        [Fact]
        public void ReadById_InvalidId_ReturnsNull()
        {
            // Arrange
            MenuActionService menuActionService = new MenuActionService();

            // Act
            var result = menuActionService.ReadById(-1);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void ReadById_ValidId_ReturnsItemWithGivenId()
        {
            // Arrange
            MenuActionService menuActionService = new MenuActionService();

            // Act
            var result = menuActionService.ReadById(1);

            // Assert
            result.Should().BeAssignableTo<IMenuAction>();
            result.Should().NotBeNull();
            result!.Id.Should().Be(1);
        }

        [Theory]
        [InlineData(MenuType.Main)]
        [InlineData(MenuType.MergedFile)]
        [InlineData(MenuType.TableOfContents)]
        public void ReadByMenuType_MenuType_ReturnsOnlyItemsWithGivenMenuType(MenuType menuType)
        {
            // Arrange
            MenuActionService menuActionService = new MenuActionService();

            // Act
            var result = menuActionService.ReadByMenuType(menuType);

            // Assert
            result.Should().BeOfType<List<IMenuAction>>();
            result.Should().AllBeAssignableTo<IMenuAction>();
            result.Should().NotBeNull();
            result.Should().OnlyContain(action => action.Menu == menuType);
        }
    }
}