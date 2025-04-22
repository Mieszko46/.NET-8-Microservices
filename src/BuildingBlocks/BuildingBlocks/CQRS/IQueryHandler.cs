
using MediatR;

namespace BuildingBlocks.CQRS
{
	public interface IQueryHandler<in TQuerry, TResponse> : IRequestHandler<TQuerry, TResponse>
		where TQuerry : IQuery<TResponse>
		where TResponse : notnull
	{
	}
}
