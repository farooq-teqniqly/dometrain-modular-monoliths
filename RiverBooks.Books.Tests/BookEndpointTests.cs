using FastEndpoints;
using FluentAssertions;
using NSubstitute;

namespace RiverBooks.Books.Tests;

public class BookEndpointTests
{
    private readonly IBookService _fakeBookService = Substitute.For<IBookService>();

    [Fact]
    public async Task Can_Get_Books()
    {
        _fakeBookService.GetBooksAsync().Returns(new List<BookDto>
        {
            new(Guid.NewGuid(), "Book 1", "Author 1"), new(Guid.NewGuid(), "Book 1", "Author 1")
        });

        var endpoint = Factory.Create<ListBooksEndpoint>(_fakeBookService);
        await endpoint.HandleAsync(CancellationToken.None);

        var response = endpoint.Response;
        response.Books.Count.Should().Be(2);
    }
}
