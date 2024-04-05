namespace RiverBooks.Books;

public record CreateBookRequest(Guid Id, string Title, string Author, decimal Price);
