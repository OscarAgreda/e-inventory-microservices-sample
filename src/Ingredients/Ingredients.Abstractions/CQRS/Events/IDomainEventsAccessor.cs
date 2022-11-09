using Ingredients.Abstractions.CQRS.Events.Internal;

namespace Ingredients.Abstractions.CQRS.Events;

public interface IDomainEventsAccessor
{
    IReadOnlyList<IDomainEvent> UnCommittedDomainEvents { get; }
}
