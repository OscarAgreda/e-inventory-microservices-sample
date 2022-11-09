using Ingredients.Core.Domain.Exceptions;

namespace EInventory.Services.Catalogs.Brands.Exceptions.Domain;

public class BrandDomainException : DomainException
{
    public BrandDomainException(string message) : base(message)
    {
    }
}
