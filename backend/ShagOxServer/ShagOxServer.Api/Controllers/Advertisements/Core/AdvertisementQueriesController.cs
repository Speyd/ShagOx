using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Advertisements.Core;

[ApiController]
[Route("api/advertisements")]
public class AdvertisementQueriesController 
    : BaseQueryController<AdvertisementDto,
        AdvertisementSearchFilter>
{
    private readonly IAdvertisementQueryService _advertQueryService;


    public AdvertisementQueriesController(
        IAdvertisementQueryService queryService
    )
        : base(queryService)
    {
        _advertQueryService = queryService;
    }


    [HttpGet("short")]
    public async Task<IActionResult> GetShortPaged(
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var result = await _advertQueryService
            .GetShortPagedAsync(pagination,
                cancellationToken);

        return result.ToActionResult();
    }

    [HttpGet("seller{userId:long}")]
    public async Task<IActionResult> GetBySeller(
        [FromRoute] long userId,
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var result = await _advertQueryService
            .GetBySellerAsync(userId, pagination,
                cancellationToken);

        return result.ToActionResult();
    }

    [HttpGet("search")]
    public override async Task<IActionResult> Search(
        [FromQuery] AdvertisementSearchFilter filter,
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        filter = AdvertisementSearchFilter.Create(filter);

        var result = await _advertQueryService
            .SearchAsync(filter, pagination,
                cancellationToken);

        return result.ToActionResult();
    }

    [HttpGet("short-search")]
    public async Task<IActionResult> ShortSearch(
        [FromQuery] AdvertisementSearchFilter filter,
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        filter = AdvertisementSearchFilter.Create(filter);

        var result = await _advertQueryService
            .ShortSearchAsync(filter, pagination,
                cancellationToken);

        return result.ToActionResult();
    }
}
