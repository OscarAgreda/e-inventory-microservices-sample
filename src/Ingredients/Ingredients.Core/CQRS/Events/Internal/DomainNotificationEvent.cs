using Ingredients.Abstractions.CQRS.Events.Internal;

namespace Ingredients.Core.CQRS.Events.Internal;

public abstract record DomainNotificationEvent : Event, IDomainNotificationEvent;
