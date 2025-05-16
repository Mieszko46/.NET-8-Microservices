
using Catalog.API.Rooms.GetRooms;

namespace Catalog.API.Rooms.NewFolder
{
	//public record GetRoomsRequest();
	public record GetRoomsResponse(IEnumerable<Room> Rooms);

	public class GetRoomsEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/rooms", 
				async (ISender sender) =>
			{
				var results = await sender.Send(new GetRoomsQuery());

				var response = results.Adapt<GetRoomsResponse>();

				return Results.Ok(response);

			})
				.WithName("GetRooms")
				.Produces<GetRoomsResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.WithSummary("Get Rooms")
				.WithDescription("Get Rooms");
		}
	}
}
