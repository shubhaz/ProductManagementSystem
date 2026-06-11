using ProductManagementSystem.Application.DTOs.Product;
using ProductManagementSystem.Application.Interfaces;
using ProductManagementSystem.Application.Interfaces.Services;
using ProductManagementSystem.Domain.Entities;
using ProductManagementSystem.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                CreatedBy = p.CreatedBy,
                CreatedOn = p.CreatedOn
            });
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException($"Product with Id {id} not found.");

            return new ProductDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                CreatedBy = product.CreatedBy,
                CreatedOn = product.CreatedOn
            };
        }

        public async Task<int> CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                CreatedBy = "Admin",
                CreatedOn = DateTime.UtcNow
            };

            await _unitOfWork.Products.AddAsync(product);

            await _unitOfWork.SaveChangesAsync();

            return product.Id;
        }

        public async Task UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException($"Product with Id {id} not found.");

            product.ProductName = dto.ProductName;
            product.ModifiedBy = "Admin";
            product.ModifiedOn = DateTime.UtcNow;

            _unitOfWork.Products.Update(product);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException($"Product with Id {id} not found.");

            _unitOfWork.Products.Delete(product);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
