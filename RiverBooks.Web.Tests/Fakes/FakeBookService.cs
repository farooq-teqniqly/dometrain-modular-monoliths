using RiverBooks.Books;

namespace RiverBooks.Web.Tests.Fakes;

internal class FakeBookService : IBookService
{
    public Task<IEnumerable<BookDto>> GetBooksAsync()
    {
        return Task.FromResult(new List<BookDto>
        {
            new(Guid.NewGuid(),"Book 1", "Author 1" ),
            new(Guid.NewGuid(),"Book 2", "Author 2" ),
            new(Guid.NewGuid(),"Book 3", "Author 3" )
        }.AsEnumerable());
    }
}