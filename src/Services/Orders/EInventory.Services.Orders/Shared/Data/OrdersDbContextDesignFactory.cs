using Ingredients.Persistence.EfCore.Postgres;

namespace EInventory.Services.Orders.Shared.Data;

public class OrdersDbContextDesignFactory : DbContextDesignFactoryBase<OrdersDbContext>
{
    public OrdersDbContextDesignFactory() : base("ConnectionStrings:OrdersServiceConnection")
    {
    }
}
