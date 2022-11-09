using Ingredients.Abstractions.Persistence.EfCore;
using Microsoft.EntityFrameworkCore;
using EInventory.Services.Orders.Orders.Models;

namespace EInventory.Services.Orders.Shared.Contracts;

public interface IOrdersDbContext : IDbContext
{
    public DbSet<Order> Orders { get; }
}
