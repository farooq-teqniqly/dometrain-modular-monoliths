using System.Net;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using RiverBooks.Users;

namespace RiverBooks.Web.Tests;
public class UserApiTests(UserApiTestFixture fixture) : TestBase<UserApiTestFixture>
{
    private readonly dynamic _user = new { Email = "foo@bar.com", Password = "P@ssword1!" };

    [Fact]
    public async Task Can_Login_User()
    {
        var createUserResult = await fixture.Client.POSTAsync<CreateUserEndpoint, CreateUserRequest>(
            new CreateUserRequest(_user.Email, _user.Password));

        createUserResult.StatusCode.Should().Be(HttpStatusCode.Created);

        var (loginResult, loginResponse) = await fixture.Client.POSTAsync<LoginEndpoint, LoginRequest, LoginResponse>(
            new LoginRequest(_user.Email, _user.Password));

        loginResult.StatusCode.Should().Be(HttpStatusCode.OK);

        loginResponse.Email.Should().Be(_user.Email);
        loginResponse.Token.Should().NotBeNullOrWhiteSpace();
    }
}
