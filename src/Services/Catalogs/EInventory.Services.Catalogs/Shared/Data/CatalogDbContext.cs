using Ingredients.Core.Persistence.EfCore;
using EInventory.Services.Catalogs.Brands;
using EInventory.Services.Catalogs.Categories;
using EInventory.Services.Catalogs.Products.Models;
using EInventory.Services.Catalogs.Shared.Contracts;
using EInventory.Services.Catalogs.Suppliers;

namespace EInventory.Services.Catalogs.Shared.Data;

public class CatalogDbContext : EfDbContextBase, ICatalogDbContext
{
    public const string DefaultSchema = "catalog";

    public CatalogDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductView> ProductsView => Set<ProductView>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Brand> Brands => Set<Brand>();
}
