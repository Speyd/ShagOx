using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Interfaces.Services.Location.Regions.Delete;
public interface IRegionDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<Region, DeleteResponse>
{
}
