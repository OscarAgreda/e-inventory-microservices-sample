using AutoMapper;
using Ingredients.Abstractions.CQRS.Commands;
using Ingredients.Abstractions.CQRS.Queries;
using MediatR;

namespace Ingredients.Web;

[ApiController]
public abstract class BaseController : Controller
{
    protected const string BaseApiPath = Constants.BaseApiPath;
    private IMapper? _mapper;

    private IMediator _mediator;
    private ICommandProcessor _commandProcessor;
    private IQueryProcessor _queryProcessor;

    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

    protected IQueryProcessor? QueryProcessor =>
        _queryProcessor ??= HttpContext.RequestServices.GetService<IQueryProcessor>()!;

    protected ICommandProcessor CommandProcessor =>
        _commandProcessor ??= HttpContext.RequestServices.GetService<ICommandProcessor>()!;

    protected IMapper Mapper => (_mapper ??= HttpContext.RequestServices.GetService<IMapper>())!;
}
