namespace Api.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public decimal OrderTotal { get; set; }

        public List<OrderProductModel> OrderProducts { get; set; }
    }
}