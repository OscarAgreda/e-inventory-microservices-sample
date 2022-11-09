using Ingredients.Core.CQRS.Events.Internal;
using EInventory.Services.Catalogs.Products.ValueObjects;

namespace EInventory.Services.Catalogs.Products.Features.ReplenishingProductStock.Events.Domain;

public record ProductStockReplenished(ProductId ProductId, Stock NewStock, int ReplenishedQuantity) : DomainEvent;
