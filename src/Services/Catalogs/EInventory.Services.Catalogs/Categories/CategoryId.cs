using Ingredients.Abstractions.Domain;

namespace EInventory.Services.Catalogs.Categories;

public record CategoryId : AggregateId
{
    public CategoryId(long value) : base(value)
    {
    }

    public static implicit operator long(CategoryId id) => id.Value;

    public static implicit operator CategoryId(long id) => new(id);
}
