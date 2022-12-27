using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore.Config;

namespace Repositories.EFCore
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        //constructer'ımız  bunu oluşturunca default olan RepositoryC
        public RepositoryContext(DbContextOptions<RepositoryContext> options) 
            : base(options)    //base'e göndericeğiz
        {
           
        }

        //ctrl+.   generate overriding   onmodelcreating, bir builder var üzerinden kayıt yapıcaz
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //kayıt var mı yokmu bak varsa ekle, modeli create ederken bu config dosyasına bak demek
            modelBuilder.ApplyConfiguration(new ProductConfig());
        }
    }
}
