

namespace Catalog.API.Rooms.DeleteRoom
{
	public record DeleteRoomCommand(Guid id) : ICommand<DeleteRoomResult>;

	public record DeleteRoomResult(bool isSuccess);

	internal class DeleteRoomCommandHandler(IDocumentSession session, ILogger<DeleteRoomCommandHandler> logger) 
		: ICommandHandler<DeleteRoomCommand, DeleteRoomResult>
	{
		public async Task<DeleteRoomResult> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
		{
			logger.LogInformation("DeleteRoomCommadHandler.Handle called with {@command}", command);

			var room = await session.LoadAsync<Room>(command.id, cancellationToken) ?? throw new RoomNotFoundException();

			session.Delete<Room>(command.id);
			await session.SaveChangesAsync();

			return new DeleteRoomResult(true);
		}
	}
}
