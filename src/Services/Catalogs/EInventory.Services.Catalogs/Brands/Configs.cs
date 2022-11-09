using Ingredients.Abstractions.Persistence;
using Ingredients.Abstractions.Web.Module;
using EInventory.Services.Catalogs.Brands.Data;

namespace EInventory.Services.Catalogs.Brands;

internal class Configs : IModuleConfiguration
{
    public IServiceCollection AddModuleServices(
        IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment webHostEnvironment)
    {
        services.AddScoped<IDataSeeder, BrandDataSeeder>();

        return services;
    }

    public Task<WebApplication> ConfigureModule(WebApplication app)
    {
        return Task.FromResult(app);
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        return endpoints;
    }
}
