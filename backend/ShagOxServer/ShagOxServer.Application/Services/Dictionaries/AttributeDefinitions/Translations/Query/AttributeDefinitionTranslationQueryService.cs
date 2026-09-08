using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions.Translations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Translations.Mapping;
using ShagOxServer.Application.Services.Location.Cities.Translations.Mapping;
using ShagOxServer.Domain.Filters.Dictionaries.AttributeDefinitions.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Translations.Query;

public class AttributeDefinitionTranslationQueryService
    : IAttributeDefinitionTranslationQueryService
{
    private readonly IAttributeDefinitionTranslationQueryRepository _attributeRepository;


    public AttributeDefinitionTranslationQueryService(
        IAttributeDefinitionTranslationQueryRepository attributeRepository)
    {
        _attributeRepository = attributeRepository;
    }


    public async Task<Result<AttributeDefinitionTranslationDto>> GetByIdAsync(
        int id)
    {
        var city = await _attributeRepository
            .GetByIdAsync(id);

        return city.ToResult(AttributeDefinitionTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<AttributeDefinitionTranslationDto>>> GetPagedAsync(
        PaginationParams pagination,
        string language)
    {
        var cities = await _attributeRepository
            .GetPagedAsync(pagination, language);

        return cities.ToResultPaged(AttributeDefinitionTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<AttributeDefinitionTranslationDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var cities = await _attributeRepository
            .GetPagedAsync(pagination);

        return cities.ToResultPaged(AttributeDefinitionTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<AttributeDefinitionTranslationDto>>> Search(
        AttributeDefinitionTranslationSearchFilter filter,
        PaginationParams pagination)
    {
        var cities = await _attributeRepository
            .Search(filter, pagination);

        return cities.ToResultPaged(AttributeDefinitionTranslationMapper.ToDto);
    }
}
