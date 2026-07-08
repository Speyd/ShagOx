namespace ShagOxServer.Infrastructure.Interfaces.Location.Regions;
public interface IRegionExistsRepository
{
    Task<bool> ExistsAsync(string? name);
}
