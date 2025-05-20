
namespace Catalog.API.Rooms.UpdateRoom
{
	public record UpdateRoomCommand(
		Guid Id,
		string Name,
		List<string> Category,
		string Description,
		string ImageFile,
		decimal Price
		) : ICommand<UpdateRoomResult>;

	public record UpdateRoomResult(bool isSuccess);

	internal class UpdateRoomCommandHandler(IDocumentSession session, ILogger<UpdateRoomCommandHandler> logger) 
		: ICommandHandler<UpdateRoomCommand, UpdateRoomResult>
	{
		public async Task<UpdateRoomResult> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
		{
			logger.LogInformation("UpdateRoomCommandHandler.Handle called with {@Command}", command);

			var room = await session.LoadAsync<Room>(command.Id, cancellationToken);

            if (room is null)
            {
				throw new RoomNotFoundException();
            }

			room.Id = command.Id;
			room.Name = command.Name;
			room.Description = command.Description;
			room.Category = command.Category;
			room.ImageFile = command.ImageFile;
			room.Price = command.Price;

			session.Update(room);
			await session.SaveChangesAsync(cancellationToken);

			return new UpdateRoomResult(true);
		}
	}
}
