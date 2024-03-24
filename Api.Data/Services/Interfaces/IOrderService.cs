namespace Api.Data.Services.Interfaces
{
    using Api.Data.Entities;

    public interface IOrderService
    {
        Task<List<Order>> GetOrdersForCompany(int companyId);
    }
}