using ProductManagementSystem.Application.DTOs.Item;
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
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ItemDto>> GetAllAsync()
        {
            var items = await _unitOfWork.Items.GetAllAsync();

            return items.Select(i => new ItemDto
            {
                Id = i.Id,
                ProductId = i.ProductId,
                Quantity = i.Quantity
            });
        }

        public async Task<ItemDto?> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.Items.GetByIdAsync(id);

            if (item == null)
                throw new NotFoundException($"Item with Id {id} not found.");

            return new ItemDto
            {
                Id = item.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity
            };
        }

        public async Task<int> CreateAsync(CreateItemDto dto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(dto.ProductId);

            if (product == null)
                throw new NotFoundException($"Product with Id {dto.ProductId} not found.");

            var item = new Item
            {
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            };

            await _unitOfWork.Items.AddAsync(item);

            await _unitOfWork.SaveChangesAsync();

            return item.Id;
        }

        public async Task UpdateAsync(int id, UpdateItemDto dto)
        {
            var item = await _unitOfWork.Items.GetByIdAsync(id);

            if (item == null)
                throw new NotFoundException($"Item with Id {id} not found.");

            item.Quantity = dto.Quantity;

            _unitOfWork.Items.Update(item);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _unitOfWork.Items.GetByIdAsync(id);

            if (item == null)
                throw new NotFoundException($"Item with Id {id} not found.");

            _unitOfWork.Items.Delete(item);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
