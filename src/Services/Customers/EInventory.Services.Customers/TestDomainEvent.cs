using Ingredients.Core.CQRS.Events.Internal;

namespace EInventory.Services.Customers;

public record TestDomainEvent(string Data) : DomainEvent;
