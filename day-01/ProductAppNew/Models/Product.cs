namespace ProductAppNew.Models
{
    public class Product
    {
        public int Id { get; set; }    //önce prop+tab+tab bir classın en önemli elemanı property //Int16 daha fazla özelliğe sahip int'den //default 0
        public String ProductName { get; set; } // default null, null olduğu için diğerleri gibi değil oluşturup kullanmak gerek.
        public Decimal Price { get; set; } // default 0


        //ctor  constructer
        public Product()
        {

        }

        //5-6-7 seç ctrl+dot'de generate constructer

        public Product(int id, string productName, decimal price)
        {
            Id = id;
            ProductName = productName;
            Price = price;
        }
    }
}

