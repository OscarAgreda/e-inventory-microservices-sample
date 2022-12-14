using Ardalis.GuardClauses;
using Ingredients.Abstractions.CQRS.Commands;
using Ingredients.Core.Exception;
using EInventory.Services.Catalogs.Products.Exceptions.Application;
using EInventory.Services.Catalogs.Shared.Contracts;
using EInventory.Services.Catalogs.Shared.Extensions;

namespace EInventory.Services.Catalogs.Products.Features.ReplenishingProductStock;

public record ReplenishingProductStock(long ProductId, int Quantity) : ITxCommand;

internal class ReplenishingProductStockValidator : AbstractValidator<ReplenishingProductStock>
{
    public ReplenishingProductStockValidator()
    {
        // https://docs.fluentvalidation.net/en/latest/conditions.html#stop-vs-stoponfirstfailure
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProductId must be greater than 0");
    }
}

internal class ReplenishingProductStockHandler : ICommandHandler<ReplenishingProductStock>
{
    private readonly ICatalogDbContext _catalogDbContext;

    public ReplenishingProductStockHandler(ICatalogDbContext catalogDbContext)
    {
        _catalogDbContext = Guard.Against.Null(catalogDbContext, nameof(catalogDbContext));
    }

    public async Task<Unit> Handle(ReplenishingProductStock command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var product = await _catalogDbContext.FindProductByIdAsync(command.ProductId);
        Guard.Against.NotFound(product, new ProductNotFoundException(command.ProductId));

        product!.ReplenishStock(command.Quantity);
        await _catalogDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
