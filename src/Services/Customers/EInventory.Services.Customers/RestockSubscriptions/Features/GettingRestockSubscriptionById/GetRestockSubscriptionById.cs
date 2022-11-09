using Ardalis.GuardClauses;
using AutoMapper;
using Ingredients.Abstractions.CQRS.Queries;
using Ingredients.Core.Exception;
using EInventory.Services.Customers.RestockSubscriptions.Dtos;
using EInventory.Services.Customers.RestockSubscriptions.Exceptions.Application;
using EInventory.Services.Customers.Shared.Data;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EInventory.Services.Customers.RestockSubscriptions.Features.GettingRestockSubscriptionById;

public record GetRestockSubscriptionById(long Id) : IQuery<GetRestockSubscriptionByIdResult>;

internal class GetRestockSubscriptionByIdValidator : AbstractValidator<GetRestockSubscriptionById>
{
    public GetRestockSubscriptionByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal class GetRestockSubscriptionByIdHandler
    : IQueryHandler<GetRestockSubscriptionById, GetRestockSubscriptionByIdResult>
{
    private readonly CustomersReadDbContext _customersReadDbContext;
    private readonly IMapper _mapper;

    public GetRestockSubscriptionByIdHandler(CustomersReadDbContext customersReadDbContext, IMapper mapper)
    {
        _customersReadDbContext = customersReadDbContext;
        _mapper = mapper;
    }

    public async Task<GetRestockSubscriptionByIdResult> Handle(
        GetRestockSubscriptionById query,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var restockSubscription =
            await _customersReadDbContext.RestockSubscriptions.AsQueryable()
                .Where(x => x.IsDeleted == false)
                .SingleOrDefaultAsync(x => x.RestockSubscriptionId == query.Id, cancellationToken: cancellationToken);

        Guard.Against.NotFound(restockSubscription, new RestockSubscriptionNotFoundException(query.Id));

        var subscriptionDto = _mapper.Map<RestockSubscriptionDto>(restockSubscription);

        return new GetRestockSubscriptionByIdResult(subscriptionDto);
    }
}

public record GetRestockSubscriptionByIdResult(RestockSubscriptionDto RestockSubscription);
