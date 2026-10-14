using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Query;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Api.Controllers.Advertisements.Statuses;

[ApiController]
[Route("api/statuses")]
public class StatusQueriesController 
    : BaseQueryController<StatusDto,
        StatusSearchFilter>
{
    public StatusQueriesController(
        IStatusQueryService queryService
    )
        : base(queryService)
    {
    }
}
