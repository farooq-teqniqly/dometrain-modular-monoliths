using Ardalis.GuardClauses;

namespace RiverBooks.Users;

public class CartItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid BookId { get; }
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
    }

    public void UpdateDescription(string newDescription)
    {
        Description = Guard.Against.NullOrWhiteSpace(newDescription);
    }

    public void UpdateUnitPrice(decimal newUnitPrice)
    {
        UnitPrice = Guard.Against.Negative(newUnitPrice);
    }
}
