using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace RiverBooks.Books;

internal class CreateBookEndpoint(IBookService bookService) : Endpoint<CreateBookRequest, BookDto>
{
    public override void Configure()
    {
        Post("/api/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateBookRequest req, CancellationToken ct)
    {
        var createdBookDto = new BookDto(req.Id, req.Title, req.Author, req.Price);

        try
        {
            await bookService.CreateBook(createdBookDto);
        }
        catch (ArgumentException argumentException)
        {
            await SendResultAsync(Results.BadRequest(argumentException.Message));
            return;
        }
       

        await SendCreatedAtAsync<GetBookByIdEndpoint>(new { createdBookDto.Id }, createdBookDto, cancellation: ct);
    }
}
