using Ardalis.GuardClauses;
using Ingredients.Abstractions.CQRS.Commands;
using Ingredients.Abstractions.CQRS.Events.Internal;
using Ingredients.Core.CQRS.Events.Internal;
using Ingredients.Core.Exception;
using EInventory.Services.Customers.Customers.Exceptions.Application;
using EInventory.Services.Customers.RestockSubscriptions.Models.Write;
using EInventory.Services.Customers.Shared.Data;
using EInventory.Services.Customers.Shared.Extensions;

namespace EInventory.Services.Customers.RestockSubscriptions.Features.CreatingRestockSubscription.Events.Domain;

public record RestockSubscriptionCreated(RestockSubscription RestockSubscription) : DomainEvent
{
    public CreateMongoRestockSubscriptionReadModels ToCreateMongoRestockSubscriptionReadModels(
        long customerId,
        string customerName)
    {
        return new CreateMongoRestockSubscriptionReadModels(
            RestockSubscription.Id,
            customerId,
            customerName,
            RestockSubscription.ProductInformation.Id,
            RestockSubscription.ProductInformation.Name,
            RestockSubscription.Email.Value,
            RestockSubscription.Created,
            RestockSubscription.Processed,
            RestockSubscription.ProcessedTime);
    }
}

internal class RestockSubscriptionCreatedHandler : IDomainEventHandler<RestockSubscriptionCreated>
{
    private readonly ICommandProcessor _commandProcessor;
    private readonly CustomersDbContext _customersDbContext;

    public RestockSubscriptionCreatedHandler(ICommandProcessor commandProcessor, CustomersDbContext customersDbContext)
    {
        _commandProcessor = commandProcessor;
        _customersDbContext = customersDbContext;
    }

    public async Task Handle(RestockSubscriptionCreated notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));

        var customer = await _customersDbContext.FindCustomerByIdAsync(notification.RestockSubscription.CustomerId);

        Guard.Against.NotFound(
            customer,
            new CustomerNotFoundException(notification.RestockSubscription.CustomerId));

        var mongoReadCommand =
            notification.ToCreateMongoRestockSubscriptionReadModels(customer!.Id, customer.Name.FullName);

        // https://github.com/kgrzybek/modular-monolith-with-ddd#38-internal-processing
        // Schedule multiple read sides to execute here
        await _commandProcessor.ScheduleAsync(new IInternalCommand[] { mongoReadCommand }, cancellationToken);
    }
}
