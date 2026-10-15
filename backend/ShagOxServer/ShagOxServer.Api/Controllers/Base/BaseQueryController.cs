using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Base;

[ApiController]
public abstract class BaseQueryController<TDto, TFilter>
    : ApiController
    where TDto : BaseDto
    where TFilter : BaseFilter
{
    protected readonly IQueryOperations<TDto, TFilter>
        _queryService;


    protected BaseQueryController(
       IQueryOperations<TDto, TFilter> queryService)
    {
        _queryService = queryService;
    }


    [HttpGet("{id:long}")]
    public virtual async Task<IActionResult> GetById(
       [FromRoute] long id,
        CancellationToken cancellationToken)
    {
        var result = await _queryService
            .GetByIdAsync(id, cancellationToken);

        return result.ToActionResult();
    }

    [HttpGet("search")]
    public virtual async Task<IActionResult> Search(
       [FromQuery] TFilter filter,
       [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var result = await _queryService
            .SearchAsync(filter, pagination,
                cancellationToken);

        return result.ToActionResult();
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetPaged(
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var result = await _queryService
            .GetPagedAsync(pagination);

        return result.ToActionResult();
    }
}
