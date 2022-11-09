using Ardalis.GuardClauses;
using Ingredients.Abstractions.Caching;
using Ingredients.Core.Caching;
using Ingredients.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace Ingredients.Caching.InMemory;

public static class Extensions
{
    public static IServiceCollection AddCustomInMemoryCache(
        this IServiceCollection services,
        IConfiguration config,
        Action<InMemoryCacheOptions>? configureOptions = null)
    {
        Guard.Against.Null(services, nameof(services));

        if (configureOptions is { })
        {
            services.Configure(configureOptions);
        }
        else
        {
            services.AddOptions<InMemoryCacheOptions>().Bind(config.GetSection(nameof(InMemoryCacheOptions)))
                .ValidateDataAnnotations();
        }

        services.AddMemoryCache();
        services.AddTransient<ICacheManager, CacheManager>();
        services.AddSingleton<ICacheProvider, InMemoryCacheProvider>();

        return services;
    }
}
