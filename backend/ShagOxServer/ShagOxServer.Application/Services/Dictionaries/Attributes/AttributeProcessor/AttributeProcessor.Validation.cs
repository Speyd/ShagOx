using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.CategoryAttributes;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeProcessor;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Validations;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeProcessor;
public partial class AttributeProcessor
    : IAttributeProcessor
{
    private async Task<Result<AttributeDto>> GetVariantAttributeDto(
    AttributeDefinition attribute,
    JsonElement element,
    Dictionary<long, AttributeDictionaryValue> valuesById)
    {
        if (attribute.Multiple)
        {
            if (element.ValueKind != JsonValueKind.Array)
            {
                return Result<AttributeDto>.Fail(
                    string.Format(
                        ValidationResources.UnsupportedAttributeType,
                        attribute.Type));
            }

            var values = new List<AttributeDictionaryValueDto>();

            foreach (var item in element.EnumerateArray())
            {
                var result = await GetSingleVariantAttributeDto(
                    attribute,
                    item,
                    valuesById);

                if (!result.IsSuccess)
                    return result;

                values.AddRange(result.Value!.Values);
            }

            return Result<AttributeDto>.Success(
                new AttributeDto(
                    attribute.Id,
                    attribute.Key,
                    values));
        }

        return await GetSingleVariantAttributeDto(
            attribute,
            element,
            valuesById);
    }
    private async Task<Result<AttributeDto>> GetSingleVariantAttributeDto(
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
                return Result<AttributeDto>.Fail(
                    string.Format(
                        ValidationResources.UnsupportedAttributeType,
                        attribute.Type));
        }
    }


    private async Task<Result<AttributeDto>> GetSelectAttributeDto(
        AttributeDefinition attribute,
        JsonElement element,
        Dictionary<long, AttributeDictionaryValue> valuesById)
    {
        if (attribute.DictionaryId is null)
        {
            _logger.LogError(
                "Select attribute '{AttributeKey}' has no configured dictionary.",
                attribute.Key);

            return Result<AttributeDto>.NotFound(
                EntityNamesResources.AttributeDictionary);
        }

        if (element.ValueKind != JsonValueKind.Number ||
            !element.TryGetInt64(out var valueId))
        {
            return Result<AttributeDto>.Fail(
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

            return Result<AttributeDto>.NotFound(
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

            return Result<AttributeDto>.NotFound(
                EntityNamesResources.AttributeDictionaryValue);
        }
        var resuultDto = await _variantAttributeService
            .ApplyMapperAsync(attribute,value);

        return Result<AttributeDto>.Success(resuultDto);
    }

    private Result<AttributeDto> GetStringAttributeDto(
        AttributeDefinition attribute,
        JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.String)
        {
            return Result<AttributeDto>.Fail(
                string.Format(
                    ValidationResources.AttributeMustBeString,
                    attribute.Key));
        }

        return Result<AttributeDto>.Success(
            VariantAttributeMapper.ToDto(
                attribute,
                element));
    }
    private Result<AttributeDto> GetIntegerAttributeDto(
        AttributeDefinition attribute,
        JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Number ||
            !element.TryGetInt64(out _))
        {
            return Result<AttributeDto>.Fail(
                string.Format(
                    ValidationResources.AttributeMustBeInteger,
                    attribute.Key));
        }

        return Result<AttributeDto>.Success(
            VariantAttributeMapper.ToDto(
                attribute,
                element));
    }
    private Result<AttributeDto> GetDecimalAttributeDto(
        AttributeDefinition attribute,
        JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Number ||
            !element.TryGetDecimal(out _))
        {
            return Result<AttributeDto>.Fail(
                string.Format(
                    ValidationResources.AttributeMustBeDecimal,
                    attribute.Key));
        }

        return Result<AttributeDto>.Success(
            VariantAttributeMapper.ToDto(
                attribute,
                element));
    }
    private Result<AttributeDto> GetBooleanAttributeDto(
        AttributeDefinition attribute,
        JsonElement element)
    {
        if (element.ValueKind is not JsonValueKind.True &&
            element.ValueKind is not JsonValueKind.False)
        {
            return Result<AttributeDto>.Fail(
                string.Format(
                    ValidationResources.AttributeMustBeBoolean,
                    attribute.Key));
        }

        return Result<AttributeDto>.Success(
            VariantAttributeMapper.ToDto(
                attribute,
                element));
    }
}
