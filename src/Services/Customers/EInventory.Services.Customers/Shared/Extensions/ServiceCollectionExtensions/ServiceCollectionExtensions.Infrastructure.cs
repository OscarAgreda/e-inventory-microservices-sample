using Ardalis.GuardClauses;
using Ingredients.Caching.InMemory;
using Ingredients.Core.Caching;
using Ingredients.Core.Extensions;
using Ingredients.Core.IdsGenerator;
using Ingredients.Core.Persistence.EfCore;
using Ingredients.Core.Registrations;
using Ingredients.Email;
using Ingredients.Integration.MassTransit;
using Ingredients.Logging;
using Ingredients.Messaging.Persistence.Postgres.Extensions;
using Ingredients.Monitoring;
using Ingredients.Persistence.EfCore.Postgres;
using Ingredients.Persistence.Mongo;
using Ingredients.Validation;
using EInventory.Services.Customers.Customers;
using EInventory.Services.Customers.Products;
using EInventory.Services.Customers.RestockSubscriptions;
using EInventory.Services.Customers.Shared.Clients.Catalogs;
using EInventory.Services.Customers.Shared.Clients.Identity;
using EInventory.Services.Customers.Users;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EInventory.Services.Customers.Shared.Extensions.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment webHostEnvironment)
    {
        SnowFlakIdGenerator.Configure(2);

        services.AddCore(configuration);

        services.AddMonitoring(healthChecksBuilder =>
        {
            var postgresOptions = configuration.GetOptions<PostgresOptions>(nameof(PostgresOptions));
            var rabbitMqOptions = configuration.GetOptions<RabbitMqOptions>(nameof(RabbitMqOptions));
            var mongoOptions = configuration.GetOptions<MongoOptions>(nameof(MongoOptions));
            var identityApiClientOptions =
                configuration.GetOptions<IdentityApiClientOptions>(nameof(IdentityApiClientOptions));
            var catalogsApiClientOptions =
                configuration.GetOptions<CatalogsApiClientOptions>(nameof(CatalogsApiClientOptions));

            Guard.Against.Null(postgresOptions, nameof(postgresOptions));
            Guard.Against.Null(rabbitMqOptions, nameof(rabbitMqOptions));
            Guard.Against.Null(mongoOptions, nameof(mongoOptions));
            Guard.Against.Null(identityApiClientOptions, nameof(identityApiClientOptions));
            Guard.Against.Null(catalogsApiClientOptions, nameof(catalogsApiClientOptions));

            healthChecksBuilder
                .AddNpgSql(
                    postgresOptions.ConnectionString,
                    name: "CustomersService-Postgres-Check",
                    tags: new[] {"postgres", "database", "infra", "customers-service"})
                .AddRabbitMQ(
                    rabbitMqOptions.ConnectionString,
                    name: "CustomersService-RabbitMQ-Check",
                    timeout: TimeSpan.FromSeconds(3),
                    tags: new[] {"rabbitmq", "bus", "infra", "customers-service"})
                .AddMongoDb(
                    mongoOptions.ConnectionString,
                    mongoDatabaseName: mongoOptions.DatabaseName,
                    "CustomersService-Mongo-Check",
                    tags: new[] {"mongodb", "database", "infra", "customers-service"})
                .AddUrlGroup(
                    new List<Uri> {new($"{catalogsApiClientOptions.BaseApiAddress}/healthz")},
                    name: "Catalogs-Downstream-API-Check",
                    failureStatus: HealthStatus.Unhealthy,
                    timeout: TimeSpan.FromSeconds(3),
                    tags: new[] {"uris", "downstream-services", "customers-service"})
                .AddUrlGroup(
                    new List<Uri> {new($"{identityApiClientOptions.BaseApiAddress}/healthz")},
                    name: "Identity-Downstream-API-Check",
                    failureStatus: HealthStatus.Unhealthy,
                    timeout: TimeSpan.FromSeconds(3),
                    tags: new[] {"uris", "downstream-services", "customers-service"});
        });

        services.AddEmailService(configuration);

        services.AddCqrs(
            doMoreActions: s =>
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
                cfg.AddUsersEndpoints(context);
                cfg.AddProductEndpoints(context);

                cfg.AddCustomerPublishers();
                cfg.AddRestockSubscriptionPublishers();
            },
            autoConfigEndpoints: false);

        services.AddCustomValidators(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddCustomInMemoryCache(configuration)
            .AddCachingRequestPolicies(Assembly.GetExecutingAssembly());

        services.AddCustomHttpClients(configuration);

        return services;
    }
}
