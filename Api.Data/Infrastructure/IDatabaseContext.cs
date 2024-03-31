namespace Api.Data.Infrastructure
{
    using Api.Data.Entity;
    using Microsoft.EntityFrameworkCore;

    public interface IDatabaseContext
    {
        DbSet<Company> Companies { get; set; }
        DbSet<OrderProduct> OrderProducts { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }
    }
}