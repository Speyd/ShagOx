using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Query;
using ShagOxServer.Domain.Filters.Advertisements.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Statuses.Translations;

[ApiController]
[Route("api/statuses/translations")]
public class StatusTranslationQueriesController : ApiController
{
    private readonly IStatusTranslationQueryService _queryService;
    private readonly ILanguageProvider _languageProvider;

    public StatusTranslationQueriesController(
        IStatusTranslationQueryService queryService,
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

        var result = await _queryService
            .GetPagedAsync(pagination, language);

        return result.ToActionResult();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] StatusTranslationSearchFilter filter,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .Search(filter, pagination);

        return result.ToActionResult();
    }
}