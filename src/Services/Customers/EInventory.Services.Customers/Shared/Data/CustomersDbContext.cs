using Ingredients.Core.Persistence.EfCore;
using Microsoft.EntityFrameworkCore;
using EInventory.Services.Customers.Customers.Models;
using EInventory.Services.Customers.RestockSubscriptions.Models.Write;
using EInventory.Services.Customers.Shared.Contracts;

namespace EInventory.Services.Customers.Shared.Data;

public class CustomersDbContext : EfDbContextBase, ICustomersDbContext
{
    public const string DefaultSchema = "customer";

    public CustomersDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension(EfConstants.UuidGenerator);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<RestockSubscription> RestockSubscriptions => Set<RestockSubscription>();
}
