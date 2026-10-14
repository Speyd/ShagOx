using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Location.Cities.Translations;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Query;
using ShagOxServer.Domain.Filters.Location.Cities.Translations;

namespace ShagOxServer.Api.Controllers.Location.Cities.Translations;

[ApiController]
[Route("api/cities/translations")]
public  class CityTranslationQueriesController
    : BaseTranslationQueryController<CityTranslationDto,
        CityTranslationSearchFilter>
{
    public CityTranslationQueriesController(
        ICityTranslationQueryService queryService,
        ILanguageProvider languageProvider
    )
        : base(queryService, languageProvider)
    {
    }
}
