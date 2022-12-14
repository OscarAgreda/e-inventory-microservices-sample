using Ardalis.GuardClauses;
using Ingredients.Caching.InMemory;
using Ingredients.Core.Caching;
using Ingredients.Core.Extensions;
using Ingredients.Core.Persistence.EfCore;
using Ingredients.Core.Registrations;
using Ingredients.Email;
using Ingredients.Integration.MassTransit;
using Ingredients.Logging;
using Ingredients.Messaging.Persistence.Postgres.Extensions;
using Ingredients.Monitoring;
using Ingredients.Persistence.EfCore.Postgres;
using Ingredients.Validation;
using EInventory.Services.Identity.Users;

namespace EInventory.Services.Identity.Shared.Extensions.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment webHostEnvironment)
    {
        services.AddCore(configuration);

        services.AddMonitoring(healthChecksBuilder =>
        {
            var postgresOptions = configuration.GetOptions<PostgresOptions>(nameof(PostgresOptions));
            var rabbitMqOptions = configuration.GetOptions<RabbitMqOptions>(nameof(RabbitMqOptions));

            Guard.Against.Null(postgresOptions, nameof(postgresOptions));
            Guard.Against.Null(rabbitMqOptions, nameof(rabbitMqOptions));

            healthChecksBuilder
                .AddNpgSql(
                    postgresOptions.ConnectionString,
                    name: "IdentityService-Postgres-Check",
                    tags: new[] {"postgres", "database", "infra", "identity-service"})
                .AddRabbitMQ(
                    rabbitMqOptions.ConnectionString,
                    name: "IdentityService-RabbitMQ-Check",
                    timeout: TimeSpan.FromSeconds(3),
                    tags: new[] {"rabbitmq", "bus", "infra", "identity-service"});
        });

        services.AddEmailService(configuration);

        services.AddCqrs(doMoreActions: s =>
        {
            s.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
                .AddScoped(typeof(IStreamPipelineBehavior<,>), typeof(StreamRequestValidationBehavior<,>))
                .AddScoped(typeof(IStreamPipelineBehavior<,>), typeof(StreamLoggingBehavior<,>))
                .AddScoped(typeof(IStreamPipelineBehavior<,>), typeof(StreamCachingBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(InvalidateCachingBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(EfTxBehavior<,>));
        });

        services.AddPostgresMessagePersistence(configuration);

        services.AddCustomMassTransit(
            configuration,
            webHostEnvironment,
            (context, cfg) =>
            {
                cfg.AddUserPublishers();
            },
            autoConfigEndpoints: false);

        services.AddCustomInMemoryCache(configuration)
            .AddCachingRequestPolicies(Assembly.GetExecutingAssembly());


        services.AddCustomValidators(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
