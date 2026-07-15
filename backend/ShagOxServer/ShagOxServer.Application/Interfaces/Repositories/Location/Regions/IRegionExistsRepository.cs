namespace ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
public interface IRegionExistsRepository
{
    Task<bool> ExistsByIdAsync(int id);

    Task<bool> ExistsByNameAsync(string? name);
}