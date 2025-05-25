namespace Catalog.API.Rooms.GetRoomById
{

	public record GetRoomByIdQuery(Guid id) : IQuery<GetRoomByIdResult>;
	
	public record GetRoomByIdResult(Room Room);

	internal class GetRoomByIdHandler(IDocumentSession session, ILogger<GetRoomByIdHandler> logger) 
		: IQueryHandler<GetRoomByIdQuery, GetRoomByIdResult>
	{
		public async Task<GetRoomByIdResult> Handle(GetRoomByIdQuery query, CancellationToken cancellationToken)
		{
			logger.LogInformation("GetRoomByIdQueryHandler.Handle called with {@Query}", query);

			var result = await session.LoadAsync<Room>(query.id, cancellationToken);

			if (result is null)
			{
				throw new RoomNotFoundException(query.id);
			}

			return new GetRoomByIdResult(result);
		}
	}
}
