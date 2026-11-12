using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Base.Items.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Application.Services.Caches.Keys.LineItems;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Text.Json;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
using ShagOxServer.Application.Interfaces.Services.LineItems.Core.Query;
using ShagOxServer.Application.Interfaces.Repositories.LineItems.LineItemAttributes;
using ShagOxServer.Domain.Entities.LineItems.Core;

namespace ShagOxServer.Application.Services.LineItems.Core.Query;
public abstract class LineItemQueryService<TDto, TEntity, TFilter>
    : BaseLocalizedQueryService<TDto, TEntity, TFilter>,
    ILineItemQueryService<TDto, TEntity, TFilter>
    where TDto : BaseDto
    where TEntity : LineItem
    where TFilter : BaseFilter  
{
    private readonly ILineItemQueryRepository<TEntity, TFilter> _itemQueryRepository;

    private readonly ILineItemAttributeExistsRepository _attributeRepository;

    
    public LineItemQueryService(
        ILineItemQueryRepository<TEntity, TFilter> itemQueryRepository,
        ILineItemAttributeExistsRepository attributeRepository,
        IDataCacheService cacheService,
        IOptions<CacheSettings> settings,
        ILanguageProvider language
    )
        : base(itemQueryRepository, language, cacheService, settings)
    {
        _itemQueryRepository = itemQueryRepository;
        _attributeRepository = attributeRepository;
    }


    public async Task<Result<PagedResult<TDto>>> GetByParentAsync(
        long parentId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = LineItemsCache<TEntity>.ByParent(
           parentId,
           _language.Language,
           pagination.Page,
           pagination.PageSize,
           status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var items = await _itemQueryRepository
                    .GetByParentAsync(parentId, pagination,
                        cancellationToken, status);

                return await items.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }

    public abstract Task<Result<PagedResult<TDto>>> GetPagedAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    public async Task PrepareAttributesForMappingAsync(
        Advertisement advert,
        AdvertisementVariant? variant)
    {
        if (variant is not null)
        {
            var variantKeys = await _attributeRepository
                .ExistsByKeysAsync(
                    variant.Attributes,
                    advert.CategoryId);

            variant.Attributes = FilterAttributes(variant.Attributes, variantKeys);
        }


        var advertisementKeys = await _attributeRepository.ExistsByKeysAsync(
            advert.Attributes,
            advert.CategoryId);


        advert.Attributes = FilterAttributes(
            advert.Attributes,
            advertisementKeys);
    }

    protected static JsonDocument FilterAttributes(
        JsonDocument attributes,
        List<string> existingKeys)
    {
        var filtered = attributes.RootElement
            .EnumerateObject()
            .Where(x => existingKeys.Contains(x.Name))
            .ToDictionary(x => x.Name, x => x.Value.Clone());

        return JsonDocument.Parse(
            JsonSerializer.Serialize(filtered));
    }
}
