using Ingredients.Persistence.EfCore.Postgres;

namespace EInventory.Services.Identity.Shared.Data;

public class DbContextDesignFactory : DbContextDesignFactoryBase<IdentityContext>
{
    public DbContextDesignFactory() : base("ConnectionStrings:IdentityServiceConnection")
    {
    }
}
