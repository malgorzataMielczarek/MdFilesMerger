using FluentAssertions;
using MdFilesMerger.App.Common;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;
using Xunit;

namespace MdFilesMerger.Tests
{
    public class BaseServiceTests
    {
        [Fact]
        public void CanAddItem()
        {
            // Arrange
            BaseItem baseItem = new BaseItem(100000);
            var service = new BaseService<IItem>();

            // Act
            var result = service.Create(baseItem);

            // Assert
            result.Should().Be(baseItem.Id);
            service.ReadAll().Should().HaveCountGreaterThanOrEqualTo(1).And.Contain(baseItem);
        }

        [Fact]
        public void CanNotReaddItem()
        {
            // Arrange
            BaseItem baseItem = new BaseItem(100000);
            var service = new BaseService<IItem>();
            int count = service.ReadAll().Count;
            int result = 0;

            // Act
            for (int i = 0; i < 10; i++)
            {
                result = service.Create(baseItem);
            }

            // Assert
            result.Should().Be(-1);
            service.ReadAll().Should().HaveCountLessThanOrEqualTo(count + 1).And.Contain(baseItem);
        }

        [Fact]
        public void CanAddRange()
        {
            // Arrange
            List<IItem> items = new List<IItem>();
            for (int i = 100000; i < 100010; i++)
            {
                items.Add(new BaseItem(i));
            }

            var service = new BaseService<IItem>();
            int count = service.ReadAll().Count;

            // Act
            var result = service.CreateRange(items);

            // Assert
            result.Should().Be(items.First().Id);
            service.ReadAll().Should().HaveCount(count + 10).And.Contain(items);
        }

        [Fact]
        public void CanRemoveItem()
        {
            // Arrange
            List<IItem> items = new List<IItem>();
            for (int i = 100000; i < 100010; i++)
            {
                items.Add(new BaseItem(i));
            }

            var service = new BaseService<IItem>();
            service.CreateRange(items);
            int count = service.ReadAll().Count;

            // Act
            var result = service.Delete(items[5]);

            // Assert
            result.Should().Be(items[5].Id);
            service.ReadAll().Should().HaveCount(count - 1).And.NotContain(items[5]);
            service.ReadById(items[5].Id).Should().NotBeNull().And.Be(items[6]);
        }

        [Fact]
        public void CanRemoveItemById()
        {
            // Arrange
            List<IItem> items = new List<IItem>();
            for (int i = 100000; i < 100010; i++)
            {
                items.Add(new BaseItem(i));
            }

            var service = new BaseService<IItem>();
            service.CreateRange(items);
            int count = service.ReadAll().Count;

            // Act
            var result = service.Delete(items[5].Id);

            // Assert
            result.Should().Be(items[5].Id);
            service.ReadAll().Should().HaveCount(count - 1).And.NotContain(items[5]);
            service.ReadById(items[5].Id).Should().NotBeNull().And.Be(items[6]);
        }

        [Fact]
        public void IsNotRemovingIfItemNotContained()
        {
            // Arrange
            List<IItem> items = new List<IItem>();
            for (int i = 100000; i < 100010; i++)
            {
                items.Add(new BaseItem(i));
            }

            var service = new BaseService<IItem>();
            service.CreateRange(items);
            int count = service.ReadAll().Count;

            // Act
            var result = service.Delete(new BaseItem(items.Last().Id + 1));

            // Assert
            result.Should().Be(-1);
            service.ReadAll().Should().HaveCount(count);
        }

        [Fact]
        public void IsNotRemovingIfInvalidId()
        {
            // Arrange
            List<IItem> items = new List<IItem>();
            for (int i = 100000; i < 100010; i++)
            {
                items.Add(new BaseItem(i));
            }

            var service = new BaseService<IItem>();
            service.CreateRange(items);
            int count = service.ReadAll().Count;

            // Act
            var result = service.Delete(-500);

            // Assert
            result.Should().Be(-1);
            service.ReadAll().Should().HaveCount(count);
        }

        [Fact]
        public void CanGetNewIdFromEmpty()
        {
            // Arrange
            var service = new BaseService<IItem>();
            var items = service.ReadAll();
            foreach (var item in items)
            {
                service.Delete(item);
            }

            // Act
            var result = service.GetNewId();

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public void CanGetNewId()
        {
            // Arrange
            var service = new BaseService<IItem>();
            var items = service.ReadAll();
            foreach (var item in items)
            {
                service.Delete(item);
            }

            for (int i = 1; i <= 10; i++)
            {
                service.Create(new BaseItem(i));
            }

            // Act
            var result = service.GetNewId();

            // Assert
            result.Should().Be(11);
        }

        [Fact]
        public void IsEmpty()
        {
            // Arrange
            var service = new BaseService<IItem>();
            var items = service.ReadAll();
            foreach (var item in items)
            {
                service.Delete(item);
            }

            // Act
            var result = service.IsEmpty();

            // Assert
            result.Should().Be(true);
        }

        [Fact]
        public void IsNotEmpty()
        {
            // Arrange
            var service = new BaseService<IItem>();
            for (int i = 1; i <= 10; i++)
            {
                service.Create(new BaseItem(i));
            }

            // Act
            var result = service.IsEmpty();

            // Assert
            result.Should().Be(false);
        }

        [Fact]
        public void CanReadById()
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
        public void IsNullForInvalidId()
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
        public void CanUpdate()
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
        public void CanNotUpdateToContainedItem()
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
        public void CanNotUpdateToNull()
        {
            // Arrange
            var service = new BaseService<IItem>();
            foreach (var item in service.ReadAll())
            {
                service.Delete(item);
            }

            BaseItem itemToUpdate = new BaseItem(1) { Name = "Item 1" };
            BaseItem containedItem = new BaseItem(2) { Name = "Item 2" };
            service.Create(itemToUpdate);
            service.Create(containedItem);

            // Act
            var result = service.Update(null);

            // Assert
            result.Should().Be(-1);
            service.ReadById(itemToUpdate.Id).Should().Be(itemToUpdate);
            service.ReadById(containedItem.Id).Should().Be(containedItem);
        }
    }
}