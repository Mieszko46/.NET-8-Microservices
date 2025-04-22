
namespace Catalog.API.Rooms.CreateRoom
{
	public record CreateRoomRequest(
		string Name, 
		List<string> Category, 
		string Description, 
		string ImageFile, 
		decimal Price);

	public record CreateRoomResponse(Guid Id);

	public class CreateRoomEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPost("/rooms",
				async (CreateRoomRequest request, ISender sender) =>
			{
				// get the request and map it using Mapster, to the romm command object
				var command = request.Adapt<CreateRoomCommand>();

				// send command using the MediatoR, which will trigger the handler class
				var result = await sender.Send(command);

				// after that we adapt result with Mapster to the CreateRoomResponse
				var response = result.Adapt<CreateRoomResponse>();

				return Results.Created($"/rooms/{response.Id}", response);

			})
				.WithName("CreateRoom")
				.Produces<CreateRoomResponse>(StatusCodes.Status201Created)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.WithSummary("Create Room")
				.WithDescription("Create Room");
		}
	}
}
