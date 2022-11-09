using Ingredients.Persistence.EfCore.Postgres;

namespace EInventory.Services.Customers.Shared.Data;

public class CustomerDbContextDesignFactory : DbContextDesignFactoryBase<CustomersDbContext>
{
    public CustomerDbContextDesignFactory() : base("ConnectionStrings:CustomersServiceConnection")
    {
    }
}
