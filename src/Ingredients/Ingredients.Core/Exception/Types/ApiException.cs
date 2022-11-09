using System.Net;

namespace Ingredients.Core.Exception.Types;

public class ApiException : CustomException
{
    public ApiException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
    {
        StatusCode = statusCode;
    }
}
