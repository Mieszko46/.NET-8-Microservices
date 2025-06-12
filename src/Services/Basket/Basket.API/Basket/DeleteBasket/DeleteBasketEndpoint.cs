namespace Basket.API.Basket.DeleteBasket
{
	//public record DeleteBasketRequest() { }

	public record DeleteBasketResponse(bool isSuccess);

	public class DeleteBasketEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
			{
				var result = await sender.Send(new DeleteBasketCommand(userName));

				var response = new DeleteBasketResponse(result.isSuccess);  // for simple mapping, manual is better 
				//var response = result.Adapt<DeleteBasketResponse>();

				return Results.Ok(response);
			})
				.WithName("DeleteBasket")
				.Produces(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status404NotFound)
				.WithSummary("Delete Basket")
				.WithDescription("Delete Basket");
		}
	}
}
