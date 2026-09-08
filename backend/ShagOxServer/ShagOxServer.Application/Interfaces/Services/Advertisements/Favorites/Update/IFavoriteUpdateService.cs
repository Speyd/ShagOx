using ShagOxServer.Application.DTOs.Advertisements.Favorites.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Update;
public interface IFavoriteUpdateService
    : IUpdateService<UpdateResponse, FavoriteUpdateRequest>
{
}