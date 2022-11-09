using Microsoft.EntityFrameworkCore;
using EInventory.Services.Orders.Orders.Models;
using EInventory.Services.Orders.Orders.ValueObjects;
using EInventory.Services.Orders.Shared.Data;

namespace EInventory.Services.Orders.Shared.Extensions;

public static class OrdersDbContextExtensions
{
    public static ValueTask<Order?> FindOrderByIdAsync(this OrdersDbContext context, OrderId id)
    {
        return context.Orders.FindAsync(id);
    }

    public static Task<bool> ExistsOrderByIdAsync(this OrdersDbContext context, OrderId id)
    {
        return context.Orders.AnyAsync(x => x.Id == id);
    }
}
