namespace Catalog.API.Rooms.CreateRoom
{
	public record CreateRoomCommand(
		string Name,
		List<string> Category,
		string Description,
		string ImageFile,
		decimal Price) : ICommand<CreateRoomResult>;

	public record CreateRoomResult(Guid Id);

	internal class CreateRoomCommandHandler(IDocumentSession session) 
		: ICommandHandler<CreateRoomCommand, CreateRoomResult>
	{
		public async Task<CreateRoomResult> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
		{
			// create Room entity from command object
			var room = new Room
			{
				Name = command.Name,
				Category = command.Category,
				Description = command.Description,
				ImageFile = command.ImageFile,
				Price = command.Price,
			};

			// save to database
			session.Store(room);
			await session.SaveChangesAsync(cancellationToken);

			return new CreateRoomResult(room.Id);
		}
	}
}
