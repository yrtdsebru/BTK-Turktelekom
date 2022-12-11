﻿using Microsoft.EntityFrameworkCore;
using Repositories.Contract;
using Repositories.EFCore;
using Services;
using Services.Contracts;

namespace ProductApp.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, //ben IServiceCol.'u genişletecegim adı da services olucak, genişlettigimiz type'ı veriyoruz.
            IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("sqlconnection"),
                prj => prj.MigrationsAssembly("ProductApp")));

        }
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
        }
    }
}
