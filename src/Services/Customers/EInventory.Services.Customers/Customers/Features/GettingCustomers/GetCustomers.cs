using AutoMapper;
using Ingredients.Abstractions.CQRS.Queries;
using Ingredients.Core.CQRS.Queries;
using Ingredients.Core.Types;
using Ingredients.Persistence.Mongo;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using EInventory.Services.Customers.Customers.Dtos;
using EInventory.Services.Customers.Customers.Models.Reads;
using EInventory.Services.Customers.Shared.Data;

namespace EInventory.Services.Customers.Customers.Features.GettingCustomers;

public record GetCustomers : ListQuery<GetCustomersResult>;

public class GetCustomersValidator : AbstractValidator<GetCustomers>
{
    public GetCustomersValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page should at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should at least greater than or equal to 1.");
    }
}

public class GetCustomersHandler : IQueryHandler<GetCustomers, GetCustomersResult>
{
    private readonly CustomersReadDbContext _customersReadDbContext;
    private readonly IMapper _mapper;

    public GetCustomersHandler(CustomersReadDbContext customersReadDbContext, IMapper mapper)
    {
        _customersReadDbContext = customersReadDbContext;
        _mapper = mapper;
    }

    public async Task<GetCustomersResult> Handle(GetCustomers request, CancellationToken cancellationToken)
    {
        var customer = await _customersReadDbContext.Customers.AsQueryable()
            .OrderByDescending(x => x.City)
            .ApplyFilter(request.Filters)
            .ApplyPagingAsync<CustomerReadModel, CustomerReadDto>(
                _mapper.ConfigurationProvider,
                request.Page,
                request.PageSize,
                cancellationToken: cancellationToken);

        return new GetCustomersResult(customer);
    }
}

public record GetCustomersResult(ListResultModel<CustomerReadDto> Customers);
