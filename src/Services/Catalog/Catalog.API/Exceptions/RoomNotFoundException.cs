namespace Catalog.API.Exceptions
{
	internal class RoomNotFoundException : Exception
	{
		public RoomNotFoundException() : base("Room not found!") { }
	}
}
