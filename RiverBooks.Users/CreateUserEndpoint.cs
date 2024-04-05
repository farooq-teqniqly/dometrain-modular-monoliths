using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastEndpoints;
using FastEndpoints.Security;
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

internal class LoginEndpoint(UserManager<ApplicationUser> userManager) : Endpoint<LoginRequest>
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

        await SendOkAsync(new { user.Email, Token = token }, ct);
    }
}
