using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Query;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Avatars;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Caches.Keys.Specification.Pictures;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Mapping;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Avatars;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Query;
public class AvatarQueryService
    : BaseQueryService<
        AvatarDto,
        Avatar,
        AvatarSearchFilter
        >,
    IAvatarQueryService
{
    private readonly IAvatarQueryRepository _avatarQueryRepository;


    public AvatarQueryService(
        IAvatarQueryRepository avatarQueryRepository,
        ICacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(avatarQueryRepository, cacheService, settings)
    {
        _avatarQueryRepository = avatarQueryRepository;
    }


    public override async Task<AvatarDto> ApplyMapperAsync(
        Avatar entity)
    {
        return AvatarMapper.ToDto(entity);
    }

    public async Task<Result<AvatarDto>> GetByUserIdAsync(
        long userId)
    {
        var cacheKey = AvatarCache.ByUser(userId);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var image = await _avatarQueryRepository
                    .GetByUserIdAsync(userId);

                return await image.ToResultAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }
}