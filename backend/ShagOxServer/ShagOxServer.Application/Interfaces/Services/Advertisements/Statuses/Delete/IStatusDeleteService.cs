using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Delete;
public interface IStatusDeleteService
    : IDeleteService<DeleteResponse>
{
}