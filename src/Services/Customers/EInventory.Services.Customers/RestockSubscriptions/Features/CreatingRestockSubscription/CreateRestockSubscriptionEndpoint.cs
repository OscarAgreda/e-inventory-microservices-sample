using Ardalis.GuardClauses;
using Ingredients.Abstractions.CQRS.Commands;
using Ingredients.Abstractions.Web.MinimalApi;
using EInventory.Services.Customers.RestockSubscriptions.Features.DeletingRestockSubscriptionsByTime;
using SerilogTimings;

namespace EInventory.Services.Customers.RestockSubscriptions.Features.CreatingRestockSubscription;

public class CreateRestockSubscriptionEndpoint : IMinimalEndpointConfiguration
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(RestockSubscriptionsConfigs.RestockSubscriptionsUrl, CreateRestockSubscription)
            .AllowAnonymous()
            .WithTags(RestockSubscriptionsConfigs.Tag)
            .Produces<CreateRestockSubscriptionResult>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithName("CreateRestockSubscription")
            .WithDisplayName("Register New RestockSubscription for Customer.");

        return builder;
    }

    private static async Task<IResult> CreateRestockSubscription(
        CreateRestockSubscriptionRequest request,
        ICommandProcessor commandProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var command = new CreateRestockSubscription(request.CustomerId, request.ProductId, request.Email);

        using (Serilog.Context.LogContext.PushProperty("Endpoint", nameof(CreateRestockSubscriptionEndpoint)))
        using (Serilog.Context.LogContext.PushProperty("RestockSubscriptionId", command.Id))
        {
            var result = await commandProcessor.SendAsync(command, cancellationToken);

            return Results.Created(
                $"{RestockSubscriptionsConfigs.RestockSubscriptionsUrl}/{result.RestockSubscription.Id}",
                result);
        }
    }
}
