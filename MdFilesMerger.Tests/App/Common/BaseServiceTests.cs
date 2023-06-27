using FluentAssertions;
using MdFilesMerger.App.Common;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;
using Xunit;

namespace MdFilesMerger.Tests.App.Common
{
    public class BaseServiceTests
    {
        [Fact]
        public void Create_ContainedItem_DoesNotAddItemToCollection()
        {
            // Arrange
            BaseItem baseItem = new BaseItem(100000);
            var service = new BaseService<IItem>();
            service.Create(baseItem);

            // Act

            var result = service.Create(baseItem);

            // Assert
            result.Should().Be(-1);
            service.ReadAll().Should().HaveCount(1).And.Contain(baseItem);
        }

        [Fact]
        public void Create_NewValidItem_AddsItemToCollection()
        {
            // Arrange
            BaseItem baseItem = new BaseItem(1);
            var service = new BaseService<IItem>();

            // Act
            var result = service.Create(baseItem);

            // Assert
            result.Should().Be(baseItem.Id);
            service.ReadAll().Should().HaveCountGreaterThanOrEqualTo(1).And.Contain(baseItem);
        }

        [Fact]
        public void CreateRange_SetOfNewAndValidItems_AddsAllItemsToCollection()
        {
            // Arrange
            List<IItem> items = new List<IItem>();
            items.Add(new BaseItem(1));
            items.Add(new BaseItem(2));

            var service = new BaseService<IItem>();

            // Act
            var result = service.CreateRange(items);

            // Assert
            result.Should().Be(items.First().Id);
            service.ReadAll().Should().HaveCount(2).And.Contain(items);
        }

        [Fact]
        public void Delete_ContainedItem_RemovesItemFromCollectionAndDecreaseGreaterIds()
        {
            // Arrange
            var service = new BaseService<IItem>();

            IItem itemToDelete = new BaseItem(1);
            service.Create(itemToDelete);

            IItem nextItem = new BaseItem(2);
            service.Create(nextItem);

            // Act
            var result = service.Delete(itemToDelete);

            // Assert
            result.Should().Be(itemToDelete.Id);
            service.ReadAll().Should().HaveCount(1).And.NotContain(itemToDelete);
            service.ReadById(itemToDelete.Id).Should().NotBeNull().And.Be(nextItem);
        }

        [Fact]
        public void Delete_InvalidId_DoesNotRemoveItemFromCollection()
        {
            // Arrange
            IItem item = new BaseItem();

            var service = new BaseService<IItem>();
            service.Create(item);

            // Act
            var result = service.Delete(item.Id + 1);

            // Assert
            result.Should().Be(-1);
            service.ReadAll().Should().HaveCount(1);
        }

        [Fact]
        public void Delete_NotContainedItem_DoesNotRemoveItemFromCollection()
        {
            // Arrange
            IItem item = new BaseItem();
            var service = new BaseService<IItem>();
            service.Create(item);

            // Act
            var result = service.Delete(new BaseItem(item.Id + 1));

            // Assert
            result.Should().Be(-1);
            service.ReadAll().Should().HaveCount(1);
        }

        [Fact]
        public void Delete_ValidId_RemovesItemFromCollectionAndDecreaseGreaterIds()
        {
            // Arrange
            var service = new BaseService<IItem>();

            IItem itemToDelete = new BaseItem(1);
            service.Create(itemToDelete);

            IItem nextItem = new BaseItem(2);
            service.Create(nextItem);

            // Act
            var result = service.Delete(itemToDelete.Id);

            // Assert
            result.Should().Be(itemToDelete.Id);
            service.ReadAll().Should().HaveCount(1).And.NotContain(itemToDelete);
            service.ReadById(itemToDelete.Id).Should().NotBeNull().And.Be(nextItem);
        }

        [Fact]
        public void GetNewId_CollectionIsEmpty_ReturnsOne()
        {
            // Arrange
            var service = new BaseService<IItem>();

            // Act
            var result = service.GetNewId();

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public void GetNewId_CollectionIsNotEmpty_ReturnsNumberOneGreaterThenMaxId()
        {
            // Arrange
            var service = new BaseService<IItem>();
            var item = new BaseItem();
            service.Create(item);

            // Act
            var result = service.GetNewId();

            // Assert
            result.Should().Be(item.Id + 1);
        }

        [Fact]
        public void IsEmpty_CollectionIsEmpty_ReturnsTrue()
        {
            // Arrange
            var service = new BaseService<IItem>();

            // Act
            var result = service.IsEmpty();

            // Assert
            result.Should().Be(true);
        }

        [Fact]
        public void IsEmpty_CollectionIsNotEmpty_ReturnsFalse()
        {
            // Arrange
            var service = new BaseService<IItem>();
            service.Create(new BaseItem());

            // Act
            var result = service.IsEmpty();

            // Assert
            result.Should().Be(false);
        }

        [Fact]
        public void ReadById_InvalidId_ReturnsNull()
        {
            // Arrange
            var item = new BaseItem();
            var service = new BaseService<IItem>();

            foreach (var it in service.ReadAll())
            {
                service.Delete(it);
            }

            // Act
            var resultForEmpty = service.ReadById(1);

            service.Create(item);
            var resultForNotEmpty = service.ReadById(item.Id + 1);

            // Assert
            resultForEmpty.Should().BeNull();
            resultForNotEmpty.Should().BeNull();
        }

        [Fact]
        public void ReadById_ValidId_ReturnsItem()
        {
            // Arrange
            BaseItem item = new BaseItem();
            var service = new BaseService<IItem>();
            service.Create(item);

            // Act
            var result = service.ReadById(item.Id);

            // Assert
            result.Should().BeOfType<BaseItem>();
            result.Should().NotBeNull();
            result.Should().Be(item);
        }

        [Fact]
        public void Update_ContainedItem_DoesNotExchangeItem()
        {
            // Arrange
            User itemToUpdate = new User() { Name = "Item to update" };
            User containedItem = new User() { Name = "Contained item" };
            var service = new BaseService<User>();
            service.Create(itemToUpdate);
            service.Create(containedItem);

            User updatingItem = new User() { Id = itemToUpdate.Id, Name = containedItem.Name };

            // Act
            var result = service.Update(updatingItem);

            // Assert
            result.Should().Be(-1);
            service.ReadById(itemToUpdate.Id).Should().NotBe(updatingItem).And.Be(itemToUpdate);
        }

        [Fact]
        public void Update_ItemWithContainedId_ExchangesItemToNew()
        {
            // Arrange
            BaseItem itemToUpdate = new BaseItem();
            var service = new BaseService<IItem>();
            service.Create(itemToUpdate);

            BaseItem updatedItem = new BaseItem { Id = itemToUpdate.Id };

            // Act
            var result = service.Update(updatedItem);

            // Assert
            result.Should().Be(itemToUpdate.Id);
            service.ReadById(itemToUpdate.Id).Should().NotBe(itemToUpdate).And.Be(updatedItem);
        }

        [Fact]
        public void Update_Null_DoesNotExchangeItem()
        {
            // Arrange
            var service = new BaseService<IItem>();

            BaseItem itemToUpdate = new BaseItem(1) { Name = "Item 1" };
            service.Create(itemToUpdate);

            // Act
            var result = service.Update(null);

            // Assert
            result.Should().Be(-1);
            service.ReadById(itemToUpdate.Id).Should().Be(itemToUpdate);
        }
    }
}