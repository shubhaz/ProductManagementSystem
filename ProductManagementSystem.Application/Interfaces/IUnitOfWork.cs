using ProductManagementSystem.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }

        IItemRepository Items { get; }

        Task<int> SaveChangesAsync();
    }
}
