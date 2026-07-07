using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Interfaces.Advertisements.Favorites;
public interface IFavoriteRepository
{
    Task<Favorite?> GetByIdAsync(int id);

    Task AddAsync(Favorite favorite);

    Task DeleteAsync(Favorite favorite);

    Task<bool> UpdateAsync(Favorite favorite);
}

