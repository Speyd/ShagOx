using ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta.Query;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Queries;

namespace ShagOxServer.Application.Interfaces.Services.Deliveries.Providers.NovaPoshta;
public interface INovaPoshtaTypeResolver
    : IWarehouseTypeResolve<NovaPoshtaWarehouseTypeDto>
{
}
