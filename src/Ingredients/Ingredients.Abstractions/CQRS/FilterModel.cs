namespace Ingredients.Abstractions.CQRS;

public record FilterModel(string FieldName, string Comparision, string FieldValue);
