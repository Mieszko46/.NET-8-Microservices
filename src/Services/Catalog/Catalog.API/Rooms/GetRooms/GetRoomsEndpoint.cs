
using Catalog.API.Rooms.GetRooms;

namespace Catalog.API.Rooms.NewFolder
{
	public record GetRoomsRequest(int? PageNumber = 1, int? PageSize = 10);
	public record GetRoomsResponse(IEnumerable<Room> Rooms);

	public class GetRoomsEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/rooms", 
				async ([AsParameters] GetRoomsRequest request, ISender sender) =>
			{
				var query = request.Adapt<GetRoomsQuery>();

				var results = await sender.Send(query);

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
