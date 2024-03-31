namespace Api.Tests.Mapping.Profiles
{
    using Api.Data.Entity;
    using Api.Mapping.Profiles;
    using Api.Models;
    using AutoMapper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OrderMappingProfileTests
    {
        private IMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrderMappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [TestMethod]
        public void Order_To_OrderModel_Mapping_IsValid()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                Description = "Test Order",
                Products = new List<OrderProduct>
                {
                    new OrderProduct { ProductId = 1, Price = 10, Quantity = 2, Product = new Product { Name = "Product1" } },
                    new OrderProduct { ProductId = 2, Price = 20, Quantity = 3, Product = new Product { Name = "Product2" } }
                }
            };

            // Act
            var orderModel = _mapper.Map<OrderModel>(order);

            // Assert
            Assert.IsNotNull(orderModel);
            Assert.AreEqual(order.OrderId, orderModel.OrderId);
            Assert.AreEqual(order.Description, orderModel.Description);
            Assert.AreEqual(order.Products.Sum(p => p.Quantity * p.Price), orderModel.OrderTotal);
            Assert.AreEqual(order.Products.Count, orderModel.Products.Count);
        }

        [TestMethod]
        public void OrderProduct_To_OrderProductModel_Mapping_IsValid()
        {
            // Arrange
            var orderProduct = new OrderProduct
            {
                ProductId = 1,
                Price = 10,
                Quantity = 2,
                Product = new Product { Name = "Product1" }
            };

            // Act
            var orderProductModel = _mapper.Map<OrderProductModel>(orderProduct);

            // Assert
            Assert.IsNotNull(orderProductModel);
            Assert.AreEqual(orderProduct.ProductId, orderProductModel.ProductId);
            Assert.AreEqual(orderProduct.Price, orderProductModel.Price);
            Assert.AreEqual(orderProduct.Product.Name, orderProductModel.ProductName);
            Assert.AreEqual(orderProduct.Quantity, orderProductModel.Quantity);
        }
    }
}
