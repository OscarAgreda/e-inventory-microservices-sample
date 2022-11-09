using MediatR;

namespace Ingredients.Abstractions.Messaging;

public interface IMessage : INotification
{
    Guid MessageId { get; }
    DateTime Created { get; }
}
