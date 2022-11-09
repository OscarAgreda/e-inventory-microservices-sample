using Ingredients.Core.Exception.Types;

namespace EInventory.Services.Identity.Identity.Exceptions;

public class PasswordIsInvalidException : AppException
{
    public PasswordIsInvalidException(string message) : base(message)
    {
    }
}
