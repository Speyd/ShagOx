using ShagOxServer.Application.DTOs.Advertisements.Favorites.Create;
using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Create;
public interface IFavoriteCreateService
    : ICreateService<
        CreateResponse,
        FavoriteCreateRequest
        >
{
}