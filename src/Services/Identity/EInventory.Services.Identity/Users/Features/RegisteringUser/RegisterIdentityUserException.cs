using Ingredients.Core.Exception.Types;

namespace EInventory.Services.Identity.Users.Features.RegisteringUser;

public class RegisterIdentityUserException : BadRequestException
{
    public RegisterIdentityUserException(string error) : base(error)
    {
    }
}
