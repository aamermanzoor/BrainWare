namespace Api.Data.Tests.Services
{
    using Api.Data.Entity;
    using Api.Data.Infrastructure;
    using Api.Data.Services;
    using Api.Data.Tests.Helpers;
    using FizzWare.NBuilder;

    [TestClass]
    public class OrderServiceTests : TestBase
    {
        private OrderService _orderService;        

        [TestInitialize]
        public void Initialize()
        {            
            _orderService = Mocker.CreateInstance<OrderService>();
        }

        [TestMethod]
        public async Task GetOrdersForCompany_Returns_Orders()
        {
            // Arrange
            var company1Id = 1;
            var company2Id = 2;

            var orders = Builder<Order>.CreateListOfSize(2).All().With(o => o.CompanyId, company1Id).Build().ToList();
            orders.AddRange(Builder<Order>.CreateListOfSize(5).All().With(o => o.CompanyId, company2Id).Build().ToList());

            Mocker.GetMock<IDatabaseContext>().Setup(p => p.Orders).Returns(GetMockDbSet(orders));            

            // Act
            var result = await _orderService.GetOrdersForCompany(company1Id);            

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }
    }
}
