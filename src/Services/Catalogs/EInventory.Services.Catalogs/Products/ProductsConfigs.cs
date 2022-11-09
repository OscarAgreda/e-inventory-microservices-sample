using Ingredients.Abstractions.CQRS.Events;
using Ingredients.Abstractions.Persistence;
using Ingredients.Abstractions.Web.Module;
using EInventory.Services.Catalogs.Products.Data;
using EInventory.Services.Catalogs.Products.Features.CreatingProduct;
using EInventory.Services.Catalogs.Products.Features.DebitingProductStock;
using EInventory.Services.Catalogs.Products.Features.GettingProductById;
using EInventory.Services.Catalogs.Products.Features.GettingProductsView;
using EInventory.Services.Catalogs.Products.Features.ReplenishingProductStock;
using EInventory.Services.Catalogs.Products.Features.UpdatingProduct;

namespace EInventory.Services.Catalogs.Products;

internal class ProductsConfigs : IModuleConfiguration
{
    public const string Tag = "Product";
    public const string ProductsPrefixUri = $"{CatalogModuleConfiguration.CatalogModulePrefixUri}/products";

    public IServiceCollection AddModuleServices(
        IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment webHostEnvironment)
    {
        services.AddScoped<IDataSeeder, ProductDataSeeder>();
        services.AddSingleton<IEventMapper, ProductEventMapper>();

        return services;
    }

    public Task<WebApplication> ConfigureModule(WebApplication app)
    {
        return Task.FromResult<WebApplication>(app);
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapCreateProductsEndpoint()
            .MapUpdateProductEndpoint()
            .MapDebitProductStockEndpoint()
            .MapReplenishProductStockEndpoint()
            .MapGetProductByIdEndpoint()
            .MapGetProductsViewEndpoint();
    }
}
