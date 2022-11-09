using Ingredients.Core.Messaging;

namespace EInventory.Services.Shared.Catalogs.Products.Events.Integration;

public record ProductStockDebited(long ProductId, int NewStock, int DebitedQuantity) : IntegrationEvent;
