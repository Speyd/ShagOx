using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserDevices;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Query;
public interface IUserDeviceQueryService
    : IQueryService<UserDeviceDto, 
        UserDevice, 
        UserDeviceSearchFilter>
{
    Task<Result<UserDeviceDto>> GetByUserAndDeviceAsync(
        long userId,
        long deviceId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Result<UserDevice?>> GetEntityByUserAndDeviceAsync(
        long userId,
        long deviceId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);


    Task<Result<PagedResult<UserDeviceDto>>> GetByUserAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Result<List<UserDeviceDto>>> GetAllByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Result<PagedResult<UserDeviceDto>>> GetByDeviceAsync(
        long deviceId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Result<List<UserDeviceDto>>> GetAllByDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
