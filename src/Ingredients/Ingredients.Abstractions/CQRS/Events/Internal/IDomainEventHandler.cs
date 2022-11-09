namespace Ingredients.Abstractions.CQRS.Events.Internal;

public interface IDomainEventHandler<in TEvent> : IEventHandler<TEvent>
    where TEvent : IDomainEvent
{
}
