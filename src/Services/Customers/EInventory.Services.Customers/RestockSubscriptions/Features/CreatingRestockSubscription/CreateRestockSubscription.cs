using Ardalis.GuardClauses;
using AutoMapper;
using Ingredients.Abstractions.CQRS.Commands;
using Ingredients.Core.Domain.ValueObjects;
using Ingredients.Core.Exception;
using Ingredients.Core.IdsGenerator;
using EInventory.Services.Customers.Customers.Exceptions.Application;
using EInventory.Services.Customers.Products.Exceptions;
using EInventory.Services.Customers.RestockSubscriptions.Dtos;
using EInventory.Services.Customers.RestockSubscriptions.Features.CreatingRestockSubscription.Exceptions;
using EInventory.Services.Customers.RestockSubscriptions.Models.Write;
using EInventory.Services.Customers.RestockSubscriptions.ValueObjects;
using EInventory.Services.Customers.Shared.Clients.Catalogs;
using EInventory.Services.Customers.Shared.Data;
using EInventory.Services.Customers.Shared.Extensions;

namespace EInventory.Services.Customers.RestockSubscriptions.Features.CreatingRestockSubscription;

public record CreateRestockSubscription(long CustomerId, long ProductId, string Email)
    : ITxCreateCommand<CreateRestockSubscriptionResult>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}

internal class CreateRestockSubscriptionValidator : AbstractValidator<CreateRestockSubscription>
{
    public CreateRestockSubscriptionValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty();

        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}

public record CreateRestockSubscriptionResult(RestockSubscriptionDto RestockSubscription);

internal class CreateRestockSubscriptionHandler
    : ICommandHandler<CreateRestockSubscription, CreateRestockSubscriptionResult>
{
    private readonly CustomersDbContext _customersDbContext;
    private readonly ICatalogApiClient _catalogApiClient;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateRestockSubscriptionHandler> _logger;

    public CreateRestockSubscriptionHandler(
        CustomersDbContext customersDbContext,
        ICatalogApiClient catalogApiClient,
        IMapper mapper,
        ILogger<CreateRestockSubscriptionHandler> logger)
    {
        _customersDbContext = customersDbContext;
        _catalogApiClient = catalogApiClient;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateRestockSubscriptionResult> Handle(
        CreateRestockSubscription request,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var existsCustomer = await _customersDbContext.ExistsCustomerByIdAsync(request.CustomerId);
        Guard.Against.NotExists(existsCustomer, new CustomerNotFoundException(request.CustomerId));

        var product = (await _catalogApiClient.GetProductByIdAsync(request.ProductId, cancellationToken))?.Product;
        Guard.Against.NotFound(product, new ProductNotFoundException(request.ProductId));

        if (product!.AvailableStock > 0)
            throw new ProductHaveStockException(product.Id, product.AvailableStock, product.Name);

        var alreadySubscribed = _customersDbContext.RestockSubscriptions
            .Any(x => x.Email == request.Email && x.ProductInformation.Id == request.ProductId && x.Processed == false);

        if (alreadySubscribed)
            throw new ProductAlreadySubscribedException(product.Id, product.Name);

        var restockSubscription =
            RestockSubscription.Create(
                request.Id,
                request.CustomerId,
                ProductInformation.Create(product!.Id, product.Name),
                Email.Create(request.Email));

        await _customersDbContext.AddAsync(restockSubscription, cancellationToken);

        await _customersDbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("RestockSubscription with id '{@Id}' saved successfully", restockSubscription.Id);

        var restockSubscriptionDto = _mapper.Map<RestockSubscriptionDto>(restockSubscription);

        return new CreateRestockSubscriptionResult(restockSubscriptionDto);
    }
}
