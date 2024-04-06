using Ardalis.Result;
using MediatR;

namespace RiverBooks.Users;

public record AddItemToCartCommand(Guid BookId, int Quantity, string EmailAddress) : IRequest<Result>;

internal class AddItemToCartHandler(IApplicationUserRepository userRepository) : IRequestHandler<AddItemToCartCommand, Result>
{
    public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserWithCartByEmail(request.EmailAddress);

        if (user is null)
        {
            return Result.Unauthorized();
        }

        //TODO: Get description and price from Books module.
        var newCartItem = new CartItem(request.BookId, "description", request.Quantity, 2m);
        
        user.AddItemToCart(newCartItem);
        await userRepository.SaveChanges();

        return Result.Success();
    }
}
