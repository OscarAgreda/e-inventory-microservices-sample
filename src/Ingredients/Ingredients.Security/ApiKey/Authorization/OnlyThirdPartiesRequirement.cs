using Microsoft.AspNetCore.Authorization;

namespace Ingredients.Security.ApiKey.Authorization;

public class OnlyThirdPartiesRequirement : IAuthorizationRequirement
{
}
