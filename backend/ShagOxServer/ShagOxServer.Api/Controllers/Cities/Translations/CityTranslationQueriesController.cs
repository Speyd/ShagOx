using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Query;
using ShagOxServer.Domain.Filters.Location.Cities.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
using System.Globalization;

namespace ShagOxServer.Api.Controllers.Cities.Translations;

[ApiController]
[Route("api/cities/translations")]
public  class CityTranslationQueriesController
    : ApiController
{
    private readonly ICityTranslationQueryService _queryService;
    private readonly ILanguageProvider _languageProvider;

    public CityTranslationQueriesController(
        ICityTranslationQueryService queryService,
        ILanguageProvider languageProvider)
    {
        _queryService = queryService;
        _languageProvider = languageProvider;
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int id)
    {
        var result = await _queryService
            .GetByIdAsync(id);

        return result.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] PaginationParams pagination)
    {
        string language = _languageProvider.Language;
        var culture = CultureInfo.GetCultureInfo(language);

        var languageCode = culture.TwoLetterISOLanguageName;
        var result = await _queryService
            .GetPagedAsync(pagination, language);

        return result.ToActionResult();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] CityTranslationSearchFilter filter,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .Search(filter, pagination);

        return result.ToActionResult();
    }
}