using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Caches;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.CategoryImages.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Query;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Avatars;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.CategoryImages;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Query;
using ShagOxServer.Application.Services.Base.Query;
using ShagOxServer.Application.Services.Caches.Keys.Specification.Pictures;
using ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Mapping;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.CategoryImages;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Query;
public class CategoryImageQueryService
    : BaseQueryService<
        CategoryImageDto,
        CategoryImage,
        CategoryImageSearchFilter
        >,
    ICategoryImageQueryService
{
    private readonly ICategoryImageQueryRepository _categoryQueryRepository;

    protected override bool CacheBySearch => false;

    protected override bool CacheByPaged => false;


    public CategoryImageQueryService(
        ICategoryImageQueryRepository categoryQueryRepository,
        IDataCacheService cacheService,
        IOptions<CacheOptions> settings
    )
        : base(categoryQueryRepository, cacheService, settings)
    {
        _categoryQueryRepository = categoryQueryRepository;
    }


    public override async Task<CategoryImageDto> ApplyMapperAsync(
        CategoryImage entity)
    {
        return CategoryImageMapper.ToDto(entity);
    }

    public async Task<Result<CategoryImageDto>> GetByCategoryIdAsync(
        long categoryId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = CategoryImageCache.ByCategory(categoryId);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var image = await _categoryQueryRepository
                    .GetByOwnerAsync(categoryId,
                        cancellationToken, status);

                return await image.ToResultAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }
}
