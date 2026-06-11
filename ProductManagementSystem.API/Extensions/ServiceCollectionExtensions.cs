using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Application.Interfaces;
using ProductManagementSystem.Application.Interfaces.Repositories;
using ProductManagementSystem.Infrastructure.Data;
using ProductManagementSystem.Infrastructure.Data.Repositories;
using ProductManagementSystem.Application.Interfaces.Services;
using ProductManagementSystem.Application.Services;

namespace ProductManagementSystem.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IItemService, ItemService>();

            return services;
        }
    }
}
