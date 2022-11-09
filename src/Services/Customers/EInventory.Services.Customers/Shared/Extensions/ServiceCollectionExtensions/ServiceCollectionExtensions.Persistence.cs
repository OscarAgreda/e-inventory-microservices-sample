using Ingredients.Abstractions.Persistence;
using Ingredients.Persistence.EfCore.Postgres;
using Ingredients.Persistence.Mongo;
using EInventory.Services.Customers.Shared.Contracts;
using EInventory.Services.Customers.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace EInventory.Services.Customers.Shared.Extensions.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddStorage(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        AddStorage(builder.Services, configuration);

        return builder;
    }

    public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        AddPostgresWriteStorage(services, configuration);
        AddMongoReadStorage(services, configuration);

        return services;
    }

    private static void AddPostgresWriteStorage(IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("PostgresOptions:UseInMemory"))
        {
            services.AddDbContext<CustomersDbContext>(options =>
                options.UseInMemoryDatabase("EInventory.Services.EInventory.Services.Customers"));

            services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<CustomersDbContext>()!);
        }
        else
        {
            services.AddPostgresDbContext<CustomersDbContext>(configuration);
        }

        services.AddScoped<ICustomersDbContext>(provider => provider.GetRequiredService<CustomersDbContext>());
    }

    private static void AddMongoReadStorage(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDbContext<CustomersReadDbContext>(configuration);
    }
}
