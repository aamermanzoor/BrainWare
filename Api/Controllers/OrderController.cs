namespace Api.Controllers
{
    using Api.Data.Entity;
    using Api.Data.Services.Interfaces;
    using Api.Models;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [ApiController]
    [Route("api")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, IMapper mapper, ILogger<OrderController> logger)
        {
            this._orderService = orderService;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet]
        [Route("order/{id:int}")]
        [ProducesResponseType(typeof(IEnumerable<OrderModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrders(int id)
        {
            this._logger.LogInformation("Get orders request received");
            var data = await this._orderService.GetOrdersForCompany(id);

            var result = this._mapper.Map<List<Order>, IEnumerable<OrderModel>>(data);

            return new OkObjectResult(result);
        }
    }
}
