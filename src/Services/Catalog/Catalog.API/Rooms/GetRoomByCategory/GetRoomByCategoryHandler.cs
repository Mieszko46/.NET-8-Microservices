using Catalog.API.Rooms.GetRoomById;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Rooms.GetRoomByCategory
{
	public record GetRoomByCategoryQuery(string Category) : IQuery<GetRoomByCategoryResult>;

	public record GetRoomByCategoryResult(IEnumerable<Room> Rooms);

	internal class GetRoomByCategoryQueryHandler(IDocumentSession session) 
		: IQueryHandler<GetRoomByCategoryQuery, GetRoomByCategoryResult>
	{
		public async Task<GetRoomByCategoryResult> Handle(GetRoomByCategoryQuery query, CancellationToken cancellationToken)
		{
			var result = await session.Query<Room>().Where(x => x.Category.Contains(query.Category)).ToListAsync(cancellationToken);

			return new GetRoomByCategoryResult(result);
		}
	}
}
