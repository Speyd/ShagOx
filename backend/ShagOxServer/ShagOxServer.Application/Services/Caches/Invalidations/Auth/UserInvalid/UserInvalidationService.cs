using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Avatars;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Auth.UserInvalid;
public partial class UserInvalidationService
    : ICacheInvalidationService<User, UserCacheContext>
{
    private readonly IAdvertisementQueryRepository _advertRepository;
    private readonly AdvertisementInvalidationService _advertInvalid;

    private readonly IAvatarQueryRepository _avatarRepository;
    private readonly AvatarInvalidationService _avatarInvalid;

    private readonly IReviewCommentQueryRepository _reviewRepository;
    private readonly ReviewCommentInvalidationService _reviewInvalid;

    private readonly IReviewRepliCommentQueryRepository _reviewRepliRepository;
    private readonly ReviewRepliCommentInvalidationService _reviewRepliInvalid;

    private readonly ICurrencyQueryRepository _currencyRepository;
    private readonly CurrencyInvalidationService _currencyInvalid;

    private readonly IUserDeviceQueryRepository _deviceRepository;
    private readonly UserDeviceInvalidationService _deviceInvalid;

    private readonly IDataCacheService _cache;


    public UserInvalidationService(
        IAdvertisementQueryRepository advertRepository,
        AdvertisementInvalidationService advertInvalid,
        IAvatarQueryRepository avatarRepository,
        AvatarInvalidationService avatarInvalid,
        IReviewCommentQueryRepository reviewRepository,
        ReviewCommentInvalidationService reviewInvalid,
        IReviewRepliCommentQueryRepository reviewRepliRepository,
        ReviewRepliCommentInvalidationService reviewRepliInvalid,
        ICurrencyQueryRepository currencyRepository,
        CurrencyInvalidationService currencyInvalid,
        IUserDeviceQueryRepository deviceRepository,
        UserDeviceInvalidationService deviceInvalid,
        IDataCacheService cache)
    {
        _advertRepository = advertRepository;
        _advertInvalid = advertInvalid;
        _avatarRepository = avatarRepository;
        _avatarInvalid = avatarInvalid;
        _reviewRepository = reviewRepository;
        _reviewInvalid = reviewInvalid;
        _reviewRepliRepository = reviewRepliRepository;
        _reviewRepliInvalid = reviewRepliInvalid;
        _currencyRepository = currencyRepository;
        _currencyInvalid = currencyInvalid;
        _deviceRepository = deviceRepository;
        _deviceInvalid = deviceInvalid;
        _cache = cache;
    }
}
