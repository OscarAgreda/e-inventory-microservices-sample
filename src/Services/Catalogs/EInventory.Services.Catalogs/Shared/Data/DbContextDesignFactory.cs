using Ingredients.Persistence.EfCore.Postgres;

namespace EInventory.Services.Catalogs.Shared.Data;

public class CatalogDbContextDesignFactory : DbContextDesignFactoryBase<CatalogDbContext>
{
    public CatalogDbContextDesignFactory() : base("ConnectionStrings:CatalogServiceConnection")
    {
    }
}
