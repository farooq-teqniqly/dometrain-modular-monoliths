using Ardalis.Result;
using MediatR;

namespace RiverBooks.Users;
public record ListCartItemsQuery(string EmailAddress) : IRequest<Result<List<CartItemDto>>>;

internal class ListCartItemsQueryHandler(IApplicationUserRepository userRepository) : IRequestHandler<ListCartItemsQuery, Result<List<CartItemDto>>>
{
    public async Task<Result<List<CartItemDto>>> Handle(ListCartItemsQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserWithCartByEmail(request.EmailAddress);

        if (user is null)
        {
            return Result.Unauthorized();
        }

        return user.CartItems
            .Select(ci => new CartItemDto(ci.Id, ci.BookId, ci.Description, ci.Quantity, ci.UnitPrice))
            .ToList();
    }
}
