using Ingredients.Core.Exception.Types;

namespace EInventory.Services.Customers.Customers.Exceptions.Domain;

public class InvalidNationalityException : BadRequestException
{
    public string Nationality { get; }

    public InvalidNationalityException(string nationality) : base($"Nationality: '{nationality}' is invalid.")
    {
        Nationality = nationality;
    }
}
