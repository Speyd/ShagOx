using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;

namespace ShagOxServer.Application.Interfaces.Services.Location.Cities.Delete;
public interface ICityDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<DeleteResponse>
{
}
