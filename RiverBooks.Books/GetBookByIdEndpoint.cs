using FastEndpoints;

namespace RiverBooks.Books;

internal class GetBookByIdEndpoint(IBookService bookService) : Endpoint<GetBookByIdRequest, BookDto>
{
    public override void Configure()
    {
        Get("/api/books/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetBookByIdRequest req, CancellationToken ct)
    {
        var book = await bookService.GetBookById(req.Id);

        if (book is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendAsync(book, cancellation: ct);
    }
}
