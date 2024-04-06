using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;

namespace RiverBooks.Users;

internal class AddCartItemEndpoint(IMediator mediator) : Endpoint<AddCartItemRequest>
{
    public override void Configure()
    {
        Post("/api/cart");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(AddCartItemRequest req, CancellationToken ct)
    {
        var emailAddress = User.FindFirstValue("EmailAddress")!;
        var command = new AddItemToCartCommand(req.BookId, req.Quantity, emailAddress);
        var result = await mediator.Send(command, ct);

        if (result.Status == ResultStatus.Unauthorized)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        await SendOkAsync(ct);
    }
}
