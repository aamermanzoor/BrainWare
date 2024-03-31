namespace Api.Tests.Controllers
{
    using Api.Controllers;
    using Api.Data.Entity;
    using Api.Data.Services.Interfaces;
    using Api.Models;
    using AutoMapper;
    using FizzWare.NBuilder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Moq.AutoMock;

    [TestClass]
    public class OrderControllerTests
    {
        private OrderController _testController;
        private AutoMocker _mocker;

        [TestInitialize]
        public void Initialize()
        {
            _mocker = new AutoMocker();
            _testController = _mocker.CreateInstance<OrderController>();
        }

        [TestMethod]
        public async Task GetOrders_VerifyData()
        {
            // Arrange
            int companyId = 1;
            
            var orders = Builder<Order>.CreateListOfSize(5)
                .Build()
                .ToList();

            var orderModels = Builder<OrderModel>.CreateListOfSize(5)
                .Build()
                .ToList();

            _mocker.GetMock<IOrderService>().Setup(service => service.GetOrdersForCompany(companyId)).ReturnsAsync(orders);
            _mocker.GetMock<IMapper>().Setup(mapper => mapper.Map<List<Order>, IEnumerable<OrderModel>>(orders)).Returns(orderModels);

            // Act
            var response = await _testController.GetOrders(companyId);

            // Assert
            var okResult = response.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.AreEqual(orderModels, okResult.Value);
        }
    }
}
