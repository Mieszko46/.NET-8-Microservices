
using Catalog.API.Rooms.NewFolder;

namespace Catalog.API.Rooms.GetRoomByCategory
{
	//public record GetRoomByCategoryRequest();  - to jest dobra praktyka, że nawet kiedy nie potrzebujemy, to zostawiamy zakomentowane

	public record GetRoomByCategoryResponse(IEnumerable<Room> Rooms);

	public class GetRoomByCategoryEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/rooms/category/{category}",		// używaliśmy już rooms/{id} w innych przykładach, więc aby zachować dobre praktyki
														// dodajemy kolejną warstwę '/category'
				async (string category, ISender sender) =>
				{
					var result = await sender.Send(new GetRoomByCategoryQuery(category));

					var response = result.Adapt<GetRoomByCategoryResponse>();

					return Results.Ok(response);
				}).WithName("Get Room By Category")
				.Produces<GetRoomByCategoryResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.WithSummary("Get Room By Category")
				.WithDescription("Get Room By Category");
		}
	}
}
