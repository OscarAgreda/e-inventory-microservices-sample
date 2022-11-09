using Ingredients.Abstractions.Persistence;
using Ingredients.Persistence.EfCore.Postgres;
using Ingredients.Persistence.Mongo;
using EInventory.Services.Catalogs.Shared.Contracts;
using EInventory.Services.Catalogs.Shared.Data;

namespace EInventory.Services.Catalogs.Shared.Extensions.ServiceCollectionExtensions;

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
            services.AddDbContext<CatalogDbContext>(options =>
                options.UseInMemoryDatabase("EInventory.Services.EInventory.Services.Catalogs"));

            services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<CatalogDbContext>()!);
        }
        else
        {
            services.AddPostgresDbContext<CatalogDbContext>(configuration);
        }

        services.AddScoped<ICatalogDbContext>(provider => provider.GetRequiredService<CatalogDbContext>());
    }

    private static void AddMongoReadStorage(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDbContext<CatalogReadDbContext>(configuration);
    }
}
