using MediatR;

namespace Catalog.API.Rooms.CreateRoom
{
	public record CreateRoomCommand(
		string Name,
		List<string> Category,
		string Description,
		string ImageFile,
		decimal Price) : IRequest<CreateRoomResult>;

	public record CreateRoomResult(Guid Id);

	internal class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, CreateRoomResult>
	{
		public Task<CreateRoomResult> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
