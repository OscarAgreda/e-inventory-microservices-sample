using Ingredients.Core.CQRS.Events.Internal;

namespace EInventory.Services.Catalogs.Products.Features.ChangingMaxThreshold;

public record MaxThresholdChanged(long ProductId, int MaxThreshold) : DomainEvent;
