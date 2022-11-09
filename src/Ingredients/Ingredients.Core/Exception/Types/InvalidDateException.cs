namespace Ingredients.Core.Exception.Types;

public class InvalidDateException : BadRequestException
{
    public DateTime Date { get; }

    public InvalidDateException(DateTime date) : base($"Date: '{date}' is invalid.")
    {
        Date = date;
    }
}
