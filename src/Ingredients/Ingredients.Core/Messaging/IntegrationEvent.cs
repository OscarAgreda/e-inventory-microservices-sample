using Ingredients.Abstractions.Messaging;

namespace Ingredients.Core.Messaging;

public record IntegrationEvent : Message, IIntegrationEvent;
