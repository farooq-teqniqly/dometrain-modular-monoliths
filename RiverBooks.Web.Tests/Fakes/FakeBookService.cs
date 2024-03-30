using RiverBooks.Books;

namespace RiverBooks.Web.Tests.Fakes;

internal class FakeBookService : IBookService
{
    public Task<IEnumerable<BookDto>> GetBooksAsync()
    {
        return Task.FromResult(new List<BookDto>
        {
            new(Guid.NewGuid(), "Book 1", "Author 1", 1m),
            new(Guid.NewGuid(), "Book 2", "Author 2", 1m),
            new(Guid.NewGuid(), "Book 3", "Author 3", 1m)
        }.AsEnumerable());
    }
}
