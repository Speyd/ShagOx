using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Domain.Cache.Advertisements;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Cache.Comments;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Auth.UserInvalid;
public partial class UserInvalidationService
    : ICacheInvalidationService<User, UserCacheContext>
{
    private async Task<List<AdvertisementCacheContext>> GetAdvertisementInfos(
        long entityId)
    {
        return await _advertRepository
            .GetCacheInfosByUserAsync(entityId);
    }

    private async Task<PictureCacheContext?> GetAvatarInfo(
        long entityId)
    {
        return await _avatarRepository
            .GetCacheInfoByOwnerAsync(entityId);
    }

    private async Task<List<CommentCacheContext>> GetReviewInfos(
        long entityId)
    {
        return await _reviewRepository
            .GetCacheInfosByOwnerAsync(entityId);
    }

    private async Task<List<CommentCacheContext>> GetReviewRepliInfos(
        long entityId)
    {
        return await _reviewRepliRepository
            .GetCacheInfosByOwnerAsync(entityId);
    }

    private async Task<CurrencyCacheContext?> GetCurrencyInfo(
        long entityId)
    {
        return await _currencyRepository
            .GetCahceInfoByUserAsync(entityId);
    }

    private async Task<List<UserDeviceCacheContext>> GetUserDeviceInfo(
        long entityId)
    {
        return await _deviceRepository
            .GetCacheContextByUserAsync(entityId);
    }
}
