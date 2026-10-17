using ShagOxServer.Application.DTOs.Advertisements.Favorites.Create;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Create;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Create;
public interface IFavoriteCreateService
    : ICreateService<
        CreateResponse,
        FavoriteCreateRequest
        >
{
}