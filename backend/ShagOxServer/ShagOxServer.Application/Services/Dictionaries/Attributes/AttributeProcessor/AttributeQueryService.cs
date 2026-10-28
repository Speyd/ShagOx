using ShagOxServer.Application.DTOs.Dictionaries.Attributes.CategoryAttributes;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeProcessor;
public partial class AttributeQueryService
{
    protected readonly IAttributeDictionaryValueQueryService _valueService;


    public AttributeQueryService(
        IAttributeDictionaryValueQueryService valueService)
    {
        _valueService = valueService;
    }


    public async Task<AttributeDto> ApplyMapperAsync(
        AttributeDefinition attributeDefinition,
        AttributeDictionaryValue value)
    {
        var valueDto = await _valueService.ApplyMapperAsync(value);

        return VariantAttributeMapper.ToSelectDto(
            attributeDefinition,
            valueDto);
    }
}
