using Microsoft.EntityFrameworkCore;
using EInventory.Services.Customers.Customers.Models;

namespace EInventory.Services.Customers.Shared.Contracts;

public interface ICustomersDbContext
{
    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

    public DbSet<Customer> Customers { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
