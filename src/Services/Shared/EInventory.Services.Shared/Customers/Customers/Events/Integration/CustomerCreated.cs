using Ingredients.Core.Messaging;

namespace EInventory.Services.Shared.Customers.Customers.Events.Integration;

public record CustomerCreated(long CustomerId) : IntegrationEvent;
