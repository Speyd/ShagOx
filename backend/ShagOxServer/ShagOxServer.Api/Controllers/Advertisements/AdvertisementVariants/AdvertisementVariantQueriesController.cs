using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Advertisements.AdvertisementVariants;

[ApiController]
[Route("api/advertisement-variants")]
public class AdvertisementVariantQueriesController
    : ApiController
{
    private readonly IAdvertisementVariantQueryService _queryService;


    public AdvertisementVariantQueriesController(
        IAdvertisementVariantQueryService queryService)
    {
        _queryService = queryService;
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(
        [FromRoute] long id)
    {
        var result = await _queryService
            .GetByIdAsync(id);

        return result.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .GetPagedAsync(pagination);

        return result.ToActionResult();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] AdvertisementVariantSearchFilter filter,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .Search(filter, pagination);

        return result.ToActionResult();
    }
}