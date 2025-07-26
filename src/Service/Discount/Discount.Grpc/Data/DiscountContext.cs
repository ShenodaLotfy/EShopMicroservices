using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
            
        }

        DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                    new Coupon { Id = 1, ProductName = "IPhone", Description = "IPhone Discount", Amount = 100},
                    new Coupon { Id = 2, ProductName = "Samsung", Description = "Samsung Discount", Amount = 100}
                );
        }
    }
}
