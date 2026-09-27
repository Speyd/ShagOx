using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Dictionaries.Attributes.AttributeDictionaryValues;
[ApiController]
[Route("api/attribute-dictionary-values")]
public class AttributeDictionaryValueQueriesController
    : ApiController
{
    private readonly IAttributeDictionaryValueQueryService _queryService;


    public AttributeDictionaryValueQueriesController(
        IAttributeDictionaryValueQueryService queryService)
    {
        _queryService = queryService;
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(
        [FromRoute] long id)
    {
        var result = await _queryService
            .GetByIdAsync(id);

        return result.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .GetPagedAsync(pagination);

        return result.ToActionResult();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] AttributeDictionaryValueSearchFilter filter,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .Search(filter, pagination);

        return result.ToActionResult();
    }
}