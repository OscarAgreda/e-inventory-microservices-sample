using Ingredients.Core.CQRS.Events.Internal;
using EInventory.Services.Catalogs.Brands;
using EInventory.Services.Catalogs.Categories;
using EInventory.Services.Catalogs.Products.Models;
using EInventory.Services.Catalogs.Products.ValueObjects;
using EInventory.Services.Catalogs.Suppliers;

namespace EInventory.Services.Catalogs.Products.Features.CreatingProduct.Events.Domain;

public record CreatingProduct(
    ProductId Id,
    Name Name,
    Price Price,
    Stock Stock,
    ProductStatus Status,
    Dimensions Dimensions,
    Category? Category,
    Supplier? Supplier,
    Brand? Brand,
    string? Description = null) : DomainEvent;
