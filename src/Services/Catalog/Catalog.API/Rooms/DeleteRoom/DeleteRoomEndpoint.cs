
using Catalog.API.Rooms.GetRoomById;

namespace Catalog.API.Rooms.DeleteRoom
{
	//public record DeleteRoomRequest();

	public record DeleteRoomResponse();

	public class DeleteRoomEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapDelete("rooms/{id}",
				async (Guid id, ISender sender) =>
				{
					var result = await sender.Send(new DeleteRoomCommand(id));

					var response = result.Adapt<DeleteRoomResponse>();

					return Results.Ok(response);
				})
				.WithName("DeleteRoom")
				.Produces(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status404NotFound)
				.WithSummary("Delete Room")
				.WithDescription("Delete Room");
		}
	}
}
