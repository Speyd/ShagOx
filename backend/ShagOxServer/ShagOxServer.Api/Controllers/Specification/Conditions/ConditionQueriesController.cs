using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Specification.Conditions.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Query;
using ShagOxServer.Domain.Filters.Specification.Conditions;

namespace ShagOxServer.Api.Controllers.Specification.Conditions;

[ApiController]
[Route("api/conditions")]
public class ConditionQueriesController 
    : BaseQueryController<ConditionDto,
        ConditionSearchFilter>
{
    public ConditionQueriesController(
        IConditionQueryService queryService
    )
        : base(queryService)
    {
    }
}
