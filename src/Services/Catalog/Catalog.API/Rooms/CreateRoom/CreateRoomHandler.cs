 namespace Catalog.API.Rooms.CreateRoom
{
	public record CreateRoomCommand(
		string Name,
		List<string> Category,
		string Description,
		string ImageFile,
		decimal Price) : ICommand<CreateRoomResult>;

	public record CreateRoomResult(Guid Id);

	public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
	{
		public CreateRoomCommandValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
			RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
			RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
			RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
		}
	}

	internal class CreateRoomCommandHandler(IDocumentSession session, ILogger<CreateRoomCommandHandler> logger) 
		: ICommandHandler<CreateRoomCommand, CreateRoomResult>
	{
		public async Task<CreateRoomResult> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
		{
			// validate data with fluent validation
			logger.LogInformation("CreateRoomCommandHandler.Handle called with {@command}", command);

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
