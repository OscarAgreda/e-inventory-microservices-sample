using Ingredients.Core.Messaging;

namespace EInventory.Services.Shared.Customers.RestockSubscriptions.Events.Integration;

public record RestockSubscriptionCreated(long CustomerId, string? Email) : IntegrationEvent;
