using Ardalis.GuardClauses;
using EInventory.Services.Catalogs.Suppliers.Exceptions.Application;

namespace EInventory.Services.Catalogs.Suppliers;

public static class GuardExtensions
{
    public static void ExistsSupplier(this IGuardClause guardClause, bool exists, long supplierId)
    {
        if (exists == false)
            throw new SupplierNotFoundException(supplierId);
    }
}
