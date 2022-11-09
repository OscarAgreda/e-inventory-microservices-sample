using Ingredients.Abstractions.CQRS.Commands;

namespace EInventory.Services.Catalogs.Products.Features.ChangingRestockThreshold;

public record ChangeRestockThreshold(long ProductId, int NewRestockThreshold) : ITxCommand;
