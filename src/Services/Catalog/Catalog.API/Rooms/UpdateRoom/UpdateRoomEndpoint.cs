
namespace Catalog.API.Rooms.UpdateRoom
{
	public record UpdateRoomRequest(
		Guid Id,
		string Name,
		List<string> Category,
		string Description,
		string ImageFile,
		decimal Price
	);

	public record UpdateRoomResponse(bool isSuccess);

	public class UpdateRoomEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPut("/rooms",
				async (UpdateRoomRequest request, ISender sender) =>
				{
					var command = request.Adapt<UpdateRoomCommand>();

					var result = await sender.Send(command);

					var response = new UpdateRoomResponse(result.isSuccess);

					return Results.Ok(response);

				})
				.WithName("Update Room")
				.Produces<UpdateRoomResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status404NotFound)
				.WithSummary("Update Room")
				.WithDescription("Update Room");
		}
	}
}
