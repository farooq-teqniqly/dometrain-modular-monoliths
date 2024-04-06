using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;

namespace RiverBooks.Users;

internal class LoginEndpoint(UserManager<ApplicationUser> userManager) : Endpoint<LoginRequest, LoginResponse>
{
    public override void Configure()
    {
        Post("/api/users/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var user = await userManager.FindByEmailAsync(req.Email);

        if (user is null)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var loginSuccess = await userManager.CheckPasswordAsync(user, req.Password);

        if (!loginSuccess)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var jwtSecret = Config["Auth:JwtSecret"]!;

        var token = JwtBearer.CreateToken(opts =>
        {
            opts.SigningKey = jwtSecret;
            opts.User["EmailAddress"] = user.Email!;
        });

        await SendOkAsync(new LoginResponse(user.Email!, token), ct);
    }
}