using BuildingBlocks.Exceptions;

namespace Catalog.API.Exceptions
{
	internal class RoomNotFoundException : NotFoundException
	{
		public RoomNotFoundException(Guid id) : base("Room", id) { }
	}
}
