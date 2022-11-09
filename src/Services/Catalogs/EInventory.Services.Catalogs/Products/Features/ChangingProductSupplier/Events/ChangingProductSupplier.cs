using Ardalis.GuardClauses;
using Ingredients.Abstractions.CQRS.Events.Internal;
using Ingredients.Core.CQRS.Events.Internal;
using EInventory.Services.Catalogs.Shared.Contracts;
using EInventory.Services.Catalogs.Shared.Extensions;
using EInventory.Services.Catalogs.Suppliers;

namespace EInventory.Services.Catalogs.Products.Features.ChangingProductSupplier.Events;

public record ChangingProductSupplier(SupplierId SupplierId) : DomainEvent;

internal class ChangingSupplierValidationHandler :
    IDomainEventHandler<ChangingProductSupplier>
{
    private readonly ICatalogDbContext _catalogDbContext;

    public ChangingSupplierValidationHandler(ICatalogDbContext catalogDbContext)
    {
        _catalogDbContext = catalogDbContext;
    }

    public async Task Handle(ChangingProductSupplier notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
        Guard.Against.NegativeOrZero(notification.SupplierId, nameof(notification.SupplierId));
        Guard.Against.ExistsSupplier(
            await _catalogDbContext.SupplierExistsAsync(notification.SupplierId, cancellationToken: cancellationToken),
            notification.SupplierId);
    }
}
