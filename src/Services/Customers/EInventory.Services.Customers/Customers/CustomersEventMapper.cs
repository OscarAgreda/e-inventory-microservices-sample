using Ingredients.Abstractions.CQRS.Events;
using Ingredients.Abstractions.CQRS.Events.Internal;
using Ingredients.Abstractions.Messaging;
using EInventory.Services.Customers.Customers.Features.CreatingCustomer.Events.Domain;

namespace EInventory.Services.Customers.Customers;

public class CustomersEventMapper : IIntegrationEventMapper
{
    public IReadOnlyList<IIntegrationEvent?> MapToIntegrationEvents(IReadOnlyList<IDomainEvent> domainEvents)
    {
        return domainEvents.Select(MapToIntegrationEvent).ToList();
    }

    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            TestDomainEvent e => new TestIntegration(e.Data),
            CustomerCreated e => new Services.Shared.Customers.Customers.Events.Integration.CustomerCreated(e.Customer.Id),
            _ => null
        };
    }
}
