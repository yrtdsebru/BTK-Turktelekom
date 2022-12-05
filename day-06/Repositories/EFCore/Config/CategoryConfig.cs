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
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.HasKey(x => x.CategoryId);   //Primary Key

            builder.Property(x => x.CategoryName).IsRequired(); //String bos olamaz nullable. 

            builder.Property(x => x.Description).HasDefaultValue("No Description");

            builder.HasData(                       //seed data, cekirdek. Bu uygulama basladigi anda eger tabloda veri yoksa bunları ekle ancak yoksa ekleme
                new Category(1,"Teknoloji", "New Technologies"),
                new Category(2, "Ev", "Best Sale"),
                new Category()
                {
                    CategoryId = 3,
                    CategoryName="Sağlık",
                    Description="...."
                }
                );
        }
    }
}
