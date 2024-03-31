using RiverBooks.Books;

namespace RiverBooks.Web.Tests.Fakes;

internal class FakeBookService : IBookService
{
    public Task<IEnumerable<BookDto>> ListBooks()
    {
        return Task.FromResult(new List<BookDto>
        {
            new(Guid.NewGuid(), "Book 1", "Author 1", 1m),
            new(Guid.NewGuid(), "Book 2", "Author 2", 1m),
            new(Guid.NewGuid(), "Book 3", "Author 3", 1m)
        }.AsEnumerable());
    }

    public Task<BookDto> GetBookById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task CreateBook(BookDto newBook)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBook(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateBookPrice(Guid bookId, decimal newPrice)
    {
        throw new NotImplementedException();
    }
}
