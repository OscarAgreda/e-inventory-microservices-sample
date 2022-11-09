using Ardalis.GuardClauses;
using Ingredients.Core.Domain;
using Ingredients.Core.Domain.ValueObjects;
using EInventory.Services.Customers.Customers.Features.CreatingCustomer.Events.Domain;
using EInventory.Services.Customers.Customers.ValueObjects;

namespace EInventory.Services.Customers.Customers.Models;

public class Customer : Aggregate<CustomerId>
{
    public Guid IdentityId { get; private set; }
    public Email Email { get; private set; } = null!;
    public CustomerName Name { get; private set; } = null!;
    public Address? Address { get; private set; }
    public Nationality? Nationality { get; private set; }
    public BirthDate? BirthDate { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }

    public static Customer Create(CustomerId id, Email email, CustomerName name, Guid identityId)
    {
        var customer = new Customer
        {
            Id = Guard.Against.Null(id, nameof(id)),
            Email = Guard.Against.Null(email, nameof(email)),
            Name = Guard.Against.Null(name, nameof(name)),
            IdentityId = Guard.Against.NullOrEmpty(identityId, nameof(IdentityId)),
        };

        customer.AddDomainEvents(new CustomerCreated(customer));

        return customer;
    }
}
