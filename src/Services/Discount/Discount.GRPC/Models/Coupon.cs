﻿namespace Discount.GRPC.Models
{
	public class Coupon
	{
		public int Id { get; set; }
		public string RoomCategory { get; set; }
		public string Description { get; set; }
		public int Amount { get; set; }
	}
}
