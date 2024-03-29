using FastEndpoints.Testing;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Web.Tests.Fakes;

namespace RiverBooks.Web.Tests;

public class BookApiTestFixture : AppFixture<Program>
{
    protected override void ConfigureServices(IServiceCollection s)
    {
        s.AddScoped(_ => new FakeBookService());
    }
}
