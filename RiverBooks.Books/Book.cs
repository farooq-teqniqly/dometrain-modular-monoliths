using Ardalis.GuardClauses;

namespace RiverBooks.Books;

internal class Book
{
    public Guid Id { get; private init; }
    public string Title { get; private init; }
    public string Author { get; private init; }
    public decimal Price { get; private set; }

    internal Book(Guid id, string title, string author, decimal price)
    {
        Id = Guard.Against.Default(id);
        Title = Guard.Against.NullOrWhiteSpace(title);
        Author = Guard.Against.NullOrWhiteSpace(author);
        Price = Guard.Against.Negative(price);
    }

    internal void UpdatePrice(decimal newPrice)
    {
        Price = Guard.Against.Negative(newPrice);
    }
}
