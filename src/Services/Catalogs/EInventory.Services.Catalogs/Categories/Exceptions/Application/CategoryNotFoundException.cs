using Ingredients.Core.Exception.Types;

namespace EInventory.Services.Catalogs.Categories.Exceptions.Application;

public class CategoryNotFoundException : NotFoundException
{
    public CategoryNotFoundException(long id) : base($"Category with id '{id}' not found.")
    {
    }

    public CategoryNotFoundException(string message) : base(message)
    {
    }
}
