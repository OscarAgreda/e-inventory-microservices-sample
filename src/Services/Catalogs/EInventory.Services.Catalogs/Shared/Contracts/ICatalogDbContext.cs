using EInventory.Services.Catalogs.Brands;
using EInventory.Services.Catalogs.Categories;
using EInventory.Services.Catalogs.Products.Models;
using EInventory.Services.Catalogs.Suppliers;

namespace EInventory.Services.Catalogs.Shared.Contracts;

public interface ICatalogDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Category> Categories { get; }
    DbSet<Brand> Brands { get; }
    DbSet<Supplier> Suppliers { get; }
    DbSet<ProductView> ProductsView { get; }

    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
