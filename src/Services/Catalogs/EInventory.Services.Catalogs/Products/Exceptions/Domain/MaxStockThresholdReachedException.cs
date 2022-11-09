using Ingredients.Core.Domain.Exceptions;

namespace EInventory.Services.Catalogs.Products.Exceptions.Domain;

public class MaxStockThresholdReachedException : DomainException
{
    public MaxStockThresholdReachedException(string message) : base(message)
    {
    }
}
