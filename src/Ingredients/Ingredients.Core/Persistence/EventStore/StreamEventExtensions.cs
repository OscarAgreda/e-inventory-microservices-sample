using Ingredients.Abstractions.CQRS.Events.Internal;
using Ingredients.Abstractions.Persistence.EventStore;
using Ingredients.Core.Utils;

namespace Ingredients.Core.Persistence.EventStore;

public static class StreamEventExtensions
{
    public static IStreamEvent ToStreamEvent(
        this IDomainEvent domainEvent,
        IStreamEventMetadata? metadata)
    {
        return ReflectionUtilities.CreateGenericType(
            typeof(StreamEvent<>),
            new[] { domainEvent.GetType() },
            domainEvent,
            metadata);
    }
}
