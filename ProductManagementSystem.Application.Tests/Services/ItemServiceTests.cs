using FluentAssertions;
using Moq;
using ProductManagementSystem.Application.DTOs.Item;
using ProductManagementSystem.Application.Interfaces;
using ProductManagementSystem.Application.Interfaces.Repositories;
using ProductManagementSystem.Application.Services;
using ProductManagementSystem.Domain.Entities;
using ProductManagementSystem.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Application.Tests.Services
{
    public class ItemServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IItemRepository> _itemRepoMock;
        private readonly Mock<IProductRepository> _productRepoMock;

        private readonly ItemService _itemService;

        public ItemServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _itemRepoMock = new Mock<IItemRepository>();
            _productRepoMock = new Mock<IProductRepository>();

            _unitOfWorkMock
                .Setup(x => x.Items)
                .Returns(_itemRepoMock.Object);

            _unitOfWorkMock
                .Setup(x => x.Products)
                .Returns(_productRepoMock.Object);

            _itemService =
                new ItemService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnItem_WhenExists()
        {
            var item = new Item
            {
                Id = 1,
                ProductId = 1,
                Quantity = 5
            };

            _itemRepoMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(item);

            var result =
                await _itemService.GetByIdAsync(1);

            result.Should().NotBeNull();
            result!.Quantity.Should().Be(5);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateItem_WhenProductExists()
        {
            var dto = new CreateItemDto
            {
                ProductId = 1,
                Quantity = 10
            };

            _productRepoMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(new Product
                {
                    Id = 1,
                    ProductName = "Laptop"
                });

            await _itemService.CreateAsync(dto);

            _itemRepoMock.Verify(
                x => x.AddAsync(It.IsAny<Item>()),
                Times.Once);

            _unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenProductNotFound()
        {
            var dto = new CreateItemDto
            {
                ProductId = 100,
                Quantity = 10
            };

            _productRepoMock
                .Setup(x => x.GetByIdAsync(100))
                .ReturnsAsync((Product?)null);

            await Assert.ThrowsAsync<NotFoundException>(
                () => _itemService.CreateAsync(dto));
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteItem()
        {
            var item = new Item
            {
                Id = 1,
                ProductId = 1,
                Quantity = 5
            };

            _itemRepoMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(item);

            await _itemService.DeleteAsync(1);

            _itemRepoMock.Verify(
                x => x.Delete(item),
                Times.Once);

            _unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }
    }
}
