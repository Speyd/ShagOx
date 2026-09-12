namespace ShagOxServer.Application.Interfaces.Repositories.Base.Special;
public interface IExistsOwnerRepository
{
    Task<bool> IsOwnerAsync(int entityId, int userId);
}