using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Translations;
using ShagOxServer.Application.DTOs.Location.Cities.Translations.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Query;
using ShagOxServer.Domain.Filters.Location.Cities.Translations;

namespace ShagOxServer.Api.Controllers.Location.Cities.Translations.Admin;

[ApiController]
[Route("api/admin/cities/translations")]
[Authorize(Roles = "Admin")]
public class CityTranslationAdminQueriesController
    : BaseTranslationAdminQueryController<CityTranslationDto,
        CityTranslationSearchFilter>
{
    public CityTranslationAdminQueriesController(
        ICityTranslationQueryService queryService,
        ILanguageProvider languageProvider,
        IUserContext userContext
    )
        : base(queryService, languageProvider, userContext)
    {
    }
}
