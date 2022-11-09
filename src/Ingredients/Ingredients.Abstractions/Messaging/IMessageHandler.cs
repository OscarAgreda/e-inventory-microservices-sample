using Ingredients.Abstractions.Messaging.Context;

namespace Ingredients.Abstractions.Messaging;

public interface IMessageHandler<in TMessage>
    where TMessage : class, IMessage
{
    Task HandleAsync(IConsumeContext<TMessage> messageContext, CancellationToken cancellationToken = default);
}
