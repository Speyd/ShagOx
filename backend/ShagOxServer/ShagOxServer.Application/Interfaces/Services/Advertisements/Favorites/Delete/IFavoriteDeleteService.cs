using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Delete;
public interface IFavoriteDeleteService
{
    Task<Result<DeleteResponse>> DeleteAsync(
       int id, int userId);
}