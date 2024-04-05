using FastEndpoints;

namespace RiverBooks.Books;

internal class ListBooksEndpoint(IBookService bookService) : EndpointWithoutRequest<GetBooksResponse>
{
    public override void Configure()
    {
        Get("/api/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var books = await bookService.ListBooks();

        await SendAsync(new GetBooksResponse(books.ToList()), cancellation: ct);
    }
}
