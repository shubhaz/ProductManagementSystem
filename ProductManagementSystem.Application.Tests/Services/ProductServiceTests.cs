using FluentAssertions;
using Moq;
using ProductManagementSystem.Application.DTOs.Product;
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
    public class ProductServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IProductRepository> _productRepoMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _productRepoMock = new Mock<IProductRepository>();

            _unitOfWorkMock
                .Setup(x => x.Products)
                .Returns(_productRepoMock.Object);

            _productService =
                new ProductService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenExists()
        {
            var product = new Product
            {
                Id = 1,
                ProductName = "Laptop",
                CreatedBy = "Admin",
                CreatedOn = DateTime.UtcNow
            };

            _productRepoMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(product);

            var result =
                await _productService.GetByIdAsync(1);

            result.Should().NotBeNull();
            result!.ProductName.Should().Be("Laptop");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowException_WhenNotFound()
        {
            _productRepoMock
                .Setup(x => x.GetByIdAsync(100))
                .ReturnsAsync((Product?)null);

            await Assert.ThrowsAsync<NotFoundException>(
                () => _productService.GetByIdAsync(100));
        }

        [Fact]
        public async Task CreateAsync_ShouldAddProduct()
        {
            var dto = new CreateProductDto
            {
                ProductName = "Mouse"
            };

            _productRepoMock
                .Setup(x => x.AddAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            await _productService.CreateAsync(dto);

            _productRepoMock.Verify(
                x => x.AddAsync(It.IsAny<Product>()),
                Times.Once);

            _unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteProduct()
        {
            var product = new Product
            {
                Id = 1,
                ProductName = "Laptop"
            };

            _productRepoMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(product);

            await _productService.DeleteAsync(1);

            _productRepoMock.Verify(
                x => x.Delete(product),
                Times.Once);

            _unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }
    }
}
