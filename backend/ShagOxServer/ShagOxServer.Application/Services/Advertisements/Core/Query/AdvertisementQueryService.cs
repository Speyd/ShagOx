using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.DTOs.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
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

    private readonly ICategoryQueryService _categoryService;


    public AdvertisementQueryService(
        IAdvertisementQueryRepository advertisementRepository,
        IAdvertisementVariantQueryService variantService,
        ICategoryQueryService categoryService
    )
        : base(advertisementRepository)
    {
        _advertisementRepository = advertisementRepository;
        _variantService = variantService;
        _categoryService = categoryService;
    }


    public override async Task<AdvertisementDto> ApplyMapperAsync(
        Advertisement entity)
    {
        var attributes = 
            await GetAttributesByVariantIdAsync(entity.Variants);

        var categoryDto = await _categoryService
            .ApplyMapperAsync(entity.Category);

        return AdvertisementMapper.ToDto(
            entity,
            attributes,
            categoryDto);
    }

    private async Task<Dictionary<long, List<VariantAttributeDto>>>
    GetAttributesByVariantIdAsync(
        IEnumerable<AdvertisementVariant> variants)
    {
        var result = new Dictionary<long, List<VariantAttributeDto>>();

        foreach (var variant in variants)
        {
            var attributesResult = await _variantService
                .GetVariantAttributeAsync(variant);

            if (!attributesResult.IsSuccess)
            {
                throw new InvalidOperationException(
                    attributesResult.Error);
            }

            result[variant.Id] =
                attributesResult.Value ?? [];
        }

        return result;
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