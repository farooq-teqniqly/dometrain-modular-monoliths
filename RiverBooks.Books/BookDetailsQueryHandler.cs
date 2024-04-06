using Ardalis.Result;
using MediatR;
using RiverBooks.Books.Contracts;

namespace RiverBooks.Books;

internal class BookDetailsQueryHandler(IBookService bookService) : IRequestHandler<BookDetailsQuery, Result<BookDetailsResponse>>
{
    public async Task<Result<BookDetailsResponse>> Handle(BookDetailsQuery request, CancellationToken cancellationToken)
    {
        var book = await bookService.GetBookById(request.BookId);

        if (book is null)
        {
            return Result.NotFound();
        }

        return Result.Success(new BookDetailsResponse(book.Id, book.Author, book.Title, book.Price));
    }
}
