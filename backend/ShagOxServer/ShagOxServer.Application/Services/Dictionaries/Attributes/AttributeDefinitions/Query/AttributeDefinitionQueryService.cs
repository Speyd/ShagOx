using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
public class AttributeDefinitionQueryService 
    : BaseQueryService<
        AttributeDefinitionDto,
        AttributeDefinition,
        AttributeDefinitionSearchFilter
        >,
    IAttributeDefinitionQueryService
{
    public readonly IAttributeDefinitionQueryRepository _attributeRepository;

    public AttributeDefinitionQueryService(
        IAttributeDefinitionQueryRepository attributeQueryRepository
    )
        : base(attributeQueryRepository)
    {
        _attributeRepository = attributeQueryRepository;
    }


    protected override async Task<AttributeDefinitionDto> ApplyMapperAsync(
        AttributeDefinition entity)
    {
        return AttributeDefinitionMapper.ToDto(entity);
    }

    public async Task<Result<AttributeDefinitionDto>> GetByKeyAsync(
        string key)
    {
        var attribute = await _attributeRepository
            .GetByKeyAsync(key);

        return await attribute.ToResultAsync(ApplyMapperAsync);
    }
}