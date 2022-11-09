using Ingredients.Abstractions.CQRS.Commands;

namespace EInventory.Services.Catalogs.Products.Features.ChangingMaxThreshold;

public record ChangeMaxThreshold(long ProductId, int NewMaxThreshold) : ITxCommand;
