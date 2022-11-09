using Ingredients.Abstractions.CQRS.Events.Internal;
using Ingredients.Abstractions.Persistence.EventStore;
using Ingredients.Core.CQRS.Events;

namespace Ingredients.Core.Persistence.EventStore;

public record StreamEvent
    (IDomainEvent Data,  IStreamEventMetadata? Metadata = null) : Event, IStreamEvent;

public record StreamEvent<T>(T Data,  IStreamEventMetadata? Metadata = null)
    : StreamEvent(Data, Metadata), IStreamEvent<T>
    where T : IDomainEvent
{
    public new T Data => (T)base.Data;
}
