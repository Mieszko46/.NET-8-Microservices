﻿
using Basket.API.Data;

namespace Basket.API.Basket.StoreBasket
{
	public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

	public record StoreBasketResult(string UserName);

	public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
	{
		public StoreBasketCommandValidator() 
		{
			RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
			RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username is required");
		}
	}

	internal class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
	{
		public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
		{
			await repository.StoreBasket(command.Cart);

			return new StoreBasketResult(command.Cart.UserName);
		}
	}
}
