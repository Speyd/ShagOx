using ShagOxServer.Application.DTOs.Dictionaries.Attributes.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeValues;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeValues.Query;
public partial class AttributeValueQueryService
    : IAttributeValueQueryService
{
    private readonly IAttributeDefinitionQueryService _attributeService;
    private readonly IAttributeDictionaryValueQueryService _valueService;


    public AttributeValueQueryService(
        IAttributeDefinitionQueryService attributeService,
        IAttributeDictionaryValueQueryService valueService)
    {
        _attributeService = attributeService;
        _valueService = valueService;
    }


    public async Task<Result<List<AttributeDto>>> GetValuesByCategoryAsync(
        long categoryId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default)
    {
        var definitions = await _attributeService
            .GetByCategoryAsync(categoryId, cancellationToken);

        if (!definitions.IsSuccess ||
            definitions.Value is null)
        {
            return Result<List<AttributeDto>>
                .Fail(definitions.Error);
        }

        var result = new List<AttributeDto>(
            definitions.Value!.Count);

        foreach (var definition in definitions.Value!)
        {
            if (definition.DictionaryId is null)
            {
                result.Add(new AttributeDto(
                     Attribute: definition,
                     Values: null
                ));

                continue;
            }

            var dictionaryResult = await _valueService
                .GetByDictionaryAsync(
                    definition.DictionaryId.Value,
                    pagination,
                    cancellationToken);

            if (!dictionaryResult.IsSuccess)
            {
                return Result<List<AttributeDto>>
                    .Fail(dictionaryResult.Error);
            }

            result.Add(new AttributeDto(
                Attribute: definition,
                Values: dictionaryResult.Value?.Items
            ));
        }

        return Result<List<AttributeDto>>
            .Success(result);
    }
}
