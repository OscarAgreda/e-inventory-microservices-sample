using Ingredients.Core.Exception.Types;

namespace EInventory.Services.Identity.Identity.Features.VerifyEmail.Exceptions;

public class EmailAlreadyVerifiedException : ConflictException
{
    public EmailAlreadyVerifiedException(string email) : base($"User with email {email} already verified.")
    {
    }
}
