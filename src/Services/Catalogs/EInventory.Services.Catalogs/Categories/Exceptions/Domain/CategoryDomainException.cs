using Ingredients.Core.Domain.Exceptions;

namespace EInventory.Services.Catalogs.Categories.Exceptions.Domain;

public class CategoryDomainException : DomainException
{
    public CategoryDomainException(string message) : base(message)
    {
    }

    public CategoryDomainException(long id) : base($"Category with id: '{id}' not found.")
    {
    }
}
