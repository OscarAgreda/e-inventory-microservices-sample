using Ingredients.Resiliency.Extensions;
using EInventory.Services.Customers.Shared.Clients.Catalogs;
using EInventory.Services.Customers.Shared.Clients.Identity;

namespace EInventory.Services.Customers.Shared.Extensions.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomHttpClients(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<IdentityApiClientOptions>().Bind(configuration.GetSection(nameof(IdentityApiClientOptions)))
            .ValidateDataAnnotations();

        services.AddOptions<CatalogsApiClientOptions>().Bind(configuration.GetSection(nameof(CatalogsApiClientOptions)))
            .ValidateDataAnnotations();

        services.AddHttpApiClient<ICatalogApiClient, CatalogApiClient>();
        services.AddHttpApiClient<IIdentityApiClient, IdentityApiClient>();

        return services;
    }
}
