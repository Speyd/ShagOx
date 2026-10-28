using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.DTOs.Advertisements.Core.Query;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.CategoryAttributes;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeProcessor;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
using ShagOxServer.Application.Services.Advertisements.Core.Mapping;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Application.Services.Caches.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Advertisements.Core.Query;
public class AdvertisementQueryService 
    : BaseLocalizedQueryService<
        AdvertisementDto, 
        Advertisement, 
        AdvertisementSearchFilter>,
      IAdvertisementQueryService
{
    private readonly IAdvertisementQueryRepository _advertisementRepository;
    private readonly IAdvertisementVariantQueryService _variantService;
    private readonly IAttributeProcessor _attributeProcessor;

    private readonly ICategoryQueryService _categoryService;
    private readonly IStatusQueryService _statusService;

    protected override bool CacheBySearch => false;

    protected override bool CacheByPaged => false;


    public AdvertisementQueryService(
        IAdvertisementQueryRepository advertisementRepository,
        IAdvertisementVariantQueryService variantService,
        IAttributeProcessor attributeProcessor,
        ICategoryQueryService categoryService,
        IDataCacheService cacheService,
        IStatusQueryService statusService,
        IOptions<CacheSettings> settings,
        ILanguageProvider language
    )
        : base(advertisementRepository, 
            language,
            cacheService, 
            settings)
    {
        _advertisementRepository = advertisementRepository;
        _variantService = variantService;
        _attributeProcessor = attributeProcessor;
        _categoryService = categoryService;
        _statusService = statusService;
    }


    public override async Task<AdvertisementDto> ApplyMapperAsync(
        Advertisement entity)
    {
        var variants = new List<AdvertisementVariantDto>();

        foreach (var variant in entity.Variants)
        {
            var variantDto = await _variantService.ApplyMapperAsync(variant);
            variants.Add(variantDto);
        }

        var categoryDto = await _categoryService
            .ApplyMapperAsync(entity.Category);

        var statusDto = await _statusService
            .ApplyMapperAsync(entity.Status);

        var attributeDto = await _attributeProcessor
            .GetAttributesAsync(
                entity.Attributes,
                entity.CategoryId
            );


        return AdvertisementMapper.ToDto(
            entity,
            variants,
            categoryDto,
            statusDto,
            attributeDto.Value!);
    }

    public async Task<Result<PagedResult<AdvertisementDto>>> GetBySellerAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default)
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
                    .GetBySellerAsync(userId, pagination, 
                        cancellationToken);

                return await advert.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }

    public async Task<Result<PagedResult<AdvertisementDto>>> GetPurchasedByUserAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default)
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
                    .GetPurchasedByUserAsync(userId, pagination, 
                        cancellationToken);

                return await advert.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }

    public async Task<Result<PagedResult<AdvertisementShortDto>>> GetShortPagedAsync(
        PaginationParams pagination,
        CancellationToken cancellationToken = default)
    {
        var cacheKey = AdvertisementCache.ByShortPaged(
           _language.Language,
           pagination.Page,
           pagination.PageSize);
       

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var adverts = await _advertisementRepository
                    .GetPagedAsync(pagination, cancellationToken);
                
                return adverts.ToResultPaged(
                    AdvertisementShortMapper.ToDto);
            },
            _settings.KeyExpiration
        );
    }

    public async Task<Result<PagedResult<AdvertisementShortDto>>> ShortSearchAsync(
        AdvertisementSearchFilter filter,
        PaginationParams pagination,
        CancellationToken cancellationToken = default)
    {
        var cacheKey = AdvertisementCache.ByShortSearch(
            filter,
           _language.Language,
           pagination.Page,
           pagination.PageSize);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var adverts = await _advertisementRepository
                    .SearchAsync(filter, pagination, 
                        cancellationToken);

                return adverts.ToResultPaged(
                    AdvertisementShortMapper.ToDto);
            },
            _settings.KeyExpiration
        );
    }
}
