using Ingredients.Abstractions.Web.Module;
using EInventory.Services.Identity.Users.Features.GettingUerByEmail;
using EInventory.Services.Identity.Users.Features.GettingUserById;
using EInventory.Services.Identity.Users.Features.RegisteringUser;
using EInventory.Services.Identity.Users.Features.UpdatingUserState;

namespace EInventory.Services.Identity.Users;

internal class UsersConfigs : IModuleConfiguration
{
    public const string Tag = "Users";
    public const string UsersPrefixUri = $"{IdentityModuleConfiguration.IdentityModulePrefixUri}/users";

    public IServiceCollection AddModuleServices(
        IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment webHostEnvironment)
    {
        return services;
    }

    public Task<WebApplication> ConfigureModule(WebApplication app)
    {
        return Task.FromResult(app);
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapRegisterNewUserEndpoint();
        endpoints.MapUpdateUserStateEndpoint();
        endpoints.MapGetUserByIdEndpoint();
        endpoints.MapGetUserByEmailEndpoint();

        return endpoints;
    }
}
