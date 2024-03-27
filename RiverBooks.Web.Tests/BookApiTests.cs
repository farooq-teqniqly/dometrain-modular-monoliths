using System.Net;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using RiverBooks.Books;

namespace RiverBooks.Web.Tests
{
    public class BookApiTests(BookApiTestFixture fixture): TestBase<BookApiTestFixture>
    {
        [Fact]
        public async Task Can_Get_Books()
        {
            var (result, response) = await fixture.Client.GETAsync<ListBooksEndpoint, GetBooksResponse>();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Books.Count.Should().Be(3);
        }
    }
}