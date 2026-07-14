namespace ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
public interface IRegionExistsRepository
{
    Task<bool> ExistsAsync(int id);

    Task<bool> ExistsAsync(string? name);
}