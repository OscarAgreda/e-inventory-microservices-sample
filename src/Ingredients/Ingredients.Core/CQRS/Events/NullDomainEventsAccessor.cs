using Ingredients.Abstractions.CQRS.Events;
using Ingredients.Abstractions.CQRS.Events.Internal;

namespace Ingredients.Core.CQRS.Events;

public class NullDomainEventsAccessor : IDomainEventsAccessor
{
    public IReadOnlyList<IDomainEvent> UnCommittedDomainEvents { get; }
}
