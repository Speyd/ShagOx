using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Specification.Conditions.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Query;
using ShagOxServer.Domain.Filters.Specification.Conditions;

namespace ShagOxServer.Api.Controllers.Specification.Conditions.Admin;

[ApiController]
[Route("api/admin/conditions")]
[Authorize(Roles = "Admin")]
public class ConditionAdminQueriesController
    : BaseAdminQueryController<ConditionDto,
        ConditionSearchFilter>
{
    public ConditionAdminQueriesController(
        IConditionQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
    }
}
