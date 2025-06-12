namespace Basket.API.Models
{
	public class ShoppingCartItem
	{
		public DateTime CheckInDate { get; set; }
		public DateTime CheckOutDate { get; set; }
		public decimal Price { get; set; }
		public Guid RoomId { get; set; }
		public string RoomName { get; set; } = default!;
	}
}
