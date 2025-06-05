
using Marten.Pagination;

namespace Catalog.API.Rooms.GetRooms
{
	public record GetRoomsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetRoomsResult>;

	public record GetRoomsResult (IEnumerable<Room> Rooms);

	internal class GetRoomsQueryHandler(IDocumentSession session)
		: IQueryHandler<GetRoomsQuery, GetRoomsResult>
	{
		public async Task<GetRoomsResult> Handle(GetRoomsQuery query, CancellationToken cancellationToken)
		{
			var rooms = await session.Query<Room>()
				.ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

			return new GetRoomsResult(rooms);
		}
	}
}
