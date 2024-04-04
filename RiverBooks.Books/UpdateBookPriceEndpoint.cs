using FastEndpoints;

namespace RiverBooks.Books;

internal class UpdateBookPriceEndpoint(IBookService bookService) : Endpoint<UpdateBookPriceRequest, BookDto>
{
    public override void Configure()
    {
        Put("/api/books/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateBookPriceRequest req, CancellationToken ct)
    {
        await bookService.UpdateBookPrice(req.Id, req.NewPrice);
        
        var updatedBook = await bookService.GetBookById(req.Id);

        await SendAsync(updatedBook!, cancellation: ct);
    }
}
