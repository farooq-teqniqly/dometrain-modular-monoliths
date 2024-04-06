using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;

namespace RiverBooks.Users;

internal class ListCartItemsEndpoint(IMediator mediator) : EndpointWithoutRequest<ListCartItemsResponse>
{
    public override void Configure()
    {
        Get("/api/cart");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var emailAddress = User.FindFirstValue("EmailAddress")!;
        var query = new ListCartItemsQuery(emailAddress);
        var result = await mediator.Send(query, ct);

        if (result.Status == ResultStatus.Unauthorized)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var response = new ListCartItemsResponse(result.Value);
        await SendOkAsync(response, ct);
    }
}
