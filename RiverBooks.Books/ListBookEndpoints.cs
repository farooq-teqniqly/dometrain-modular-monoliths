using FastEndpoints;

namespace RiverBooks.Books;

internal class ListBooksEndpoint(IBookService bookService) : EndpointWithoutRequest<GetBooksResponse>
{
    public override void Configure()
    {
        Get("/api/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken = default)
    {
        var books = await bookService.GetBooksAsync();

        await SendAsync(new GetBooksResponse { Books = books.ToList() }, cancellation: cancellationToken);
    }
}
