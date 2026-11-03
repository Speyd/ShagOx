using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Delete;
public interface IFavoriteDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<DeleteResponse>
{
}
