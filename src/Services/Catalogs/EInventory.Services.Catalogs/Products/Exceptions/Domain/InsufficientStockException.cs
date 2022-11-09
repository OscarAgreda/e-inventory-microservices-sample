using Ingredients.Core.Domain.Exceptions;

namespace EInventory.Services.Catalogs.Products.Exceptions.Domain;

public class InsufficientStockException : DomainException
{
    public InsufficientStockException(string message) : base(message)
    {
    }
}
