using Ingredients.Abstractions.Persistence;
using Ingredients.Abstractions.Web.Module;
using EInventory.Services.Catalogs.Categories.Data;

namespace EInventory.Services.Catalogs.Categories;

internal class Configs : IModuleConfiguration
{
    public IServiceCollection AddModuleServices(
        IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment webHostEnvironment)
    {
        services.AddScoped<IDataSeeder, CategoryDataSeeder>();

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
