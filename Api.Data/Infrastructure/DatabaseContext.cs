namespace Api.Data.Infrastructure
{
    using Api.Data.Entity;
    using Api.Data.Infrastructure.Extensions;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {            
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.DisableTableNamePlularlization();
            modelBuilder.DisableCascadeDeletes();
            modelBuilder.ApplyColumnNameConvention();

            // Configure composite primary key for OrderProduct entity
            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });
        }
    }
}
