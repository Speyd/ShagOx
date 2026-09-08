using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Translations.Query;
using ShagOxServer.Domain.Filters.Dictionaries.AttributeDefinitions.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.AttributeDefinitions.Translations;

[ApiController]
[Route("api/attributes/translations")]
public class AttributeDefinitionTranslationQueriesController
    : ApiController
{
    private readonly IAttributeDefinitionTranslationQueryService _queryService;
    private readonly ILanguageProvider _languageProvider;

    public AttributeDefinitionTranslationQueriesController(
        IAttributeDefinitionTranslationQueryService queryService,
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
        [FromQuery] AttributeDefinitionTranslationSearchFilter filter,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .Search(filter, pagination);

        return result.ToActionResult();
    }
}