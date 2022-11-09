using Ingredients.Abstractions.Core;

namespace Ingredients.Core.IdsGenerator;

public class GuidIdGenerator : IIdGenerator<Guid>
{
    public Guid New()
    {
       return Guid.NewGuid();
    }
}
