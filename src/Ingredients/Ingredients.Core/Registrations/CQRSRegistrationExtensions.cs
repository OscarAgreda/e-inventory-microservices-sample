using System.Reflection;
using Ingredients.Abstractions.CQRS.Commands;
using Ingredients.Abstractions.CQRS.Events;
using Ingredients.Abstractions.CQRS.Events.Internal;
using Ingredients.Abstractions.CQRS.Queries;
using Ingredients.Abstractions.Scheduler;
using Ingredients.Core.CQRS.Commands;
using Ingredients.Core.CQRS.Events;
using Ingredients.Core.CQRS.Queries;
using Ingredients.Core.Extensions.ServiceCollection;
using Ingredients.Core.Scheduler;
using MediatR;

namespace Ingredients.Core.Registrations;

public static class CQRSRegistrationExtensions
{
    public static IServiceCollection AddCqrs(
        this IServiceCollection services,
        Assembly[]? assemblies = null,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient,
        Action<IServiceCollection>? doMoreActions = null)
    {
        services.AddMediatR(
            assemblies ?? new[] { Assembly.GetCallingAssembly() },
            x =>
            {
                switch (serviceLifetime)
                {
                    case ServiceLifetime.Transient:
                        x.AsTransient();
                        break;
                    case ServiceLifetime.Scoped:
                        x.AsScoped();
                        break;
                    case ServiceLifetime.Singleton:
                        x.AsSingleton();
                        break;
                }
            });

        services.Add<ICommandProcessor, CommandProcessor>(serviceLifetime)
            .Add<IQueryProcessor, QueryProcessor>(serviceLifetime)
            .Add<IEventProcessor, EventProcessor>(serviceLifetime)
            .Add<ICommandScheduler, NullCommandScheduler>(serviceLifetime)
            .Add<IDomainEventPublisher, DomainEventPublisher>(serviceLifetime)
            .Add<IDomainNotificationEventPublisher, DomainNotificationEventPublisher>(serviceLifetime);

        services.AddScoped<IDomainEventsAccessor, NullDomainEventsAccessor>();

        doMoreActions?.Invoke(services);

        return services;
    }
}
