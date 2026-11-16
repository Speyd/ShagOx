using ShagOxServer.Domain.Entities.Deliveries.Enum;

namespace ShagOxServer.Application.Interfaces.Services.Deliveries.Queries;
public interface IWarehouseTypeResolve<TDto>
{
    Task<IReadOnlyList<TDto>> GetWarehouseTypesAsync(
        CancellationToken cancellationToken);

    string GetWarehouseTypeRef(
        DeliveryPointType type,
        IReadOnlyList<TDto> warehouseTypes);
}
