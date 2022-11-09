using Ingredients.Abstractions.CQRS.Events.Internal;
using Ingredients.Core.Messaging;

namespace Ingredients.Core.CQRS.Events.Internal;

public record IntegrationEventWrapper<TDomainEventType>
    : IntegrationEvent
    where TDomainEventType : IDomainEvent;
