using Ingredients.Abstractions.CQRS.Events;
using Ingredients.Core.Messaging;

namespace EInventory.Services.Catalogs.Suppliers.Features.SupplierDeleted.Events.External;

public record SupplierDeleted(long Id) : IntegrationEvent;

public class SupplierDeletedConsumer : IEventHandler<SupplierDeleted>
{
    public Task Handle(SupplierDeleted notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
