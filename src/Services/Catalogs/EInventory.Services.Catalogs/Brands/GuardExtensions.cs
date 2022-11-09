using Ardalis.GuardClauses;
using EInventory.Services.Catalogs.Brands.Exceptions.Application;

namespace EInventory.Services.Catalogs.Brands;

public static class GuardExtensions
{
    public static void ExistsBrand(this IGuardClause guardClause, bool exists, long brandId)
    {
        if (!exists)
            throw new BrandNotFoundException(brandId);
    }
}
