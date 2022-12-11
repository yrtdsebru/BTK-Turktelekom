using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Category
    {
        public int CategoryId { get; set; }  

        [Required(ErrorMessage = "Category Name is required.")]
        [MinLength(3,ErrorMessage= "Category Name must consist of at least 3 characters.")]  
        public String? CategoryName { get; set; }  

        //collection navigation property
        public ICollection<Product> Products { get; set; }  //kategori nesnesinden productlara gecmemizi saglayacak. IEFcore kullanarak Code First yaklasimi ile Foreign key
        public String? Description { get; set; }

        public Category()
        {
                    
        }

        public Category(int categoryId, string? categoryName, string? description)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            Description = description;
        }
    }
}
