namespace Ingredients.Security.ApiKey;

public interface IGetApiKeyQuery
{
    Task<ApiKey> ExecuteAsync(string providedApiKey);
}
