using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Query;
using ShagOxServer.Application.Services.Base;
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
        IAvatarQueryRepository avatarQueryRepository
    )
        : base(avatarQueryRepository)
    {
        _avatarQueryRepository = avatarQueryRepository;
    }


    protected override AvatarDto ApplyMapper(
        Avatar entity)
    {
        return AvatarMapper.ToDto(entity);
    }

    public async Task<Result<AvatarDto>> GetByUserIdAsync(
        long advertId)
    {
        var image = await _avatarQueryRepository
            .GetByUserIdAsync(advertId);

        return image.ToResult(AvatarMapper.ToDto);
    }
}