using Ingredients.Abstractions.CQRS.Events;
using Ingredients.Abstractions.CQRS.Events.Internal;
using Ingredients.Abstractions.Messaging;
using EInventory.Services.Customers.RestockSubscriptions.Features.CreatingRestockSubscription.Events.Domain;

namespace EInventory.Services.Customers.RestockSubscriptions;

public class RestockSubscriptionsEventMapper : IIntegrationEventMapper
{
    public IReadOnlyList<IIntegrationEvent?> MapToIntegrationEvents(IReadOnlyList<IDomainEvent> domainEvents)
    {
        return domainEvents.Select(MapToIntegrationEvent).ToList();
    }

    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            RestockSubscriptionCreated e =>
                new Services.Shared.Customers.RestockSubscriptions.Events.Integration.RestockSubscriptionCreated(
                    e.RestockSubscription.Id.Value, e.RestockSubscription.Email),
            _ => null
        };
    }
}
