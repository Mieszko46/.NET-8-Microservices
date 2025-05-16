
using Catalog.API.Rooms.NewFolder;

namespace Catalog.API.Rooms.GetRoomById
{
	//public record GetRoomByIdRequest();

	public record GetRoomByIdResponse(Room Room);

	public class GetRoomByIdEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/rooms/{id}",
				async (Guid id, ISender sender) =>
				{
					var result = await sender.Send(new GetRoomByIdQuery(id));

					var response = result.Adapt<GetRoomByIdResponse>();

					return Results.Ok(response);

				})
				.WithName("GetRoomById")
				.Produces<GetRoomByIdResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.WithSummary("Get Room By Id")
				.WithDescription("Get Room By Id");
		}
	}
}
