using Ingredients.Abstractions.CQRS.Commands;

namespace Ingredients.Core.CQRS.Commands;

public abstract record TxInternalCommand : InternalCommand, ITxInternalCommand;
