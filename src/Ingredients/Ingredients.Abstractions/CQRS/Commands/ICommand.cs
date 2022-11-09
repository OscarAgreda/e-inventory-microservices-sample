using MediatR;

namespace Ingredients.Abstractions.CQRS.Commands;

public interface ICommand : ICommand<Unit>
{
}

public interface ICommand<out T> : IRequest<T>
    where T : notnull
{
}
