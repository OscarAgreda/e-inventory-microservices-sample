using Ingredients.Abstractions.CQRS.Events.Internal;
using Ingredients.Abstractions.Domain;
using Ingredients.Abstractions.Domain.EventSourcing;
using Ingredients.Abstractions.Persistence.EventStore.Projections;

namespace Ingredients.Abstractions.Persistence.EventStore;

public interface IHaveEventSourcingAggregate :
    IHaveAggregateStateProjection,
    IHaveAggregate,
    IHaveEventSourcedAggregateVersion
{
    /// <summary>
    /// Loads the current state of the aggregate from a list of events.
    /// </summary>
    /// <param name="history">Domain events from the aggregate stream.</param>
    void LoadFromHistory(IEnumerable<IDomainEvent> history);
}
