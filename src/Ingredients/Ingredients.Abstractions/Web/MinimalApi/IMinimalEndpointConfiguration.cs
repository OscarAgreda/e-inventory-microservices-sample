using Microsoft.AspNetCore.Routing;

namespace Ingredients.Abstractions.Web.MinimalApi;

public interface IMinimalEndpointConfiguration
{
    IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder);
}
