using Ingredients.Abstractions.Messaging.PersistMessage;
using Ingredients.Core.Extensions.ServiceCollection;
using Ingredients.Core.Messaging.MessagePersistence.InMemory;

namespace Ingredients.Core.Registrations;

public static class InMemoryMessagingRegistrationExtensions
{
    public static IServiceCollection AddInMemoryMessagePersistence(this IServiceCollection services)
    {
        services.ReplaceScoped<IMessagePersistenceRepository, InMemoryMessagePersistenceRepository>();

        return services;
    }
}
