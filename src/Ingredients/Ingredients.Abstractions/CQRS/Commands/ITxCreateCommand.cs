using Ingredients.Abstractions.Persistence;
using MediatR;

namespace Ingredients.Abstractions.CQRS.Commands;

public interface ITxCreateCommand<out TResponse> : ICommand<TResponse>, ITxRequest
    where TResponse : notnull
{
}

public interface ITxCreateCommand : ITxCreateCommand<Unit>
{
}
