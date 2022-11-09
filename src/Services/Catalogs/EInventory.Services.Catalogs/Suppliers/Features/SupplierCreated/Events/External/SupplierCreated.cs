using Ingredients.Abstractions.CQRS.Events;
using Ingredients.Core.Messaging;

namespace EInventory.Services.Catalogs.Suppliers.Features.SupplierCreated.Events.External;

public record SupplierCreated(long Id, string Name) : IntegrationEvent;


public class SupplierCreatedConsumer : IEventHandler<SupplierCreated>
{
    public Task Handle(SupplierCreated notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
