namespace EcommerceAPI.Models
{
    public class Product
    {
        public int ProductId {  get; set; }
        public string? ProductName { get; set; } 
        public int? Price { get; set; }  
        public byte[]? ImageData { get; set; }    

        public string? Thumb {  get; set; }
        public int? Count {  get; set; }
        public string? Color {  get; set; }
        public string? Size {  get; set; }
        public string? Discount { get; set; }
        public int? CurrentPrice { get; set; }
        public string? Punctuation { get; set; }
        public string? Reviews {  get; set; }

    }
}
