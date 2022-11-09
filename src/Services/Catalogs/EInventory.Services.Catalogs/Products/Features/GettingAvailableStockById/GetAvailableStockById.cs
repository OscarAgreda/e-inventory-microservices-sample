using Ingredients.Abstractions.CQRS.Queries;

namespace EInventory.Services.Catalogs.Products.Features.GettingAvailableStockById;

public record GetAvailableStockById(long ProductId) : IQuery<int>;

