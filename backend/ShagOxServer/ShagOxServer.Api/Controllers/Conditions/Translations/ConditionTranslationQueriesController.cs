using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Query;
using ShagOxServer.Domain.Filters.Specification.Conditions.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
using System.Globalization;

namespace ShagOxServer.Api.Controllers.Conditions.Translations;

[ApiController]
[Route("api/conditions/translations")]
public class ConditionTranslationQueriesController
    : ApiController
{
    private readonly IConditionTranslationQueryService _queryService;
    private readonly ILanguageProvider _languageProvider;

    public ConditionTranslationQueriesController(
        IConditionTranslationQueryService queryService,
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
        string language = _languageProvider
            .GetTwoLetterISOName();

        var result = await _queryService
            .GetPagedAsync(pagination, language);

        return result.ToActionResult();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] ConditionTranslationSearchFilter filter,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .Search(filter, pagination);

        return result.ToActionResult();
    }
}