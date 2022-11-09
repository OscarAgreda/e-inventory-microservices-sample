using Ingredients.Core.CQRS.Events.Internal;
using EInventory.Services.Catalogs.Brands;
using EInventory.Services.Catalogs.Products.ValueObjects;

namespace EInventory.Services.Catalogs.Products.Features.ChangingProductBrand.Events.Domain;

internal record ProductBrandChanged(BrandId BrandId, ProductId ProductId) : DomainEvent;
