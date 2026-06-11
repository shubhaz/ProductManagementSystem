using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Application.Interfaces.Repositories;
using ProductManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Infrastructure.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _context.Items
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Item?> GetByIdAsync(int id)
        {
            return await _context.Items
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Item item)
        {
            await _context.Items.AddAsync(item);
        }

        public void Update(Item item)
        {
            _context.Items.Update(item);
        }

        public void Delete(Item item)
        {
            _context.Items.Remove(item);
        }
    }
}
