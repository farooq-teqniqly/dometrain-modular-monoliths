using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastEndpoints.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using RiverBooks.Books;

namespace RiverBooks.Web.Tests
{
    public class BookApiTestFixture: AppFixture<Program>
    {
        protected override void ConfigureServices(IServiceCollection s)
        {
            var bookService = Substitute.For<IBookService>();

            bookService.GetBooksAsync().Returns(new List<BookDto>
            {
                new(Guid.NewGuid(),"Book 1", "Author 1" ),
                new(Guid.NewGuid(),"Book 2", "Author 2" ),
                new(Guid.NewGuid(),"Book 3", "Author 3" )
            });

            s.AddScoped(_ => bookService);
        }
    }
}
