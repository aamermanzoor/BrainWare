namespace Api.Data.Entity
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class OrderProduct
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public Product Product { get; set; }

        public Order Order { get; set; }
    }
}
