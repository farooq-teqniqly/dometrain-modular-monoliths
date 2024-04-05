namespace RiverBooks.Books;

public record CreateBookRequest(Guid Id, decimal Price)
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
}
