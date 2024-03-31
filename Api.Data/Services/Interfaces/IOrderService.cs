namespace Api.Data.Services.Interfaces
{
    using Api.Data.Entity;

    public interface IOrderService
    {
        Task<List<Order>> GetOrdersForCompany(int companyId);
    }
}