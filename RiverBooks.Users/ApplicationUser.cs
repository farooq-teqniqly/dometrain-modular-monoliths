using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

namespace RiverBooks.Users;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    private readonly List<CartItem> _cartItems = new();
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

public class CartItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid BookId { get; private set; }
    public string Description { get; private set; } = String.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    public CartItem(Guid bookId, string description, int quantity, decimal unitPrice)
    {
        BookId = Guard.Against.Default(bookId);
        Description = Guard.Against.NullOrWhiteSpace(description);
        Quantity = Guard.Against.Negative(quantity);
        UnitPrice = Guard.Against.Negative(unitPrice);
    }

    /// <summary>
    /// Constructor for Entity Framework
    /// </summary>
    public CartItem()
    {
        
    }

    internal void UpdateQuantity(int newQuantity)
    {
        Quantity = Guard.Against.Negative(newQuantity);
        Quantity = newQuantity;
    }
}
