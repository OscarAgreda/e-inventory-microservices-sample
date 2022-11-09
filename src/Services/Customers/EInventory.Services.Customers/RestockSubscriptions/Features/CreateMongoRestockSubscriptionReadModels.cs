using Ardalis.GuardClauses;
using AutoMapper;
using Ingredients.Abstractions.CQRS.Commands;
using Ingredients.Core.CQRS.Commands;
using EInventory.Services.Customers.RestockSubscriptions.Models.Read;
using EInventory.Services.Customers.Shared.Data;

namespace EInventory.Services.Customers.RestockSubscriptions.Features;

public record CreateMongoRestockSubscriptionReadModels(
    long RestockSubscriptionId,
    long CustomerId,
    string CustomerName,
    long ProductId,
    string ProductName,
    string Email,
    DateTime Created,
    bool Processed,
    DateTime? ProcessedTime = null) : InternalCommand
{
    public bool IsDeleted { get; init; } = false;
}

internal class CreateRestockSubscriptionReadModelHandler : ICommandHandler<CreateMongoRestockSubscriptionReadModels>
{
    private readonly CustomersReadDbContext _mongoDbContext;
    private readonly IMapper _mapper;

    public CreateRestockSubscriptionReadModelHandler(CustomersReadDbContext mongoDbContext, IMapper mapper)
    {
        _mongoDbContext = mongoDbContext;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(
        CreateMongoRestockSubscriptionReadModels command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var readModel = _mapper.Map<RestockSubscriptionReadModel>(command);

        await _mongoDbContext.RestockSubscriptions.InsertOneAsync(readModel, cancellationToken: cancellationToken);

        return Unit.Value;
    }
}
