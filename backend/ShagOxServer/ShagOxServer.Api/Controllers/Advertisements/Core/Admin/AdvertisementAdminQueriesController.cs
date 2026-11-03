using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Advertisements.Core.Admin;

[ApiController]
[Route("api/admin/advertisements")]
[Authorize(Roles = "Admin")]
public class AdvertisementAdminQueriesController
    : BaseAdminQueryController<AdvertisementDto,
        AdvertisementSearchFilter>
{
    private readonly IAdvertisementQueryService _queryAdvertService;


    public AdvertisementAdminQueriesController(
       IAdvertisementQueryService queryAdvertService
    ) : base(queryAdvertService)
    {
        _queryAdvertService = queryAdvertService;
    }


    [HttpGet("purchases/{userId:long}")]
    public async Task<IActionResult> GetPurchasedAdvertisements(
        [FromRoute] long userId,
        [FromQuery] PaginationParams pagination,
        [FromQuery] EntityStatus status,
        CancellationToken cancellationToken)
    {
        var advertisements = await _queryAdvertService
            .GetPurchasedByUserAsync(userId, pagination,
                cancellationToken, status);

        return advertisements.ToActionResult();
    }
}
