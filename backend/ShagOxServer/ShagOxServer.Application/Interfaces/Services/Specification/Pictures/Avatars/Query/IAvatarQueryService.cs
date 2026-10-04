using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Query;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Avatars;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Query;
public interface IAvatarQueryService
    : IQueryService<AvatarDto, Avatar, AvatarSearchFilter>
{
    Task<Result<AvatarDto>> GetByUserIdAsync(
        long userId);
}