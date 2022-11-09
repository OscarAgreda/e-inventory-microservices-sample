using System.Security.Claims;
using Ingredients.Core.Exception.Types;


namespace EInventory.Services.Identity.Identity.Exceptions;

public class InvalidTokenException : BadRequestException
{
    public InvalidTokenException(ClaimsPrincipal? claimsPrincipal) : base("access_token is invalid!")
    {
    }
}
