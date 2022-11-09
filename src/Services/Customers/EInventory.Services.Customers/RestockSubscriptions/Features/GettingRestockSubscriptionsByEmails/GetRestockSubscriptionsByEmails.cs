using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ingredients.Abstractions.CQRS.Queries;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using EInventory.Services.Customers.RestockSubscriptions.Dtos;
using EInventory.Services.Customers.Shared.Data;

namespace EInventory.Services.Customers.RestockSubscriptions.Features.GettingRestockSubscriptionsByEmails;

public record GetRestockSubscriptionsByEmails(IList<string> Emails) : IStreamQuery<RestockSubscriptionDto>;

internal class GetRestockSubscriptionsByEmailsValidator : AbstractValidator<GetRestockSubscriptionsByEmails>
{
    public GetRestockSubscriptionsByEmailsValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(request => request.Emails)
            .NotNull()
            .NotEmpty();
    }
}

internal class GetRestockSubscriptionsByEmailsHandler
    : IStreamQueryHandler<GetRestockSubscriptionsByEmails, RestockSubscriptionDto>
{
    private readonly CustomersReadDbContext _customersReadDbContext;
    private readonly IMapper _mapper;

    public GetRestockSubscriptionsByEmailsHandler(CustomersReadDbContext customersReadDbContext, IMapper mapper)
    {
        _customersReadDbContext = customersReadDbContext;
        _mapper = mapper;
    }

    public IAsyncEnumerable<RestockSubscriptionDto> Handle(
        GetRestockSubscriptionsByEmails query,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var result = _customersReadDbContext.RestockSubscriptions.AsQueryable()
            .Where(x => !x.IsDeleted)
            .Where(x => query.Emails.Contains(x.Email!))
            .ProjectTo<RestockSubscriptionDto>(_mapper.ConfigurationProvider)
            .ToAsyncEnumerable();

        return result;
    }
}
