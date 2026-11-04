using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Interfaces.Services.Location.Cities.Delete;
public interface ICityDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<City, DeleteResponse>
{
}
