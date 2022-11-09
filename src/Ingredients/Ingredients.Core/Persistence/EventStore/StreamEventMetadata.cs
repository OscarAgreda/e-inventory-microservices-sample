using Ingredients.Abstractions.Persistence.EventStore;

namespace Ingredients.Core.Persistence.EventStore;

public record StreamEventMetadata(string EventId, long StreamPosition) : IStreamEventMetadata
{
    public long? LogPosition { get; }
}
