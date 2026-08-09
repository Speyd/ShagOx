namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
public interface IStatusExistsRepository
{
    Task<bool> ExistsById(int Id);
}