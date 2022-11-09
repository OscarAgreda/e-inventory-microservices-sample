using Ingredients.Core.CQRS.Events.Internal;

namespace EInventory.Services.Catalogs.Products.Features.ChangingRestockThreshold;

public record RestockThresholdChanged(long ProductId, int RestockThreshold) : DomainEvent;
