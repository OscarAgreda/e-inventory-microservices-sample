using Ingredients.Core.Exception.Types;

namespace EInventory.Services.Customers.Customers.Exceptions.Domain;

internal class CustomerAlreadyCompletedException : AppException
{
    public long CustomerId { get; }

    public CustomerAlreadyCompletedException(long customerId)
        : base($"Customer with ID: '{customerId}' already completed.")
    {
        CustomerId = customerId;
    }
}
