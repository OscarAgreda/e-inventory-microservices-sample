using Ingredients.Core.Exception.Types;

namespace EInventory.Services.Catalogs.Products.Exceptions.Application;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(long id) : base($"Product with id '{id}' not found")
    {
    }

    public ProductNotFoundException(string message) : base(message)
    {
    }
}
