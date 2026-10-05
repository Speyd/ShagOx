using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Query;
public partial class VariantAttributeQueryService
{
    protected readonly IAttributeDictionaryValueQueryService _valueService;


    public VariantAttributeQueryService(
        IAttributeDictionaryValueQueryService valueService)
    {
        _valueService = valueService;
    }


    public async Task<VariantAttributeDto> ApplyMapperAsync(
        AttributeDefinition attributeDefinition,
        AttributeDictionaryValue value)
    {
        var valueDto = await _valueService.ApplyMapperAsync(value);

        return VariantAttributeMapper.ToSelectDto(
            attributeDefinition,
            valueDto);
    }
}