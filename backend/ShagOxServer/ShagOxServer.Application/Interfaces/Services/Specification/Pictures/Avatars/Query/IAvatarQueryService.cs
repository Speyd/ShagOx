using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Query;
public interface IAvatarQueryService
    : IQueryService<AvatarDto>
{
    Task<Result<AvatarDto>> GetByUserIdAsync(int userId);
}