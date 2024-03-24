namespace Api.Tests.Controllers
{
    using Api.Controllers;
    using Api.Data.Entities;
    using Api.Data.Services.Interfaces;
    using Api.Models;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Moq.AutoMock;

    [TestClass]
    public class OrderControllerTests
    {
        private readonly OrderController _testController;
        private readonly AutoMocker _mocker;

        public OrderControllerTests()
        {
            _mocker = new AutoMocker();
            _testController = _mocker.CreateInstance<OrderController>();
        }

        [TestMethod]
        public async Task GetOrders_VerifyData()
        {
            // setup
            // possibly could use test data builder library i.e. NBuilder
            var orders = new List<Order>
            {
                new Order {
                    CompanyName = "Company 1",
                    Description = "Order 1 Description",
                    OrderId = 1,
                    ProductId = 23,
                    ProductName = "Product 23",
                    ProductPrice = 20,
                    Quantity = 34,
                    UnitPrice = 22
                },
                new Order {
                    CompanyName = "Company 1",
                    Description = "Order 1 Description",
                    OrderId = 1,
                    ProductId = 95,
                    ProductName = "Product 95",
                    ProductPrice = 32,
                    Quantity = 4,
                    UnitPrice = 30
                },
                new Order {
                    CompanyName = "Company 1",
                    Description = "Order 2 Description",
                    OrderId = 2,
                    ProductId = 56,
                    ProductName = "Product 56",
                    ProductPrice = 78,
                    Quantity = 2,
                    UnitPrice = 75
                }
            };

            _mocker.GetMock<IOrderService>().Setup(o => o.GetOrdersForCompany(1)).ReturnsAsync(orders);


            // act
            var result = await _testController.GetOrders(1);

            // assert
            var expectedResult = new List<OrderModel>
            {
                new OrderModel
                {
                    CompanyName = "Company 1",
                    Description = "Order 1 Description",
                    OrderId = 1,
                    OrderTotal = 868,
                    OrderProducts = new List<OrderProductModel> { 
                        new OrderProductModel 
                        { 
                            Price = 22,
                            ProductId = 23,
                            Quantity = 34,
                            Product = new ProductModel
                            {
                                Name = "Product 23",
                                Price = 20
                            }
                        },
                        new OrderProductModel
                        {
                            Price = 30,
                            ProductId = 95,
                            Quantity = 4,
                            Product = new ProductModel
                            {
                                Name = "Product 95",
                                Price = 32
                            }
                        }
                    }
                },
                new OrderModel
                {
                    CompanyName = "Company 1",
                    Description = "Order 2 Description",
                    OrderId = 2,
                    OrderTotal = 150,
                    OrderProducts = new List<OrderProductModel> {
                        new OrderProductModel
                        {
                            Price = 75,
                            ProductId = 56,
                            Quantity = 2,
                            Product = new ProductModel
                            {
                                Name = "Product 56",
                                Price = 78
                            }
                        }
                    }
                }
            };

            Assert.IsNotNull(result);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
