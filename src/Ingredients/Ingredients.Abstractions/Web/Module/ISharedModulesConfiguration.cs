using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

namespace Ingredients.Abstractions.Web.Module;

public interface ISharedModulesConfiguration
{
    IServiceCollection AddSharedModuleServices(
        IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment webHostEnvironment);

    Task<WebApplication> ConfigureSharedModule(WebApplication app);

    IEndpointRouteBuilder MapSharedModuleEndpoints(IEndpointRouteBuilder endpoints);
}
