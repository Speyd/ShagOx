using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.DTOs.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.Application.Services.Advertisements.Core.Mapping;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Advertisements.Core.Query;
public class AdvertisementQueryService 
    : BaseQueryService<
        AdvertisementDto, 
        Advertisement, 
        AdvertisementSearchFilter>,
      IAdvertisementQueryService
{
    private readonly IAdvertisementQueryRepository _advertisementRepository;
    private readonly IAdvertisementVariantQueryService _variantService;



    public AdvertisementQueryService(
        IAdvertisementQueryRepository advertisementRepository,
        IAdvertisementVariantQueryService variantService
    )
        : base(advertisementRepository)
    {
        _advertisementRepository = advertisementRepository;
        _variantService = variantService;
    }


    protected override async Task<AdvertisementDto> ApplyMapperAsync(
        Advertisement entity)
    {
        var attributesByVariantId =
    new Dictionary<long, List<VariantAttributeDto>>();

        foreach (var variant in entity.Variants)
        {
            var result = await _variantService
                .GetVariantAttributeAsync(variant);

            if (!result.IsSuccess)
            {
                throw new InvalidOperationException(
                    result.Error);
            }

            attributesByVariantId[variant.Id] =
                result.Value ?? [];
        }

        return AdvertisementMapper.ToDto(
            entity,
            attributesByVariantId);
    }

    public async Task<Result<PagedResult<AdvertisementDto>>> GetBySellerAsync(
        long userId,
        PaginationParams pagination)
    {
        var advert = await _advertisementRepository
            .GetBySellerAsync(userId, pagination);

        return await advert.ToResultPagedAsync(ApplyMapperAsync);
    }
    public async Task<Result<PagedResult<AdvertisementDto>>> GetPurchasedByUserAsync(
        long userId,
        PaginationParams pagination)
    {
        var advert = await _advertisementRepository
            .GetPurchasedByUserAsync(userId, pagination);

        return await advert.ToResultPagedAsync(ApplyMapperAsync);
    }
}