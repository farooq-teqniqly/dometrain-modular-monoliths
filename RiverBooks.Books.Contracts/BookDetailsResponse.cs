namespace RiverBooks.Books.Contracts;
public record BookDetailsResponse(Guid BookId, string Author, string Title, decimal Price);
