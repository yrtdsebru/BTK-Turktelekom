using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;

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
    }
}
