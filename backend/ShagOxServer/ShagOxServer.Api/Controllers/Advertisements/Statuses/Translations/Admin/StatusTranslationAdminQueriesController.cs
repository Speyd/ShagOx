using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Translations;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Filters.Advertisements.Translations;

namespace ShagOxServer.Api.Controllers.Advertisements.Statuses.Translations.Admin;

[ApiController]
[Route("api/admin/statuses/translations")]
[Authorize(Roles = "Admin")]
public class StatusTranslationAdminQueriesController
    : BaseTranslationQueryController<StatusTranslationDto,
        StatusTranslationSearchFilter>
{
    public StatusTranslationAdminQueriesController(
        IStatusTranslationQueryService queryService,
        ILanguageProvider languageProvider,
        IUserContext userContext
    )
        : base(queryService, languageProvider, userContext)
    {
    }
}
