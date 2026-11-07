using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Specification.Currencies.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Query;
using ShagOxServer.Domain.Filters.Specification.Currencies;

namespace ShagOxServer.Api.Controllers.Specification.Currencies.Admin;

[ApiController]
[Route("api/admin/currencies")]
[Authorize(Roles = "Admin")]
public class CurrencyAdminQueriesController
    : BaseAdminQueryController<CurrencyDto,
        CurrencySearchFilter>
{
    public CurrencyAdminQueriesController(
        ICurrencyQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
    }
}
