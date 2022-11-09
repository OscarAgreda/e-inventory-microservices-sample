using Ardalis.GuardClauses;
using Ingredients.Abstractions.Messaging;
using Ingredients.Abstractions.Persistence;
using Ingredients.Core.Extensions;
using Ingredients.Core.Messaging;
using MassTransit;
using MassTransit.Configuration;
using MassTransit.Testing;
using MassTransit.Testing.Implementations;
using MassTransit.Transports;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using IBus = Ingredients.Abstractions.Messaging.IBus;

namespace Ingredients.Integration.MassTransit;

public static class Extensions
{
    public static IServiceCollection AddCustomMassTransit(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment env,
        Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? configureReceiveEndpoints = null,
        Action<IBusRegistrationConfigurator>? configureBusRegistration = null,
        bool autoConfigEndpoints = false)
    {
        var rabbitMqOptions = configuration.GetOptions<RabbitMqOptions>(nameof(RabbitMqOptions));

        Guard.Against.Null(rabbitMqOptions, nameof(rabbitMqOptions));

        void ConfiguratorAction(IBusRegistrationConfigurator busRegistrationConfigurator)
        {
            configureBusRegistration?.Invoke(busRegistrationConfigurator);

            // https://masstransit-project.com/usage/configuration.html#receive-endpoints
            busRegistrationConfigurator.AddConsumers(AppDomain.CurrentDomain.GetAssemblies());

            // exclude namespace for the messages
            busRegistrationConfigurator.SetEndpointNameFormatter(new SnakeCaseEndpointNameFormatter(false));

            // Ref: https://masstransit-project.com/advanced/topology/rabbitmq.html
            // https://masstransit-project.com/understand/publishing.html
            // https://spring.io/blog/2011/04/01/routing-topologies-for-performance-and-scalability-with-rabbitmq/
            // https://jstobigdata.com/rabbitmq/exchange-to-exchange-binding-in-rabbitmq/
            // https://masstransit-project.com/understand/under-the-hood.html
            // https://masstransit-project.com/usage/producers.html
            // https://masstransit-project.com/usage/consumers.html
            // https://masstransit-project.com/usage/messages.html
            // https://wrapt.dev/blog/building-an-event-driven-dotnet-application-setting-up-masstransit-and-rabbitmq
            busRegistrationConfigurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.PublishTopology.BrokerTopologyOptions = PublishBrokerTopologyOptions.FlattenHierarchy;

                if (autoConfigEndpoints)
                {
                    // https://masstransit-project.com/usage/consumers.html#consumer
                    cfg.ConfigureEndpoints(context);
                }

                cfg.Host(rabbitMqOptions.Host, "/", hostConfigurator =>
                {
                    hostConfigurator.Username(rabbitMqOptions.UserName);
                    hostConfigurator.Password(rabbitMqOptions.Password);
                });

                // https://masstransit-project.com/usage/exceptions.html#retry
                // cfg.UseMessageRetry(r => r.Interval(3, 3));

                // cfg.UseInMemoryOutbox();

                // https: // github.com/MassTransit/MassTransit/issues/2018
                cfg.Publish<IIntegrationEvent>(p => p.Exclude = true);
                cfg.Publish<IntegrationEvent>(p => p.Exclude = true);
                cfg.Publish<IMessage>(p => p.Exclude = true);
                cfg.Publish<Message>(p => p.Exclude = true);
                cfg.Publish<ITxRequest>(p => p.Exclude = true);

                // for setting exchange name for message type as default. masstransit by default uses fully message type name for exchange name.
                cfg.MessageTopology.SetEntityNameFormatter(new CustomEntityNameFormatter());

                configureReceiveEndpoints?.Invoke(context, cfg);
            });
        }

        if (env.IsEnvironment("test") == false)
        {
            services.AddMassTransit(ConfiguratorAction);
        }
        else
        {
            services.AddMassTransitTestHarness(ConfiguratorAction);
        }

        services.AddTransient<IBus, MassTransitBus>();

        return services;
    }
}
