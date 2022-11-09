using Ingredients.Core.CQRS.Events.Internal;
using EInventory.Services.Catalogs.Categories;
using EInventory.Services.Catalogs.Products.ValueObjects;

namespace EInventory.Services.Catalogs.Products.Features.ChangingProductCategory.Events;

public record ProductCategoryChanged(CategoryId CategoryId, ProductId ProductId) :
    DomainEvent;
