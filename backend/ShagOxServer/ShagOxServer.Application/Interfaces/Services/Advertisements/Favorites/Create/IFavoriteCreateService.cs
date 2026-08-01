using ShagOxServer.Application.DTOs.Advertisements.Favorites.Create;
using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Create;
public interface IFavoriteCreateService
{
    Task<Result<CreateResponse>> CreateAsync(
       FavoriteCreateRequest request);
}