namespace RiverBooks.Books;

public record CreateBookRequest(Guid Id, string Title, string Author, decimal Price);
public record DeleteBookRequest(Guid Id);
public record GetBookByIdRequest(Guid Id);
public record UpdateBookPriceRequest(Guid Id, decimal NewPrice);
