using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

namespace RiverBooks.Users;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    private readonly List<CartItem> _cartItems = [];
    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

    public void AddItemToCart(CartItem cartItem)
    {
        Guard.Against.Null(cartItem);

        var existingItem = _cartItems.SingleOrDefault(ci => ci.BookId == cartItem.BookId);

        if (existingItem is null)
        {
            _cartItems.Add(cartItem);
            return;
        }

        existingItem.UpdateQuantity(existingItem.Quantity + cartItem.Quantity);

        //TODO: What to do if other cart item attributes have changed?
    }
}
