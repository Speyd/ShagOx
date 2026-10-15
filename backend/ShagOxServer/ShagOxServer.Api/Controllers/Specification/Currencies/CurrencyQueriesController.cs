using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Specification.Currencies.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Query;
using ShagOxServer.Domain.Filters.Specification.Currencies;

namespace ShagOxServer.Api.Controllers.Specification.Currencies;

[ApiController]
[Route("api/currencies")]
public class CurrencyQueriesController 
    : BaseQueryController<CurrencyDto,
        CurrencySearchFilter>
{
    public CurrencyQueriesController(
        ICurrencyQueryService queryService
    )
        : base(queryService)
    {
    }
}
