namespace Api.Data.Services
{
    using System.Data;
    using Api.Data.Entities;
    using Api.Data.Services.Interfaces;
    using Api.Infrastructure;

    public class OrderService : IOrderService
    {
        private readonly IDatabase _database;

        public OrderService(IDatabase database)
        {
            _database = database;
        }

        public async Task<List<Order>> GetOrdersForCompany(int companyId)
        {
            // Get the orders
            var ordersQuery = $"SELECT company_name, order_id, description, unit_price, product_id, " +
                $"quantity, product_name, product_price FROM OrderView where company_id = {companyId}";

            var orders = new List<Order>();

            using (var orderReader = await _database.ExecuteReader(ordersQuery))
            {
                while (orderReader.Read())
                {
                    var order = (IDataRecord)orderReader;

                    orders.Add(new Order
                    {
                        CompanyName = order.GetString(0),
                        OrderId = order.GetInt32(1),
                        Description = order.GetString(2),
                        UnitPrice = order.GetDecimal(3),
                        ProductId = order.GetInt32(4),
                        Quantity = order.GetInt32(5),
                        ProductName = order.GetString(6),
                        ProductPrice = order.GetDecimal(7)
                    });
                }
            }

            return orders;
        }
    }
}