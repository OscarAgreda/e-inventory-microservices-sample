using Ardalis.GuardClauses;
using Ingredients.Abstractions.CQRS.Events.Internal;
using Ingredients.Core.CQRS.Events.Internal;
using EInventory.Services.Catalogs.Categories;
using EInventory.Services.Catalogs.Shared.Contracts;
using EInventory.Services.Catalogs.Shared.Extensions;

namespace EInventory.Services.Catalogs.Products.Features.ChangingProductCategory.Events;

public record ChangingProductCategory(CategoryId CategoryId) : DomainEvent;

internal class ChangingProductCategoryValidationHandler :
    IDomainEventHandler<ChangingProductCategory>
{
    private readonly ICatalogDbContext _catalogDbContext;

    public ChangingProductCategoryValidationHandler(ICatalogDbContext catalogDbContext)
    {
        _catalogDbContext = catalogDbContext;
    }

    public async Task Handle(ChangingProductCategory notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
        Guard.Against.NegativeOrZero(notification.CategoryId, nameof(notification.CategoryId));
        Guard.Against.ExistsCategory(
            await _catalogDbContext.CategoryExistsAsync(notification.CategoryId, cancellationToken: cancellationToken),
            notification.CategoryId);
    }
}
