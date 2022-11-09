using Ingredients.Core.Messaging;

namespace EInventory.Services.Shared.Catalogs.Products.Events.Integration;

public record ProductStockReplenished(long ProductId, int NewStock, int ReplenishedQuantity) : IntegrationEvent;
