using EInventory.Services.Catalogs.Products.Models;
using EInventory.Services.Catalogs.Shared.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EInventory.Services.Catalogs.Products.Data;

public class ProductViewEntityTypeConfiguration : IEntityTypeConfiguration<ProductView>
{
    public void Configure(EntityTypeBuilder<ProductView> builder)
    {
        builder.ToTable("product_views", CatalogDbContext.DefaultSchema);
        builder.HasKey(x => x.ProductId);
        builder.HasIndex(x => x.ProductId).IsUnique();
    }
}
