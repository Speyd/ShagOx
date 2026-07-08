namespace ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
public interface IRegionExistsRepository
{
    Task<bool> ExistsAsync(string? name);
}
