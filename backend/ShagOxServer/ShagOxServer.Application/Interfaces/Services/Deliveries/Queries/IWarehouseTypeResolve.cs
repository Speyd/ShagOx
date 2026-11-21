using ShagOxServer.Domain.Entities.Deliveries.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Deliveries.Queries;
public interface IWarehouseTypeResolve<TDto>
{
    Task<Result<IReadOnlyList<TDto>>> GetWarehouseTypesAsync(
        CancellationToken cancellationToken);

    string GetWarehouseTypeRef(
        DeliveryPointType type,
        IReadOnlyList<TDto> warehouseTypes);
}
