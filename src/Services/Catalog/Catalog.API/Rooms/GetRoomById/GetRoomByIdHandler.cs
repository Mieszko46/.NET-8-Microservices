namespace Catalog.API.Rooms.GetRoomById
{

	public record GetRoomByIdQuery(Guid id) : IQuery<GetRoomByIdResult>;
	
	public record GetRoomByIdResult(Room Room);

	internal class GetRoomByIdHandler(IDocumentSession session) 
		: IQueryHandler<GetRoomByIdQuery, GetRoomByIdResult>
	{
		public async Task<GetRoomByIdResult> Handle(GetRoomByIdQuery query, CancellationToken cancellationToken)
		{
			var result = await session.LoadAsync<Room>(query.id, cancellationToken);

			if (result is null)
			{
				throw new RoomNotFoundException(query.id);
			}

			return new GetRoomByIdResult(result);
		}
	}
}
