using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore.Config;
using System.Reflection;

namespace Repositories.EFCore
{
    public class RepositoryContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        //constructer'ımız  bunu oluşturunca default olan RepositoryC
        public RepositoryContext(DbContextOptions<RepositoryContext> options) 
            : base(options)    //base'e göndericeğiz
        {
           
        }

        //ctrl+.   generate overriding   onmodelcreating, bir builder var üzerinden kayıt yapıcaz
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //cagirmazsak primary key hatasi aliriz.
            //modelBuilder.ApplyConfiguration(new ProductConfig());
            //modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
