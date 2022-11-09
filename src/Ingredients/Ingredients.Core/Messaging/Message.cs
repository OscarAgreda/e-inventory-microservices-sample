using Ingredients.Abstractions.Messaging;

namespace Ingredients.Core.Messaging;

public record Message : IMessage
{
    public Guid MessageId => Guid.NewGuid();
    public DateTime Created { get; } = DateTime.Now;
}
