using Ingredients.Abstractions.CQRS.Events.Internal;

namespace Ingredients.Abstractions.Persistence.EventStore.Projections;

public interface IReadProjectionPublisher
{
    Task PublishAsync(IStreamEvent streamEvent, CancellationToken cancellationToken = default);

    Task PublishAsync<T>(IStreamEvent<T> streamEvent, CancellationToken cancellationToken = default)
        where T : IDomainEvent;
}