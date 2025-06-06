﻿
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

	public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
	{
		public UpdateRoomCommandValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
			RuleFor(x => x.Name).
				NotEmpty().WithMessage("Name is required")
				.Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
			
			RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
		}
	}

	public record UpdateRoomResult(bool isSuccess);

	internal class UpdateRoomCommandHandler(IDocumentSession session) 
		: ICommandHandler<UpdateRoomCommand, UpdateRoomResult>
	{
		public async Task<UpdateRoomResult> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
		{
			var room = await session.LoadAsync<Room>(command.Id, cancellationToken);

            if (room is null)
            {
				throw new RoomNotFoundException(command.Id);
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
