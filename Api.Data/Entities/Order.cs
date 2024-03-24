namespace Api.Data.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }
    }
}