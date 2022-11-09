using Ingredients.Core.CQRS.Events.Internal;
using EInventory.Services.Catalogs.Products.ValueObjects;

namespace EInventory.Services.Catalogs.Products.Features.DebitingProductStock.Events.Domain;

public record ProductStockDebited(ProductId ProductId, Stock NewStock, int DebitedQuantity) : DomainEvent;
