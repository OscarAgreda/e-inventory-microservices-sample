using Ingredients.Core.Exception.Types;

namespace EInventory.Services.Customers.Customers.Exceptions.Application;

public class CustomerNotFoundException : NotFoundException
{
    public CustomerNotFoundException(string message) : base(message)
    {
    }

    public CustomerNotFoundException(long id) : base($"Customer with id '{id}' not found.")
    {
    }
}
