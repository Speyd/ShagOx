using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Query;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Mapping;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Query;
public class AvatarQueryService
    : IAvatarQueryService
{
    private readonly IAvatarQueryRepository _avatarQueryRepository;


    public AvatarQueryService(
        IAvatarQueryRepository avatarQueryRepository)
    {
        _avatarQueryRepository = avatarQueryRepository;
    }


    public async Task<Result<AvatarDto>> GetByIdAsync(
        int id)
    {
        var image = await _avatarQueryRepository
            .GetByIdAsync(id);

        return image.ToResult(AvatarMapper.ToDto);
    }

    public async Task<Result<AvatarDto>> GetByUserIdAsync(
        int advertId)
    {
        var image = await _avatarQueryRepository
            .GetByUserIdAsync(advertId);

        return image.ToResult(AvatarMapper.ToDto);
    }

    public async Task<Result<PagedResult<AvatarDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var images = await _avatarQueryRepository
            .GetPagedAsync(pagination);

        return images.ToResultPaged(AvatarMapper.ToDto);
    }
}