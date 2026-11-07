using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Delete;
public interface IAdvertisementDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<Advertisement, DeleteResponse>
{
}
