﻿using System.Net;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using RiverBooks.Books;

namespace RiverBooks.Web.Tests;

public class BookApiTests(BookApiTestFixture fixture) : TestBase<BookApiTestFixture>
{
    [Fact]
    public async Task Can_Get_Books()
    {
        var (result, response) = await fixture.Client.GETAsync<ListBooksEndpoint, GetBooksResponse>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        
        foreach (var seedBook in BookConfiguration.SeedBooks)
        {
            var book = response.Books.Single(b => b.Id == seedBook.Id);
            book.Should().BeEquivalentTo(seedBook);
        }
    }

    [Fact]
    public async Task Can_Get_Book_By_Id()
    {
        var id = new Guid("ec5785b5-ae50-4be4-8f58-35190fcbed9f");

        var (result, response) =
            await fixture.Client.GETAsync<GetBookByIdEndpoint, GetBookByIdRequest, BookDto>(
                new GetBookByIdRequest()
                {
                    Id = id
                });

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().BeEquivalentTo(BookConfiguration.SeedBooks.Single(b => b.Id == id));

    }

    [Fact]
    public async Task Get_Book_By_Id_Returns_Not_Found()
    {
        var id = new Guid("aaa785b5-ae50-4be4-8f58-35190fcbed9f");

        var (result, response) =
            await fixture.Client.GETAsync<GetBookByIdEndpoint, GetBookByIdRequest, BookDto>(
                new GetBookByIdRequest()
                {
                    Id = id
                });

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }

    [Fact]
    public async Task Can_Create_Book()
    {
        var request = new CreateBookRequest { Id = Guid.NewGuid(),  Author = "Stephen King", Title = "Carrie", Price = 5.99m };
        
        var (result, response) =
            await fixture.Client.POSTAsync<CreateBookEndpoint, CreateBookRequest, BookDto>(request);

        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Headers.Location.Should().Be($"/api/books/{response.Id}");

        response.Id.Should().Be(request.Id);
        response.Author.Should().Be(request.Author);
        response.Title.Should().Be(request.Title);
        response.Price.Should().Be(request.Price);
    }

    [Fact]
    public async Task Creating_Book_Without_Id_Returns_Bad_Request()
    {
        var request = new CreateBookRequest { Author = "Stephen King", Title = "Carrie", Price = 5.99m };

        var (result, response) =
            await fixture.Client.POSTAsync<CreateBookEndpoint, CreateBookRequest, BookDto>(request);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
