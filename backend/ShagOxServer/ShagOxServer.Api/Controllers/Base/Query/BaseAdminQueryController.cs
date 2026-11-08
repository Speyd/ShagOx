using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Base.Query;
[ApiController]
[Authorize(Roles = "Admin")]
public abstract class BaseAdminQueryController<TDto, TFilter>
    : ApiController
    where TDto : BaseDto
    where TFilter : BaseFilter
{
    protected readonly IQueryOperations<TDto, TFilter>
        _queryService;


    protected BaseAdminQueryController(
        IQueryOperations<TDto, TFilter> queryService,
        IUserContext userContext
    )
        : base(userContext)
    {
        _queryService = queryService;
    }


    [HttpGet("count")]
    public virtual async Task<IActionResult> GetCount(
        [FromQuery] EntityStatus status,
        CancellationToken cancellationToken = default)
    {

        var result = await _queryService
            .GetCountAsync(cancellationToken, status);
        
        return result.ToActionResult();
    }

    [HttpGet("{id:long}")]
    public virtual async Task<IActionResult> GetById(
       [FromRoute] long id,
       [FromQuery] EntityStatus status,
        CancellationToken cancellationToken)
    {
        var result = await _queryService
            .GetByIdAsync(id, cancellationToken, status);

        return result.ToActionResult();
    }

    [HttpGet("search")]
    public virtual async Task<IActionResult> Search(
       [FromQuery] TFilter filter,
       [FromQuery] PaginationParams pagination,
       [FromQuery] EntityStatus status,
        CancellationToken cancellationToken)
    {
        var result = await _queryService
            .SearchAsync(filter, pagination,
                cancellationToken, status);

        return result.ToActionResult();
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetPaged(
        [FromQuery] PaginationParams pagination,
        [FromQuery] EntityStatus status,
        CancellationToken cancellationToken)
    {
        var result = await _queryService
            .GetPagedAsync(pagination, 
                cancellationToken, status);

        return result.ToActionResult();
    }
}
