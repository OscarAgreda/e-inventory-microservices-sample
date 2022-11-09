using Ingredients.Abstractions.CQRS.Commands;

namespace EInventory.Services.Catalogs.Products.Features.ChangingProductCategory;

internal record ChangeProductCategory : ITxCommand<ChangeProductCategoryResult>;

internal record ChangeProductCategoryResult;

internal class ChangeProductCategoryHandler :
    ICommandHandler<ChangeProductCategory, ChangeProductCategoryResult>
{
    public Task<ChangeProductCategoryResult> Handle(
        ChangeProductCategory command,
        CancellationToken cancellationToken)
    {
        return Task.FromResult<ChangeProductCategoryResult>(null!);
    }
}
