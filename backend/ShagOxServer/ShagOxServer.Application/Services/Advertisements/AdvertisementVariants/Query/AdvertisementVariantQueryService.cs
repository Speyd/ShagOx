using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Query;
public partial class AdvertisementVariantQueryService
    : BaseQueryService<
        AdvertisementVariantDto,
        AdvertisementVariant,
        AdvertisementVariantSearchFilter
        >,
    IAdvertisementVariantQueryService
{
    protected readonly IAdvertisementVariantQueryRepository _variantRepository;
    protected readonly IAttributeDefinitionQueryRepository _attributeRepository;
    protected readonly IAttributeDictionaryValueQueryRepository _valueRepository;

    protected readonly ILogger<AdvertisementVariantQueryService> _logger;


    public AdvertisementVariantQueryService(
        IAdvertisementVariantQueryRepository variantRepository,
        IAttributeDefinitionQueryRepository attributeRepository,
        IAttributeDictionaryValueQueryRepository valueRepository,
        ILogger<AdvertisementVariantQueryService> logger
    )
        : base(variantRepository)
    {
        _variantRepository = variantRepository;
        _attributeRepository = attributeRepository;
        _valueRepository = valueRepository;
        _logger = logger;
    }


    public override async Task<AdvertisementVariantDto> ApplyMapperAsync(
        AdvertisementVariant entity)
    {
        var attributesResult = await GetVariantAttributeAsync(entity);

        if (!attributesResult.IsSuccess)
        {
            throw new InvalidOperationException(attributesResult.Error);
        }

        return AdvertisementVariantMapper.ToDto(
            entity,
            attributesResult.Value!);
    }
}