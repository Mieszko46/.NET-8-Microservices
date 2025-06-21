using Discount.GRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data
{
	public class DiscountContext : DbContext
	{
		public DbSet<Coupon> Coupons { get; set; }

		public DiscountContext(DbContextOptions<DiscountContext> options) 
			: base(options) 
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Coupon>().HasData(
				new Coupon { Id = 1, RoomCategory = "Standard", Description = "Discount for room with standard conditions", Amount = 100},
				new Coupon { Id = 2, RoomCategory = "Deluxe", Description = "Discount for room with deluxe conditions", Amount = 200 }
				);
		}

	}
}
