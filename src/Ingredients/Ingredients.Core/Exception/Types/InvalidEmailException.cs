namespace Ingredients.Core.Exception.Types;

public class InvalidEmailException : BadRequestException
{
    public string Email { get; }

    public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
    {
        Email = email;
    }
}
