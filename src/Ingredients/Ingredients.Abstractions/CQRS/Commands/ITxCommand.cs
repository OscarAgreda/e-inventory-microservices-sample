using Ingredients.Abstractions.Persistence;
using MediatR;

namespace Ingredients.Abstractions.CQRS.Commands;

public interface ITxCommand : ITxCommand<Unit>
{
}

public interface ITxCommand<out T> : ICommand<T>, ITxRequest
    where T : notnull
{
}
