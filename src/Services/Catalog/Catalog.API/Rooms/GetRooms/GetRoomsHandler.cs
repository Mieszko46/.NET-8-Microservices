
namespace Catalog.API.Rooms.GetRooms
{
	public record GetRoomsQuery() : IQuery<GetRoomsResult>;

	public record GetRoomsResult (IEnumerable<Room> Rooms);

	internal class GetRoomsQueryHandler(IDocumentSession session, ILogger<GetRoomsQueryHandler> logger)
		: IQueryHandler<GetRoomsQuery, GetRoomsResult>
	{
		public async Task<GetRoomsResult> Handle(GetRoomsQuery query, CancellationToken cancellationToken)
		{
			logger.LogInformation("GetRoomsQueryHandler.Handle called with {@Query}", query);

			var rooms = await session.Query<Room>().ToListAsync(cancellationToken);

			return new GetRoomsResult(rooms);
		}
	}
}
