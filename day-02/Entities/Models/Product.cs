using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Product
    {
        //prop
        public int Id { get; set; }
        public String ProductName { get; set; }
        public Decimal Price { get; set; }

        //ctor, overloading(farklı parametrelerle ,overiting(görevi değiştir)
        public Product()
        {
            ProductName = "";
        }

        public Product(int id, string productName, decimal price)
        {
            Id = id;
            ProductName = productName;
            Price = price;
        }
    }
}
