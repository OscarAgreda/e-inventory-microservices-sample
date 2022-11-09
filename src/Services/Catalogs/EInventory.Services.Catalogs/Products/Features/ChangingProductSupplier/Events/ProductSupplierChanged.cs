using Ingredients.Core.CQRS.Events.Internal;
using EInventory.Services.Catalogs.Products.ValueObjects;
using EInventory.Services.Catalogs.Suppliers;

namespace EInventory.Services.Catalogs.Products.Features.ChangingProductSupplier.Events;

public record ProductSupplierChanged(SupplierId SupplierId, ProductId ProductId) : DomainEvent;
