namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
public interface IImageExistsRepository
{
    Task<bool> ExistsByIdAsync(int id);
}