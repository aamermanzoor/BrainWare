namespace Api.Controllers
{
    using Api.Data.Entities;
    using Api.Data.Services.Interfaces;
    using Api.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [ApiController]
    [Route("api")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("order/{id:int}")]
        [ProducesResponseType(typeof(IEnumerable<OrderModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<OrderModel>> GetOrders(int id)
        {
            var data = await _orderService.GetOrdersForCompany(id);

            var result = MapOrderData(data);

            return result;
        }

        private static IEnumerable<OrderModel> MapOrderData(List<Order> data)
        {
            // we can use a mapping framework here i.e. automapper
            var orders = new List<OrderModel>();

            if (data != null)
            {
                var orderGroups = data.GroupBy(x => x.OrderId);

                foreach (var orderGroup in orderGroups)
                {
                    var order = new OrderModel
                    {
                        CompanyName = orderGroup.First().CompanyName,
                        Description = orderGroup.First().Description,
                        OrderId = orderGroup.First().OrderId,
                        OrderProducts = orderGroup.Select(p => new OrderProductModel
                        {
                            ProductId = p.ProductId,
                            Price = p.UnitPrice,
                            Quantity = p.Quantity,
                            Product = new ProductModel
                            {
                                Name = p.ProductName,
                                Price = p.ProductPrice
                            }
                        }).ToList(),
                        OrderTotal = orderGroup.Select(o => o.UnitPrice * o.Quantity).Sum()
                    };

                    orders.Add(order);
                }
            }

            return orders;
        }
    }
}
