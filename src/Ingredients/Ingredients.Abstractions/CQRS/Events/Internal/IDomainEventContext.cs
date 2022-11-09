namespace Ingredients.Abstractions.CQRS.Events.Internal;

public interface IDomainEventContext
{
    IReadOnlyList<IDomainEvent> GetAllUncommittedEvents();
    void MarkUncommittedDomainEventAsCommitted();
}
