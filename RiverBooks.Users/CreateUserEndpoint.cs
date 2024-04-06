using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace RiverBooks.Users;
internal class CreateUserEndpoint(UserManager<ApplicationUser> userManager) : Endpoint<CreateUserRequest>
{
    public override void Configure()
    {
        Post("/api/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        await userManager.CreateAsync(new ApplicationUser { Email = req.Email, UserName = req.Email }, req.Password);
        await SendResultAsync(Results.Created());
    }
}
