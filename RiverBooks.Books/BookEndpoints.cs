using FastEndpoints;
using Microsoft.AspNetCore.Builder;

namespace RiverBooks.Books
{
    public static class BookEndpoints
    {
        public static void MapBookEndpoints(this WebApplication app)
        {
            app.MapGet("/books", (IBookService service) => service.GetBooksAsync());
        }
    }

    public class GetBooksResponse
    {
        public List<BookDto> Books { get; set; }
    }

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
}
