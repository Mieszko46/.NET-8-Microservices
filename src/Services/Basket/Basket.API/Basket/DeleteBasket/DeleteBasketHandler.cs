using Basket.API.Basket.StoreBasket;
using FluentValidation;

namespace Basket.API.Basket.DeleteBasket
{
	public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
	public record DeleteBasketResult(bool isSuccess);

	public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
	{
		public DeleteBasketCommandValidator()
		{
			RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
		}
	}

	internal class DeleteBasketCommandHandler(IDocumentSession session) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
	{
		public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
		{
			var shoppingCart = await session.Query<ShoppingCart>()
											.Where(x => x.UserName == command.UserName)
											.ToListAsync();

			if (shoppingCart == null)
			{
				return new DeleteBasketResult(false);
			}

			session.Delete<ShoppingCart>(command.UserName);
			await session.SaveChangesAsync();

			return new DeleteBasketResult(true);
		}
	}
}
