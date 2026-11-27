using ShagOxServer.Application.DTOs.Devices.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Devices;
using ShagOxServer.Domain.Filters.Devices;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Devices.Core.Query;
public interface IDeviceQueryService
    : IQueryService<DeviceDto,
        Device,
        DeviceSearchFilter>
{
    Task<Result<DeviceDto>> GetByIdentifierAsync(
        string identifier,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Result<Device?>> GetEntityByIdentifierAsync(
        string identifier,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Result<Device?>> GetEntityByIdentifierNoTrackingAsync(
        string identifier,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
