using System.Data;

namespace Ingredients.Abstractions.Persistence.EfCore;

public interface IConnectionFactory : IDisposable
{
    IDbConnection GetOrCreateConnection();
}
