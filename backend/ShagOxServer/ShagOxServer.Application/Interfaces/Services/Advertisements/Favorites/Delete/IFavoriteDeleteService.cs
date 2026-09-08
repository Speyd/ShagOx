using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Delete;
public interface IFavoriteDeleteService
{
    Task<Result<DeleteResponse>> DeleteAsync(
       int id, int userId);
}