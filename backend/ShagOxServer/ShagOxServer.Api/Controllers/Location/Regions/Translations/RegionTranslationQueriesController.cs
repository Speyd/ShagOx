using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Location.Regions.Translations;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Query;
using ShagOxServer.Domain.Filters.Location.Regions.Translations;

namespace ShagOxServer.Api.Controllers.Location.Regions.Translations;

[ApiController]
[Route("api/regions/translations")]
public class RegionTranslationQueriesController 
    : BaseTranslationQueryController<RegionTranslationDto,
        RegionTranslationSearchFilter>
{
    public RegionTranslationQueriesController(
        IRegionTranslationQueryService queryService,
        ILanguageProvider languageProvider
    )
        : base(queryService, languageProvider)
    {
    }
}
