using Ardalis.GuardClauses;
using EInventory.Services.Catalogs.Categories.Exceptions.Application;

namespace EInventory.Services.Catalogs.Categories;

public static class GuardExtensions
{
    public static void ExistsCategory(this IGuardClause guardClause, bool exists, long categoryId)
    {
        if (exists == false)
            throw new CategoryNotFoundException(categoryId);
    }
}
