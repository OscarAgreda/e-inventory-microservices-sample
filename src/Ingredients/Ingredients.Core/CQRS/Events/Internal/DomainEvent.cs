using Ingredients.Abstractions.CQRS.Events.Internal;

namespace Ingredients.Core.CQRS.Events.Internal;

public abstract record DomainEvent : Event, IDomainEvent
{
    public dynamic AggregateId { get; protected set; } = null!;
    public long AggregateSequenceNumber { get; protected set; }

    public virtual IDomainEvent WithAggregate(dynamic aggregateId, long version)
    {
        AggregateId = aggregateId;
        AggregateSequenceNumber = version;

        return this;
    }
}
