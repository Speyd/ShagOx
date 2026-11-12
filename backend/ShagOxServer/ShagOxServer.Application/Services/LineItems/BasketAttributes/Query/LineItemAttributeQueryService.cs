using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Query;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Application.Services.Caches.Keys.LineItems;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Mapping;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.LineItems;
using ShagOxServer.Domain.Filters.LineItems.LineItemAttributes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.LineItems.LineItemAttributes.Query;
public class LineItemAttributeQueryService
    : BaseLocalizedQueryService<
        LineItemAttributeDto,
        LineItemAttribute,
        LineItemAttributeSearchFilter
        >,
    ILineItemAttributeQueryService
{
    private readonly ILineItemAttributeQueryRepository _attributeQueryRepository;

    private readonly IAttributeDefinitionQueryService _attributeService;


    public LineItemAttributeQueryService(
        ILineItemAttributeQueryRepository attributeQueryRepository,
        IAttributeDefinitionQueryService attributeService,
        IDataCacheService cacheService,
        IOptions<CacheSettings> settings,
        ILanguageProvider language
    )
        : base(attributeQueryRepository, language, cacheService, settings)
    {
        _attributeQueryRepository = attributeQueryRepository;
        _attributeService = attributeService;
    }


    public override async Task<LineItemAttributeDto> ApplyMapperAsync(
        LineItemAttribute entity)
    {
        var attributerDto = await _attributeService
            .ApplyMapperAsync(entity.AttributeDefinition);

        return LineItemAttributeMapper.ToDto(entity, attributerDto);
    }

    public async Task<Result<LineItemAttributeDto>> GetByAttributeDefinitionAsync(
        long attributeDefenitionId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = LineItemAttributeCache.ByAttributeDefenition(
            attributeDefenitionId, 
            _language.Language,
            status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var attribute = await _attributeQueryRepository
                    .GetByAttributeDefinitionAsync(attributeDefenitionId,
                        cancellationToken, status);

                return await attribute.ToResultAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }

    public async Task<Result<PagedResult<LineItemAttributeDto>>> GetByCategoryAsync(
        long categoryId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = LineItemAttributeCache.ByCategory(
            categoryId,
            _language.Language,
            pagination.Page,
            pagination.PageSize,
            status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var attributes = await _attributeQueryRepository
                    .GetByCategoryAsync(categoryId, pagination,
                        cancellationToken, status);

                return await attributes.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }
}
