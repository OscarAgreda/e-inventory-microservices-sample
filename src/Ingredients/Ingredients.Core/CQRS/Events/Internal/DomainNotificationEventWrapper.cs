using Ingredients.Abstractions.CQRS.Events.Internal;

namespace Ingredients.Core.CQRS.Events.Internal;

public record DomainNotificationEventWrapper<TDomainEventType>(TDomainEventType DomainEvent) : DomainNotificationEvent
    where TDomainEventType : IDomainEvent;
