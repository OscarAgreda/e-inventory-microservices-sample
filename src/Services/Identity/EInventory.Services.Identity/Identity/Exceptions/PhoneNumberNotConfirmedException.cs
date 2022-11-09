using Ingredients.Core.Exception.Types;

namespace EInventory.Services.Identity.Identity.Exceptions;

public class PhoneNumberNotConfirmedException : BadRequestException
{
    public PhoneNumberNotConfirmedException(string phone) : base($"The phone number '{phone}' is not confirmed yet.")
    {
    }
}
