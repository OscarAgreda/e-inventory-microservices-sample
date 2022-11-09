using MediatR;

namespace Ingredients.Abstractions.CQRS.Events;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : INotification
{
}
