using Ingredients.Abstractions.CQRS.Events;
using Ingredients.Core.Types;

namespace Ingredients.Core.CQRS.Events;

public abstract record Event : IEvent
{
    public Guid EventId { get; protected set; } = Guid.NewGuid();

    public long EventVersion { get; protected set; } = -1;

    public DateTime OccurredOn { get; protected set; } = DateTime.Now;

    public DateTimeOffset TimeStamp { get; protected set; } = DateTimeOffset.Now;

    public string EventType => TypeMapper.GetTypeName(GetType());
}
