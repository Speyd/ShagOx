using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Caches;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Caches.Keys.Dictionaries;
using ShagOxServer.Application.Services.Dictionaries.Categories.Mapping;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Query;

public class CategoryQueryService
    : BaseTranslatableQueryService<
        CategoryDto,
        Category,
        CategorySearchFilter
        >,
    ICategoryQueryService
{
    private readonly ICategoryQueryRepository _categoryQueryRepository;
    private readonly ICategoryImageQueryService _imageQueryService;

    private readonly ICategoryTranslationQueryRepository _translationRepository;


    public CategoryQueryService(
        ICategoryQueryRepository categoryQueryRepository,
        ICategoryImageQueryService imageQueryService,
        ICategoryTranslationQueryRepository translationRepository,
        ILanguageProvider language,
        IDataCacheService cacheService,
        IOptions<CacheOptions> cacheOptions
    )
        : base(categoryQueryRepository, 
            language, 
            cacheService, 
            cacheOptions)
    {
        _categoryQueryRepository = categoryQueryRepository;
        _imageQueryService = imageQueryService;
        _translationRepository = translationRepository;
    }


    public override async Task<CategoryDto> ApplyMapperAsync(
        Category entity)
    {
        var translation = await _translationRepository
            .GetByIdentificatorAsync(entity.Code, _language.Language);

        var imageDto = await _imageQueryService
            .ApplyMapperAsync(entity.CategoryImage);

        return CategoryMapper.ToDto(entity, imageDto, translation?.Name);
    }

    public async Task<Result<PagedResult<CategoryDto>>> GetByProductTypeAsync(
        long productTypeId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = CategoryCache.ByProductType(
           productTypeId,
           _language.Language,
           pagination.Page,
           pagination.PageSize,
           status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var categories = await _categoryQueryRepository
                    .GetByProductTypeAsync(productTypeId, pagination,
                        cancellationToken, status);

                return await categories.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.DefaultExpiration
        );
    }
}
