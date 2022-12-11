using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Config
{
    //bir tipi configure edecegim. Product tipi.
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            //Convention: Ortak akıl der ki.
            //Bir nesnede eğer id diye bir alan varsa IFCore onu otomatik primary key yapar.
            //Classın ismi ile ID birleşmişse de öyle yapar.
            builder.HasKey(x => x.Id);   //Primary Key
            
            builder.Property(x => x.ProductName).IsRequired(); //String bos olamaz nullable. 
            
            builder.Property(x => x.ImageUrl).HasDefaultValue("/images/products/default.jpg");  //Fotolara default deger atiyoruz.

            builder.Property(x => x.Description).HasDefaultValue("...");

            builder.Property(x => x.AtCreated).HasDefaultValueSql("GETDATE()");  //or .HasDefaultValue(DateTime.Now); 'da olabilirdi

            builder.Property(x => x.CategoryId).HasDefaultValue(1); //Default deger verdim.

            builder.HasData(                       //seed data, cekirdek. Bu uygulama basladigi anda eger tabloda veri yoksa bunları ekle ancak yoksa ekleme
                new Product(1,"HP ZBook",17000),
                new Product(2, "Airpods", 3500),
                new Product(3, "JBL", 1000)
                {
                    ImageUrl = "/images/products/jbl.jpg",
                    Description = "JBL kulak üstü kulaklıkları. 16 saat pil ömrü.",
                    CategoryId = 1
                },
                new Product()
                {
                    Id = 4,
                    ProductName = "Samsung Laptop",
                    Price = 15000,
                    ImageUrl = "/images/products/samsung.jpg",
                    Description = "Samsung Laptop Touch your dreams",
                    CategoryId=2
                }
                );
        }
    }
}
