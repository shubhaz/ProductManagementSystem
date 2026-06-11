using ProductManagementSystem.Application.DTOs.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Application.Interfaces.Services
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetAllAsync();
        Task<ItemDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateItemDto dto);
        Task UpdateAsync(int id, UpdateItemDto dto);
        Task DeleteAsync(int id);
    }
}
