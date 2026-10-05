using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Validations;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Query;

public partial class AdvertisementVariantQueryService
{
    private async Task<Result<VariantAttributeDto>> GetVariantAttributeDto(
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
                return Result<VariantAttributeDto>.Fail(
                    string.Format(
                        ValidationResources.UnsupportedAttributeType,
                        attribute.Type));
        }
    }


    private async Task<Result<VariantAttributeDto>> GetSelectAttributeDto(
        AttributeDefinition attribute,
        JsonElement element,
        Dictionary<long, AttributeDictionaryValue> valuesById)
    {
        if (attribute.DictionaryId is null)
        {
            _logger.LogError(
                "Select attribute '{AttributeKey}' has no configured dictionary.",
                attribute.Key);

            return Result<VariantAttributeDto>.NotFound(
                EntityNamesResources.AttributeDictionary);
        }

        if (element.ValueKind != JsonValueKind.Number ||
            !element.TryGetInt64(out var valueId))
        {
            return Result<VariantAttributeDto>.Fail(
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

            return Result<VariantAttributeDto>.NotFound(
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

            return Result<VariantAttributeDto>.NotFound(
                EntityNamesResources.AttributeDictionaryValue);
        }
        var resuultDto = await _variantAttributeService
            .ApplyMapperAsync(attribute,value);

        return Result<VariantAttributeDto>.Success(resuultDto);
    }

    private Result<VariantAttributeDto> GetStringAttributeDto(
        AttributeDefinition attribute,
        JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.String)
        {
            return Result<VariantAttributeDto>.Fail(
                string.Format(
                    ValidationResources.AttributeMustBeString,
                    attribute.Key));
        }

        return Result<VariantAttributeDto>.Success(
            VariantAttributeMapper.ToDto(
                attribute,
                element));
    }
    private Result<VariantAttributeDto> GetIntegerAttributeDto(
        AttributeDefinition attribute,
        JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Number ||
            !element.TryGetInt64(out _))
        {
            return Result<VariantAttributeDto>.Fail(
                string.Format(
                    ValidationResources.AttributeMustBeInteger,
                    attribute.Key));
        }

        return Result<VariantAttributeDto>.Success(
            VariantAttributeMapper.ToDto(
                attribute,
                element));
    }
    private Result<VariantAttributeDto> GetDecimalAttributeDto(
        AttributeDefinition attribute,
        JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Number ||
            !element.TryGetDecimal(out _))
        {
            return Result<VariantAttributeDto>.Fail(
                string.Format(
                    ValidationResources.AttributeMustBeDecimal,
                    attribute.Key));
        }

        return Result<VariantAttributeDto>.Success(
            VariantAttributeMapper.ToDto(
                attribute,
                element));
    }
    private Result<VariantAttributeDto> GetBooleanAttributeDto(
        AttributeDefinition attribute,
        JsonElement element)
    {
        if (element.ValueKind is not JsonValueKind.True &&
            element.ValueKind is not JsonValueKind.False)
        {
            return Result<VariantAttributeDto>.Fail(
                string.Format(
                    ValidationResources.AttributeMustBeBoolean,
                    attribute.Key));
        }

        return Result<VariantAttributeDto>.Success(
            VariantAttributeMapper.ToDto(
                attribute,
                element));
    }
}