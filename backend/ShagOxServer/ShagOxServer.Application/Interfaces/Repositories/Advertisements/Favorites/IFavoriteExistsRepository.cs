namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
public interface IFavoriteExistsRepository
{
    Task<bool> ExistsByIdAsync(int id);
}