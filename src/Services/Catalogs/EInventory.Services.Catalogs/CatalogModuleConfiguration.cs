using Ingredients.Abstractions.Web.Module;
using Ingredients.Core;
using Ingredients.Monitoring;
using EInventory.Services.Catalogs.Shared.Extensions.ApplicationBuilderExtensions;
using EInventory.Services.Catalogs.Shared.Extensions.ServiceCollectionExtensions;

namespace EInventory.Services.Catalogs;

public class CatalogModuleConfiguration : ISharedModulesConfiguration
{
    public const string CatalogModulePrefixUri = "api/v1/catalogs";

    public IEndpointRouteBuilder MapSharedModuleEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", (HttpContext context) =>
        {
            var requestId = context.Request.Headers.TryGetValue("X-Request-Id", out var requestIdHeader)
                ? requestIdHeader.FirstOrDefault()
                : string.Empty;

            return $"Catalogs Service Apis, RequestId: {requestId}";
        }).ExcludeFromDescription();

        return endpoints;
    }

    public IServiceCollection AddSharedModuleServices(
        IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment webHostEnvironment)
    {
        services.AddInfrastructure(configuration, webHostEnvironment);

        services.AddStorage(configuration);

        return services;
    }

    public async Task<WebApplication> ConfigureSharedModule(WebApplication app)
    {
        ServiceActivator.Configure(app.Services);

        if (app.Environment.IsEnvironment("test") == false)
            app.UseMonitoring();

        await app.ApplyDatabaseMigrations(app.Logger);
        await app.SeedData(app.Logger, app.Environment);

        await app.UseInfrastructure(app.Logger);

        return app;
    }
}
