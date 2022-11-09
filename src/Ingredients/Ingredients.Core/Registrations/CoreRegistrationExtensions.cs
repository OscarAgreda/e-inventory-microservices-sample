using System.Reflection;
using Ingredients.Abstractions.Core;
using Ingredients.Abstractions.CQRS.Events;
using Ingredients.Abstractions.Messaging;
using Ingredients.Abstractions.Messaging.PersistMessage;
using Ingredients.Abstractions.Serialization;
using Ingredients.Abstractions.Types;
using Ingredients.Core.CQRS.Events;
using Ingredients.Core.Extensions;
using Ingredients.Core.Extensions.ServiceCollection;
using Ingredients.Core.IdsGenerator;
using Ingredients.Core.Messaging.BackgroundServices;
using Ingredients.Core.Messaging.MessagePersistence;
using Ingredients.Core.Messaging.MessagePersistence.InMemory;
using Ingredients.Core.Serialization;
using Ingredients.Core.Types;
using Microsoft.Extensions.Configuration;
using Scrutor;

namespace Ingredients.Core.Registrations;

public static class CoreRegistrationExtensions
{
    public static IServiceCollection AddCore(
        this IServiceCollection services,
        IConfiguration configuration,
        params Assembly[] assembliesToScan)
    {
        var systemInfo = MachineInstanceInfo.New();

        services.AddSingleton<IMachineInstanceInfo>(systemInfo);
        services.AddSingleton(systemInfo);
        services.AddSingleton<IExclusiveLock, ExclusiveLock>();

        services.AddTransient<IAggregatesDomainEventsRequestStore, AggregatesDomainEventsStore>();

        services.AddHttpContextAccessor();

        AddDefaultSerializer(services);

        AddMessagingCore(services, configuration, ServiceLifetime.Transient, assembliesToScan);

        RegisterEventMappers(services, assembliesToScan);

        switch (configuration["IdGenerator:Type"])
        {
            case "Guid":
                services.AddSingleton<IIdGenerator<Guid>, GuidIdGenerator>();
                break;
            default:
                services.AddSingleton<IIdGenerator<long>, SnowFlakIdGenerator>();
                break;
        }

        return services;
    }

    private static void RegisterEventMappers(IServiceCollection services, params Assembly[] assembliesToScan)
    {
        services.Scan(scan => scan
            .FromAssemblies(assembliesToScan.Any() ? assembliesToScan : AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(classes => classes.AssignableTo(typeof(IEventMapper)), false)
            .AsImplementedInterfaces()
            .WithSingletonLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(IIntegrationEventMapper)), false)
            .AsImplementedInterfaces()
            .WithSingletonLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(IIDomainNotificationEventMapper)), false)
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
    }

    private static void AddMessagingCore(
        this IServiceCollection services,
        IConfiguration configuration,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient,
        params Assembly[] assembliesToScan)
    {
        AddMessagingMediator(services, serviceLifetime, assembliesToScan);

        AddPersistenceMessage(services, configuration);
    }

    private static void AddPersistenceMessage(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IMessagePersistenceService, MessagePersistenceService>();
        services.AddScoped<IMessagePersistenceRepository, NullPersistenceRepository>();
        services.AddHostedService<MessagePersistenceBackgroundService>();
        services.AddOptions<MessagePersistenceOptions>()
            .Bind(configuration.GetSection(nameof(MessagePersistenceOptions)))
            .ValidateDataAnnotations();
    }

    private static void AddMessagingMediator(
        IServiceCollection services,
        ServiceLifetime serviceLifetime,
        Assembly[] assembliesToScan)
    {
        services.Scan(scan => scan
            .FromAssemblies(assembliesToScan.Any() ? assembliesToScan : AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(classes => classes.AssignableTo(typeof(IMessageHandler<>)))
            .UsingRegistrationStrategy(RegistrationStrategy.Append)
            .AsClosedTypeOf(typeof(IMessageHandler<>))
            .AsSelf()
            .WithLifetime(serviceLifetime));
    }

    private static void AddDefaultSerializer(
        IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        services.Add<ISerializer, DefaultSerializer>(lifetime);
        services.Add<IMessageSerializer, DefaultMessageSerializer>(lifetime);
    }
}
