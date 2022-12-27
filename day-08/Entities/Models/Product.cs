using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Product
    {
        //prop
        public int Id { get; set; }  // property default : 0
        public String ProductName { get; set; }  // default : null

        
        public Decimal Price { get; set; }  // default: 0
        public String? ImageUrl { get; set; }       //? koyarsak warning vermez
        public String? Description { get; set; }
        public DateTime AtCreated { get; set; }

        //Navigation Property
        public int? CategoryId { get; set; }   //bu ilişkiyi kurmayı saglamaz sadece productta kategori id leri tutar
        public Category? Category { get; set; }  //bu bir nesnedir ve bunun sayesinde iliski kurarız.

        //ctor, overloading(farklı parametrelerle ,overiting(görevi değiştir)
        public Product()
        {
            ProductName = "";
           
        }

        public Product(int id, string productName, decimal price) : this()
        {
            Id = id;
            ProductName = productName;
            Price = price;
        }
    }
}
