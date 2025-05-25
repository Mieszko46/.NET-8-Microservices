

namespace Catalog.API.Rooms.DeleteRoom
{
	public record DeleteRoomCommand(Guid Id) : ICommand<DeleteRoomResult>;

	public record DeleteRoomResult(bool isSuccess);

	public class DeleteRoomCommandValidator : AbstractValidator<DeleteRoomCommand>
	{
		public DeleteRoomCommandValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
		}
	}

	internal class DeleteRoomCommandHandler(IDocumentSession session, ILogger<DeleteRoomCommandHandler> logger) 
		: ICommandHandler<DeleteRoomCommand, DeleteRoomResult>
	{
		public async Task<DeleteRoomResult> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
		{
			logger.LogInformation("DeleteRoomCommadHandler.Handle called with {@command}", command);

			var room = await session.LoadAsync<Room>(command.Id, cancellationToken) ?? throw new RoomNotFoundException(command.Id);

			session.Delete<Room>(command.Id);
			await session.SaveChangesAsync();

			return new DeleteRoomResult(true);
		}
	}
}
