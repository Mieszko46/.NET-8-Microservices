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

	}
}
