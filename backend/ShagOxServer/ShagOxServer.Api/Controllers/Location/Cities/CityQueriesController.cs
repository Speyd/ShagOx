using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Location.Cities.Query;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Query;
using ShagOxServer.Domain.Filters.Location.Cities;

namespace ShagOxServer.Api.Controllers.Location.Cities;

[ApiController]
[Route("api/cities")]
public class CityQueriesController 
    : BaseQueryController<CityDto,
        CitySearchFilter>
{
    public CityQueriesController(
        ICityQueryService queryService
    )
        : base(queryService)
    {
    }
}
