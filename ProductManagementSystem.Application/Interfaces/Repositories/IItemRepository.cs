using ProductManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Application.Interfaces.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllAsync();

        Task<Item?> GetByIdAsync(int id);

        Task AddAsync(Item item);

        void Update(Item item);

        void Delete(Item item);
    }
}
