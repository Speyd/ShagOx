using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.DTOs.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
using ShagOxServer.Application.Services.Advertisements.Core.Mapping;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Caches.Advertisements;
using ShagOxServer.Domain.Caches;
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
    private readonly IStatusQueryService _statusService;

    private readonly ILanguageProvider _language;


    public AdvertisementQueryService(
        IAdvertisementQueryRepository advertisementRepository,
        IAdvertisementVariantQueryService variantService,
        ICategoryQueryService categoryService,
        ICacheService cacheService,
        IStatusQueryService statusService,
        IOptions<CacheSettings> settings,
        ILanguageProvider language
    )
        : base(advertisementRepository, cacheService, settings)
    {
        _advertisementRepository = advertisementRepository;
        _variantService = variantService;
        _categoryService = categoryService;
        _statusService = statusService;
        _language = language;
    }


    public override async Task<AdvertisementDto> ApplyMapperAsync(
        Advertisement entity)
    {
        var attributes = 
            await GetAttributesByVariantIdAsync(entity.Variants);

        var categoryDto = await _categoryService
            .ApplyMapperAsync(entity.Category);

        var statusDto = await _statusService
            .ApplyMapperAsync(entity.Status);

        return AdvertisementMapper.ToDto(
            entity,
            attributes,
            categoryDto,
            statusDto);
    }

    public override string GetCacheKey(
        long id)
    {
        return CacheKeys.EntityLanguage<Advertisement>(
            id,
            _language.Language);
    }

    public override async Task CreateCache(
        AdvertisementDto dto)
    {
        await _cache.SetAsync(
            CacheKeys.EntityLanguage<Advertisement>(
                dto.Id, _language.Language),
            dto,
            _settings.KeyExpiration);
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
        var cacheKey = AdvertisementCache.BySeller(
           userId,
           _language.Language,
           pagination.Page,
           pagination.PageSize);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var advert = await _advertisementRepository
                    .GetBySellerAsync(userId, pagination);

                return await advert.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }

    public async Task<Result<PagedResult<AdvertisementDto>>> GetPurchasedByUserAsync(
        long userId,
        PaginationParams pagination)
    {
        var cacheKey = AdvertisementCache.ByBuyer(
           userId,
           _language.Language,
           pagination.Page,
           pagination.PageSize);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var advert = await _advertisementRepository
                    .GetPurchasedByUserAsync(userId, pagination);

                return await advert.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }
}