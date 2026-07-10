using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
public interface IFavoriteRepository
{
    Task<Favorite?> GetByIdAsync(int id);

    void Add(Favorite favorite);

    void Delete(Favorite favorite);

    bool Update(Favorite favorite);
}