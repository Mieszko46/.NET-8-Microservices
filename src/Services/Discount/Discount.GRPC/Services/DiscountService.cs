using Discount.GRPC.Data;
using Discount.GRPC.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Services
{
	public class DiscountService
		(DiscountContext dbContext, ILogger<DiscountService> logger)
		: DiscountProtoService.DiscountProtoServiceBase
	{
		public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
		{
			var coupon = await dbContext
				.Coupons
				.FirstOrDefaultAsync(x => x.RoomCategory == request.RoomCategory);

			if (coupon is null)
				coupon = new Coupon { RoomCategory = "No discount", Description = "No discount description", Amount = 0 };

			logger.LogInformation("Discount retrieved for RoomCategory : {roomCategory}, Amount : {amount}", coupon.RoomCategory, coupon.Amount);

			var couponModel = coupon.Adapt<CouponModel>();

			return couponModel;
		}

		public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
		{
			var coupon = request.Coupon.Adapt<Coupon>();

			if (coupon is null)
			{
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
			}

			dbContext.Add(coupon);
			await dbContext.SaveChangesAsync();

			logger.LogInformation("Discount created for RoomCategory : {roomCategory}", coupon.RoomCategory);

			return request.Coupon;
		}

		public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
		{
			var coupon = request.Coupon.Adapt<Coupon>();

			if (coupon is null)
			{
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
			}

			dbContext.Update(coupon);
			await dbContext.SaveChangesAsync();

			logger.LogInformation("Discount updated for RoomCategory : {roomCategory}", coupon.RoomCategory);

			return request.Coupon;
		}

		public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
		{
			var coupon = await dbContext
				.Coupons
				.FirstOrDefaultAsync(x => x.RoomCategory == request.RoomCategory);

			if (coupon is null)
			{
				throw new RpcException(new Status(StatusCode.NotFound, $"Discount with RoomCategory={request.RoomCategory} not exists."));
			}

			dbContext.Remove(coupon);
			await dbContext.SaveChangesAsync();

			logger.LogInformation("Discount successfully deleted for RoomCategory : {roomCategory}", coupon.RoomCategory);

			return new DeleteDiscountResponse { Success = true };
		}
	}
}
