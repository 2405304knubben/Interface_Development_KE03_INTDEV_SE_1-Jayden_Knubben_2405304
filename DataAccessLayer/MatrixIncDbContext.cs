using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class MatrixIncDbContext : DbContext
    {
        public MatrixIncDbContext(DbContextOptions<MatrixIncDbContext> options) : base(options)
        {
        }

        public required DbSet<Customer> Customers { get; set; }
        public required DbSet<Order> Orders { get; set; }
        public required DbSet<Product> Products { get; set; }
        public required DbSet<Part> Parts { get; set; }
        public required DbSet<OrderRegel> OrderRegels { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId).IsRequired();

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Orders)
                .WithMany(o => o.Products);

            modelBuilder.Entity<Part>()
                .HasMany(p => p.Products)
                .WithMany(p => p.Parts);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>().HasKey(o => o.Id);
        }
    }
}
