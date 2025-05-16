
namespace Catalog.API.Rooms.UpdateRoom
{
	public record UpdateRoomCommand(Room Room) : ICommand<UpdateRoomResult>;

	public record UpdateRoomResult(bool isSuccess);

	internal class UpdateRoomCommandHandler(IDocumentSession session, ILogger<UpdateRoomCommandHandler> logger) 
		: ICommandHandler<UpdateRoomCommand, UpdateRoomResult>
	{
		public async Task<UpdateRoomResult> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
		{
			logger.LogInformation("UpdateRoomCommandHandler.Handle called with {@Command}", command);

			var room = await session.LoadAsync<Room>(command.Room.Id, cancellationToken);

            if (room is null)
            {
				throw new RoomNotFoundException();
            }

			room.Id = command.Room.Id;
			room.Name = command.Room.Name;
			room.Description = command.Room.Description;
			room.Category = command.Room.Category;
			room.ImageFile = command.Room.ImageFile;
			room.Price = command.Room.Price;

			session.Update(room);
			await session.SaveChangesAsync(cancellationToken);

			return new UpdateRoomResult(true);
		}
	}
}
