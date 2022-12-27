using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract record ProductManipulationDto
    {
        [Required(ErrorMessage = "Product Name is required.")]  //ProductName null olmayacak referans almıs olacak kuralini getirir 
        public String ProductName { get; set; }  //record icin init yazdık. Sadece initialize asamasında nesne new'lendigi zaman deger verebilirsin harici deger vermezsin.

        [Required(ErrorMessage = "Price is required.")]
        public Decimal Price { get; set; }
        public String? ImageUrl { get; set; }
        public String? Description { get; set; }
        public int? CategoryId { get; set; }
    }
}
