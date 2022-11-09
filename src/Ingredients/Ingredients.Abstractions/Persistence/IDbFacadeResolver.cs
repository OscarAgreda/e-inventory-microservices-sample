using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Ingredients.Abstractions.Persistence;

public interface IDbFacadeResolver
{
    DatabaseFacade Database { get; }
}
