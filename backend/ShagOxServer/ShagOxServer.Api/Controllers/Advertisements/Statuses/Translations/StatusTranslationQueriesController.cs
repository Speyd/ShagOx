using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Query;
using ShagOxServer.Domain.Filters.Advertisements.Translations;

namespace ShagOxServer.Api.Controllers.Advertisements.Statuses.Translations;

[ApiController]
[Route("api/statuses/translations")]
public class StatusTranslationQueriesController 
    : BaseTranslationQueryController<StatusTranslationDto,
        StatusTranslationSearchFilter>
{
    public StatusTranslationQueriesController(
        IStatusTranslationQueryService queryService,
        ILanguageProvider languageProvider
    )
        : base(queryService, languageProvider)
    {
    }
}
