using Ingredients.Abstractions.CQRS.Events;

namespace Ingredients.Abstractions.Messaging;

public interface IIntegrationEventHandler<in TEvent> : IEventHandler<TEvent>
    where TEvent : IIntegrationEvent
{
}
