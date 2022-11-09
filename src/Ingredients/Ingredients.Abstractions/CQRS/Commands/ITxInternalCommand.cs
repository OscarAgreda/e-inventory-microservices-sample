using Ingredients.Abstractions.Persistence;

namespace Ingredients.Abstractions.CQRS.Commands;

public interface ITxInternalCommand : IInternalCommand, ITxRequest
{
}
