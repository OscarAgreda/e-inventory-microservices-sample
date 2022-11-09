using Ingredients.Abstractions.CQRS.Commands;
using Ingredients.Core.Types;

namespace Ingredients.Core.CQRS.Commands;

public abstract record InternalCommand : IInternalCommand
{
    public Guid Id { get; protected set; } = Guid.NewGuid();

    public DateTime OccurredOn { get; protected set; } = DateTime.Now;

    public string Type { get { return TypeMapper.GetTypeName(GetType()); } }
}
