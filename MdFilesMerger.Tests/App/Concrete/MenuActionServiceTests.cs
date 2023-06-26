using FluentAssertions;
using MdFilesMerger.App.Concrete;
using MdFilesMerger.Domain.Abstract;
using Xunit;

namespace MdFilesMerger.Tests.App.Concrete
{
    public class MenuActionServiceTests
    {
        [Fact]
        public void CanReadById()
        {
            // Arrange
            MenuActionService menuActionService = new MenuActionService();

            // Act
            var result = menuActionService.ReadById(1);

            // Assert
            result.Should().BeAssignableTo<IMenuAction>();
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }

        [Fact]
        public void ReturnsNullForInvalidId()
        {
            // Arrange
            MenuActionService menuActionService = new MenuActionService();

            // Act
            var result = menuActionService.ReadById(-15);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void ReturnsOnlyWithSpecifiedMenuType()
        {
            // Arrange
            MenuActionService menuActionService = new MenuActionService();

            // Act
            var result = menuActionService.ReadByMenuType(Domain.Common.MenuType.Main);
            var result1 = menuActionService.ReadByMenuType(Domain.Common.MenuType.MergedFile);
            var result2 = menuActionService.ReadByMenuType(Domain.Common.MenuType.TableOfContents);

            // Assert
            result.Should().BeOfType<List<IMenuAction>>();
            result1.Should().BeOfType<List<IMenuAction>>();
            result2.Should().BeOfType<List<IMenuAction>>();

            result.Should().AllBeAssignableTo<IMenuAction>();
            result1.Should().AllBeAssignableTo<IMenuAction>();
            result2.Should().AllBeAssignableTo<IMenuAction>();

            result.Should().NotBeNull();
            result1.Should().NotBeNull();
            result2.Should().NotBeNull();

            result.Should().OnlyContain(action => action.Menu == Domain.Common.MenuType.Main);
            result1.Should().OnlyContain(action => action.Menu == Domain.Common.MenuType.MergedFile);
            result2.Should().OnlyContain(action => action.Menu == Domain.Common.MenuType.TableOfContents);
        }
    }
}