namespace Basket.API.Basket.GetBasket
{
	public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

	public record GetBasketResult(ShoppingCart Cart);

	internal class GetBasketQueryHandler(IDocumentSession session) : IQueryHandler<GetBasketQuery, GetBasketResult>
	{
		public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
		{
			var shoopingCart = await session.Query<ShoppingCart>()
											.Where(x => x.UserName == query.UserName)
											.ToListAsync(token: cancellationToken);

			var result = shoopingCart.Adapt<GetBasketResult>();

			return result;
		}
	}
}
