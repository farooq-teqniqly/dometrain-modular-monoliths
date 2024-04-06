using Ardalis.Result;
using MediatR;
using RiverBooks.Books.Contracts;

namespace RiverBooks.Users;

public record AddItemToCartCommand(Guid BookId, int Quantity, string EmailAddress) : IRequest<Result>;

internal class AddItemToCartHandler(IApplicationUserRepository userRepository, IMediator mediator) : IRequestHandler<AddItemToCartCommand, Result>
{
    public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserWithCartByEmail(request.EmailAddress);

        if (user is null)
        {
            return Result.Unauthorized();
        }

        var query = new BookDetailsQuery(request.BookId);
        var result = await mediator.Send(query, cancellationToken);

        if (result.Status == ResultStatus.NotFound)
        {
            return Result.NotFound();
        }

        var bookDetails = result.Value;

        var newCartItem = new CartItem(
            request.BookId, 
            $"{bookDetails.Title} by {bookDetails.Author}", 
            request.Quantity, 
            bookDetails.Price);
        
        user.AddItemToCart(newCartItem);
        await userRepository.SaveChanges();

        return Result.Success();
    }
}
