using EInventory.Services.Customers.Customers.Models;
using EInventory.Services.Customers.Customers.ValueObjects;
using EInventory.Services.Customers.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace EInventory.Services.Customers.Shared.Extensions;

public static class CustomersDbContextExtensions
{
    public static ValueTask<Customer?> FindCustomerByIdAsync(this CustomersDbContext context, CustomerId id)
    {
        return context.Customers.FindAsync(id);
    }

    public static Task<bool> ExistsCustomerByIdAsync(this CustomersDbContext context, CustomerId id)
    {
        return context.Customers.AnyAsync(x => x.Id == id);
    }
}
