using MediatR;

namespace Ingredients.Abstractions.CQRS.Commands;

public interface IUpdateCommand : IUpdateCommand<Unit>
{
}

public interface IUpdateCommand<out TResponse> : ICommand<TResponse>
    where TResponse : notnull
{
}
