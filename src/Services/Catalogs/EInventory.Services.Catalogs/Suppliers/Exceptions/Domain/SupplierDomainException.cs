using Ingredients.Core.Domain.Exceptions;

namespace EInventory.Services.Catalogs.Suppliers.Exceptions.Domain;

public class SupplierDomainException : DomainException
{
    public SupplierDomainException(string message) : base(message)
    {
    }
}
