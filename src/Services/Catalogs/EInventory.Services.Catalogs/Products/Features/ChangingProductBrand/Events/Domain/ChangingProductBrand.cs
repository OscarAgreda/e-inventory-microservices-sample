using Ardalis.GuardClauses;
using Ingredients.Abstractions.CQRS.Events.Internal;
using Ingredients.Core.CQRS.Events.Internal;
using EInventory.Services.Catalogs.Brands;
using EInventory.Services.Catalogs.Shared.Contracts;
using EInventory.Services.Catalogs.Shared.Extensions;

namespace EInventory.Services.Catalogs.Products.Features.ChangingProductBrand.Events.Domain;

internal record ChangingProductBrand(BrandId BrandId) : DomainEvent;

internal class ChangingProductBrandValidationHandler :
    IDomainEventHandler<ChangingProductBrand>
{
    private readonly ICatalogDbContext _catalogDbContext;

    public ChangingProductBrandValidationHandler(ICatalogDbContext catalogDbContext)
    {
        _catalogDbContext = catalogDbContext;
    }

    public async Task Handle(ChangingProductBrand notification, CancellationToken cancellationToken)
    {
        // Handling some validations
        Guard.Against.Null(notification, nameof(notification));
        Guard.Against.NegativeOrZero(notification.BrandId, nameof(notification.BrandId));
        Guard.Against.ExistsBrand(
            await _catalogDbContext.BrandExistsAsync(notification.BrandId, cancellationToken: cancellationToken),
            notification.BrandId);
    }
}
