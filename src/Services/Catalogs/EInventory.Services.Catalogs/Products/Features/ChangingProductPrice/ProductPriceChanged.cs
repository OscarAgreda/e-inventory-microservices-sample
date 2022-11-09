using Ingredients.Core.CQRS.Events.Internal;
using EInventory.Services.Catalogs.Products.ValueObjects;

namespace EInventory.Services.Catalogs.Products.Features.ChangingProductPrice;

public record ProductPriceChanged(Price Price) : DomainEvent;
