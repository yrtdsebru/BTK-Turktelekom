namespace Entities.RequestParameters
{
    public class ProductRequestParameters : RequestParameters
    {
        public uint MinPrice { get; set; } = uint.MinValue;
        public uint MaxPrice { get; set; } = uint.MaxValue;

        public bool? IsValidPrice => MaxPrice > MinPrice; // ? true : false; eklemesek de olur sonucta zaten boolen 
    }

    
}
