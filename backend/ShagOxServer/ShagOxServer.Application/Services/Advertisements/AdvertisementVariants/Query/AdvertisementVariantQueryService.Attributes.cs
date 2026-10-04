using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Validations;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Query;
public partial class AdvertisementVariantQueryService
{
    public async Task<Result<List<VariantAttributeDto>>> GetVariantAttributeAsync(
        AdvertisementVariant variant)
    {
        var jsonAttributes = variant.Attributes
            .RootElement
            .EnumerateObject()
            .ToList();

        if (jsonAttributes.Count == 0)
        {
            return Result<List<VariantAttributeDto>>.Success([]);
        }

        var definitionsResult = await GetAttributeDefinitionsAsync(
            variant,
            jsonAttributes);

        if (!definitionsResult.IsSuccess)
        {
            return Result<List<VariantAttributeDto>>.Fail(
                definitionsResult.Error);
        }

        var definitionsByKey = definitionsResult.Value!;

        var valueIdsResult = GetDictionaryValueIds(
            jsonAttributes,
            definitionsByKey);

        if (!valueIdsResult.IsSuccess)
        {
            return Result<List<VariantAttributeDto>>.Fail(
                valueIdsResult.Error);
        }

        var values = await _valueRepository.GetByIdsAsync(
            valueIdsResult.Value!.Distinct());

        var valuesById = values.ToDictionary(x => x.Id);

        return await MapAttributes(
            jsonAttributes,
            definitionsByKey,
            valuesById);
    }

    private async Task<Result<Dictionary<string, AttributeDefinition>>>
        GetAttributeDefinitionsAsync(
            AdvertisementVariant variant,
            List<JsonProperty> jsonAttributes)
    {
        var keys = jsonAttributes
            .Select(x => x.Name)
            .Distinct()
            .ToList();

        var definitions = await _attributeRepository.GetByKeysAsync(
            variant.Advertisement.CategoryId,
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

        return Result<List<long>>.Success(valueIds);
    }

    private async Task<Result<List<VariantAttributeDto>>> MapAttributes(
        List<JsonProperty> jsonAttributes,
        Dictionary<string, AttributeDefinition> definitionsByKey,
        Dictionary<long, AttributeDictionaryValue> valuesById)
    {
        var result = new List<VariantAttributeDto>();

        foreach (var item in jsonAttributes)
        {
            var definition = definitionsByKey[item.Name];

            var attributeResult = await GetVariantAttributeDto(
                definition,
                item.Value,
                valuesById);

            if (!attributeResult.IsSuccess)
            {
                return Result<List<VariantAttributeDto>>.Fail(
                    attributeResult.Error);
            }

            result.Add(attributeResult.Value!);
        }

        return Result<List<VariantAttributeDto>>.Success(result);
    }
}