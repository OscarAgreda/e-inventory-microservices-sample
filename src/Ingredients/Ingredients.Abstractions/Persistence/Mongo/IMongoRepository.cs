using Ingredients.Abstractions.Domain;

namespace Ingredients.Abstractions.Persistence.Mongo;

public interface IMongoRepository<TEntity, in TId> : IRepository<TEntity, TId>
    where TEntity : class, IHaveIdentity<TId>
{
}
