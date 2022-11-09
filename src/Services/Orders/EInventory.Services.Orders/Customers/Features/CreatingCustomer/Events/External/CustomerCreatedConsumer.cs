using MassTransit;
using EInventory.Services.Shared.Customers.Customers.Events.Integration;

namespace EInventory.Services.Orders.Customers.Features.CreatingCustomer.Events.External;

public class CustomerCreatedConsumer : IConsumer<CustomerCreated>
{
    public Task Consume(ConsumeContext<CustomerCreated> context)
    {
        return Task.CompletedTask;
    }
}
