using Ardalis.GuardClauses;
using AutoMapper;
using Ingredients.Abstractions.CQRS.Queries;
using Ingredients.Core.Exception;
using EInventory.Services.Customers.Customers.Dtos;
using EInventory.Services.Customers.Customers.Exceptions.Application;
using EInventory.Services.Customers.Shared.Data;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EInventory.Services.Customers.Customers.Features.GettingCustomerById;

public record GetCustomerById(long Id) : IQuery<GetCustomerByIdResult>;

public record GetCustomerByIdResult(CustomerReadDto Customer);

internal class GetCustomerByIdValidator : AbstractValidator<GetCustomerById>
{
    public GetCustomerByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal class GetRestockSubscriptionByIdHandler
    : IQueryHandler<GetCustomerById, GetCustomerByIdResult>
{
    private readonly CustomersReadDbContext _customersReadDbContext;
    private readonly IMapper _mapper;

    public GetRestockSubscriptionByIdHandler(CustomersReadDbContext customersReadDbContext, IMapper mapper)
    {
        _customersReadDbContext = customersReadDbContext;
        _mapper = mapper;
    }

    public async Task<GetCustomerByIdResult> Handle(
        GetCustomerById query,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var customer = await _customersReadDbContext.Customers.AsQueryable()
            .SingleOrDefaultAsync(x => x.CustomerId == query.Id, cancellationToken: cancellationToken);

        Guard.Against.NotFound(customer, new CustomerNotFoundException(query.Id));

        var customerDto = _mapper.Map<CustomerReadDto>(customer);

        return new GetCustomerByIdResult(customerDto);
    }
}
