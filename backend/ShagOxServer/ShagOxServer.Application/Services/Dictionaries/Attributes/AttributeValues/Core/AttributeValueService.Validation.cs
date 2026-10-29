using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeProcessor;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Validations;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeValues.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeProcessor;
public partial class AttributeValueService
    : IAttributeValueService
{
    private async Task<Result<List<AttributeDictionaryValueDto>>> GetAttributeValuesDto(
        AttributeDefinition attribute,
        JsonElement element,
        Dictionary<long, AttributeDictionaryValue> valuesById)
    {
        if (attribute.Multiple)
        {
            if (element.ValueKind != JsonValueKind.Array)
            {
                return Result< List<AttributeDictionaryValueDto>>.Fail(
                    string.Format(
                        ValidationResources.UnsupportedAttributeType,
                        attribute.Type));
            }

            var values = new List<AttributeDictionaryValueDto>();

            foreach (var item in element.EnumerateArray())
            {
                var result = await GetSingleAttributeValueDto(
                    attribute,
                    item,
                    valuesById);

                if (!result.IsSuccess)
                {
                    return Result<List<AttributeDictionaryValueDto>>
                        .Fail(result.Error);
                }

                values.Add(result.Value!);
            }

            return Result<List<AttributeDictionaryValueDto>>
                .Success(values);
        }

        var singleResult = await GetSingleAttributeValueDto(
            attribute,
            element,
            valuesById);

        return Result<List<AttributeDictionaryValueDto>>.Success(
            new List<AttributeDictionaryValueDto> { 
                singleResult.Value! }
        );
    }
    private async Task<Result<AttributeDictionaryValueDto>> GetSingleAttributeValueDto(
        AttributeDefinition attribute,
        JsonElement element,
        Dictionary<long, AttributeDictionaryValue> valuesById)
    {
        switch (attribute.Type)
        {
            case AttributeType.String:
                return GetStringAttributeDto(
                    attribute,
                    element);

            case AttributeType.Integer:
                return GetIntegerAttributeDto(
                    attribute,
                    element);

            case AttributeType.Decimal:
                return GetDecimalAttributeDto(
                    attribute,
                    element);

            case AttributeType.Boolean:
                return GetBooleanAttributeDto(
                    attribute,
                    element);

            case AttributeType.Select:
                return await GetSelectAttributeDto(
                    attribute,
                    element,
                    valuesById);

            default:
                return Result<AttributeDictionaryValueDto>.Fail(
                    string.Format(
                        ValidationResources.UnsupportedAttributeType,
                        attribute.Type));
        }
    }


    private async Task<Result<AttributeDictionaryValueDto>> GetSelectAttributeDto(
        AttributeDefinition attribute,
        JsonElement element,
        Dictionary<long, AttributeDictionaryValue> valuesById)
    {
        if (attribute.DictionaryId is null)
        {
            _logger.LogError(
                "Select attribute '{AttributeKey}' has no configured dictionary.",
                attribute.Key);

            return Result<AttributeDictionaryValueDto>.NotFound(
                EntityNamesResources.AttributeDictionary);
        }

        if (element.ValueKind != JsonValueKind.Number ||
            !element.TryGetInt64(out var valueId))
        {
            return Result<AttributeDictionaryValueDto>.Fail(
                string.Format(
                    ValidationResources.AttributeMustContainDictionaryValueId,
                    attribute.Key));
        }

        if (!valuesById.TryGetValue(valueId, out var value))
        {
            _logger.LogWarning(
                "Dictionary value not found. Attribute: '{AttributeKey}', " +
                "DictionaryId: {DictionaryId}, ValueId: {ValueId}.",
                attribute.Key,
                attribute.DictionaryId.Value,
                valueId);

            return Result<AttributeDictionaryValueDto>.NotFound(
                EntityNamesResources.AttributeDictionaryValue);
        }

        if (value.DictionaryId != attribute.DictionaryId.Value)
        {
            _logger.LogWarning(
                "Dictionary value {ValueId} does not belong to dictionary {DictionaryId}. " +
                "Attribute: '{AttributeKey}'.",
                valueId,
                attribute.DictionaryId.Value,
                attribute.Key);

            return Result<AttributeDictionaryValueDto>.NotFound(
                EntityNamesResources.AttributeDictionaryValue);
        }

        var resuultDto = await _valueService
            .ApplyMapperAsync(value);

        return Result<AttributeDictionaryValueDto>
            .Success(resuultDto);
    }

    private Result<AttributeDictionaryValueDto> GetStringAttributeDto(
        AttributeDefinition attribute,
        JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.String)
        {
            return Result<AttributeDictionaryValueDto>.Fail(
                string.Format(
                    ValidationResources.AttributeMustBeString,
                    attribute.Key));
        }

        return Result<AttributeDictionaryValueDto>.Success(
            AttributeValueMapper.ToDto(
                element));
    }
    private Result<AttributeDictionaryValueDto> GetIntegerAttributeDto(
        AttributeDefinition attribute,
        JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Number ||
            !element.TryGetInt64(out _))
        {
            return Result<AttributeDictionaryValueDto>.Fail(
                string.Format(
                    ValidationResources.AttributeMustBeInteger,
                    attribute.Key));
        }

        return Result<AttributeDictionaryValueDto>.Success(
            AttributeValueMapper.ToDto(
                element));
    }
    private Result<AttributeDictionaryValueDto> GetDecimalAttributeDto(
        AttributeDefinition attribute,
        JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Number ||
            !element.TryGetDecimal(out _))
        {
            return Result<AttributeDictionaryValueDto>.Fail(
                string.Format(
                    ValidationResources.AttributeMustBeDecimal,
                    attribute.Key));
        }

        return Result<AttributeDictionaryValueDto>.Success(
            AttributeValueMapper.ToDto(
                element));
    }
    private Result<AttributeDictionaryValueDto> GetBooleanAttributeDto(
        AttributeDefinition attribute,
        JsonElement element)
    {
        if (element.ValueKind is not JsonValueKind.True &&
            element.ValueKind is not JsonValueKind.False)
        {
            return Result<AttributeDictionaryValueDto>.Fail(
                string.Format(
                    ValidationResources.AttributeMustBeBoolean,
                    attribute.Key));
        }

        return Result<AttributeDictionaryValueDto>.Success(
            AttributeValueMapper.ToDto(
                element));
    }
}
