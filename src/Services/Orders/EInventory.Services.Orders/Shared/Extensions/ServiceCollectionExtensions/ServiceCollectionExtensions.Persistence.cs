using Ingredients.Abstractions.Persistence;
using Ingredients.Persistence.EfCore.Postgres;
using Ingredients.Persistence.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EInventory.Services.Orders.Shared.Contracts;
using EInventory.Services.Orders.Shared.Data;

namespace EInventory.Services.Orders.Shared.Extensions.ServiceCollectionExtensions;

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
        if (configuration.GetValue<bool>("PostgresOptions.UseInMemory"))
        {
            services.AddDbContext<OrdersDbContext>(options =>
                options.UseInMemoryDatabase("EInventory.Services.Customers"));

            services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<OrdersDbContext>()!);
        }
        else
        {
            services.AddPostgresDbContext<OrdersDbContext>(configuration);
        }

        services.AddScoped<IOrdersDbContext>(provider => provider.GetRequiredService<OrdersDbContext>());
    }

    private static void AddMongoReadStorage(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDbContext<OrderReadDbContext>(configuration);
    }
}
