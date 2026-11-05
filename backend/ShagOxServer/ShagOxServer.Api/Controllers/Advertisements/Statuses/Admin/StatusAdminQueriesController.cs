using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Api.Controllers.Advertisements.Statuses.Admin;
[ApiController]
[Route("api/admin/statuses")]
[Authorize(Roles = "Admin")]
public class StatusAdminQueriesController
    : BaseAdminQueryController<StatusDto,
        StatusSearchFilter>
{
    public StatusAdminQueriesController(
        IStatusQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
    }
}
