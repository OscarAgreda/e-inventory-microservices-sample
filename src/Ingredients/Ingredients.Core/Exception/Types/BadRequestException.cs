using System.Net;

namespace Ingredients.Core.Exception.Types;

public class BadRequestException : CustomException
{
    public BadRequestException(string message) : base(message)
    {
        StatusCode = HttpStatusCode.NotFound;
    }
}
