using Ingredients.Abstractions.Persistence;
using MediatR;

namespace Ingredients.Abstractions.CQRS.Commands;

public interface ITxUpdateCommand<out TResponse> : IUpdateCommand<TResponse>, ITxRequest
    where TResponse : notnull
{
}

public interface ITxUpdateCommand : ITxUpdateCommand<Unit>
{
}
