namespace Api.Data.Services
{
    using System.Data;
    using Api.Data.Entity;
    using Api.Data.Infrastructure;
    using Api.Data.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class OrderService : IOrderService
    {
        private readonly IDatabaseContext _databaseContext;

        public OrderService(IDatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
        }

        public Task<List<Order>> GetOrdersForCompany(int companyId)
        {
            var orders = this._databaseContext.Orders
                .Include(o => o.Products).ThenInclude(o=> o.Product)
                .Where(o => o.CompanyId == companyId)
                .ToListAsync();

            return orders;
        }
    }
}