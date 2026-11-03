using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Translations;
using ShagOxServer.Application.DTOs.Location.Regions.Translations.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Query;
using ShagOxServer.Domain.Filters.Location.Regions.Translations;

namespace ShagOxServer.Api.Controllers.Location.Regions.Translations.Admin;
[ApiController]
[Route("api/admin/regions/translations")]
public class RegionTranslationAdminQueriesController
    : BaseTranslationAdminQueryController<RegionTranslationDto,
        RegionTranslationSearchFilter>
{
    public RegionTranslationAdminQueriesController(
        IRegionTranslationQueryService queryService,
        ILanguageProvider languageProvider
    )
        : base(queryService, languageProvider)
    {
    }
}
