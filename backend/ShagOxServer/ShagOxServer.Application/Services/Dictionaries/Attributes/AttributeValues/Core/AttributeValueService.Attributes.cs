using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeProcessor;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Validations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeProcessor;
public partial class AttributeValueService
    : IAttributeValueService
{
    public async Task<Result<List<AttributeDto>>> GetAttributesAsync(
        JsonDocument variant,
        long categoryId,
        CancellationToken cancellationToken = default)
    {
        var jsonAttributes = variant
            .RootElement
            .EnumerateObject()
            .ToList();

        if (jsonAttributes.Count == 0)
        {
            return Result<List<AttributeDto>>.
                Success([]);
        }

        var definitionsResult = await GetAttributeDefinitionsAsync(
            categoryId,
            jsonAttributes);

        if (!definitionsResult.IsSuccess)
        {
            return Result<List<AttributeDto>>.Fail(
                definitionsResult.Error);
        }

        var definitionsByKey = definitionsResult.Value!;

        var valueIdsResult = GetDictionaryValueIds(
            jsonAttributes,
            definitionsByKey);

        if (!valueIdsResult.IsSuccess)
        {
            return Result<List<AttributeDto>>.Fail(
                valueIdsResult.Error);
        }

        var values = await _valueRepository.GetByIdsAsync(
            valueIdsResult.Value!.Distinct(),
            cancellationToken);

        var valuesById = values.ToDictionary(x => x.Id);

        return await MapAttributes(
            jsonAttributes,
            definitionsByKey,
            valuesById);
    }

    private async Task<Result<Dictionary<string, AttributeDefinition>>>
        GetAttributeDefinitionsAsync(
            long categoryId,
            List<JsonProperty> jsonAttributes)
    {
        var keys = jsonAttributes
            .Select(x => x.Name)
            .Distinct()
            .ToList();

        var definitions = await _attributeRepository.GetByKeysAsync(
            categoryId,
            keys);

        var definitionsByKey = definitions
            .ToDictionary(x => x.Key);

        foreach (var key in keys)
        {
            if (!definitionsByKey.ContainsKey(key))
            {
                return Result<Dictionary<string, AttributeDefinition>>.NotFound(
                    EntityNamesResources.AttributeDefinition);
            }
        }

        return Result<Dictionary<string, AttributeDefinition>>.Success(
            definitionsByKey);
    }

    private Result<List<long>> GetDictionaryValueIds(
    List<JsonProperty> jsonAttributes,
    Dictionary<string, AttributeDefinition> definitionsByKey)
    {
        var valueIds = new List<long>();

        foreach (var item in jsonAttributes)
        {
            var definition = definitionsByKey[item.Name];

            if (definition.Type != AttributeType.Select)
            {
                continue;
            }

            if (definition.DictionaryId is null)
            {
                return Result<List<long>>.NotFound(
                    EntityNamesResources.AttributeDictionary);
            }

            if (definition.Multiple &&
                item.Value.ValueKind == JsonValueKind.Array)
            {
                foreach (var element in item.Value.EnumerateArray())
                {
                    if (element.ValueKind != JsonValueKind.Number ||
                        !element.TryGetInt64(out var valueId))
                    {
                        return Result<List<long>>.Fail(
                            string.Format(
                                ValidationResources.AttributeMustContainDictionaryValueId,
                                definition.Key));
                    }

                    valueIds.Add(valueId);
                }
            }
            else
            {
                if (item.Value.ValueKind != JsonValueKind.Number ||
                    !item.Value.TryGetInt64(out var valueId))
                {
                    return Result<List<long>>.Fail(
                        string.Format(
                            ValidationResources.AttributeMustContainDictionaryValueId,
                            definition.Key));
                }

                valueIds.Add(valueId);
            }
        }

        return Result<List<long>>.Success(valueIds);
    }

    private async Task<Result<List<AttributeDto>>> MapAttributes(
        List<JsonProperty> jsonAttributes,
        Dictionary<string, AttributeDefinition> definitionsByKey,
        Dictionary<long, AttributeDictionaryValue> valuesById)
    {
        var result = new List<AttributeDto>();

        foreach (var item in jsonAttributes)
        {
            var definition = definitionsByKey[item.Name];

            var valueDtos = await GetAttributeValuesDto(
                definition,
                item.Value,
                valuesById);

            if (!valueDtos.IsSuccess)
            {
                return Result<List<AttributeDto>>.Fail(
                    valueDtos.Error);
            }

            var attributeDto = await _attributeService
                .ApplyMapperAsync(definition);

            result.Add(new AttributeDto(
                attributeDto,
                valueDtos.Value));
        }

        return Result<List<AttributeDto>>
            .Success(result);
    }
}
