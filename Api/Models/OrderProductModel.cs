namespace Api.Models
{
    public class OrderProductModel
    {
        public int ProductId { get; set; }

        public ProductModel Product { get; set; }
    
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}